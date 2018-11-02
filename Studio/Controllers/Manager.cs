using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Studio.Models;
using System.Security.Claims;

namespace Studio.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper components
        MapperConfiguration config;
        public IMapper mapper;

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;

            // If necessary, add constructor code here

            // Configure the AutoMapper components
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.ActingDoc, Controllers.ActingBase>();
                cfg.CreateMap<Models.DanceDoc, Controllers.DanceBase>();
                cfg.CreateMap<Models.VocalDoc, Controllers.VocalBase>();

                cfg.CreateMap<Controllers.VocalAdd, Models.VocalDoc>();

                cfg.CreateMap<Models.VocalDoc, Controllers.VocalPhoto>();
                cfg.CreateMap<Models.VocalDoc, Controllers.VocalAudio>();
                cfg.CreateMap<Models.VocalDoc, Controllers.VocalText>();
                cfg.CreateMap<Models.VocalDoc, Controllers.VocalVideo>();

                cfg.CreateMap<Controllers.VocalBase, Controllers.VocalEditForm>();

            });

            mapper = config.CreateMapper();
        }

        // ############################################################
        // RoleClaim
        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }


        // ############################################################
        // Acting
        public IEnumerable<ActingBase> ActingGetAll()
        {
            var o = ds.ActingDocs;
            if (o == null)
                return null;
            else
                return mapper.Map<IEnumerable<ActingBase>>(o);
        }


        // ############################################################
        // Dance
        public IEnumerable<DanceBase> DanceGetAll()
        {
            var o = ds.DanceDocs;
            if (o == null)
                return null;
            else
                return mapper.Map<IEnumerable<DanceBase>>(o);
        }


        // ############################################################
        // Vocal
        public IEnumerable<VocalBase> VocalGetAll()
        {
            var o = ds.VocalDocs;
            if (o == null)
                return null;
            else
                return mapper.Map<IEnumerable<VocalBase>>(o);
        }


        public VocalBase VocalGetOne(int id)
        {
            var o = ds.VocalDocs.Find(id);
            return mapper.Map<VocalBase>(o);
        }


        public VocalBase VocalAddNew(VocalAdd newItem)
        {
            var addedItem = ds.VocalDocs.Add(mapper.Map<VocalDoc>(newItem));

            // Doc
            if (newItem.TextUpload != null)
            {
                var textSize = newItem.TextUpload.ContentLength;
                byte[] textBytes = new byte[textSize];
                newItem.TextUpload.InputStream.Read(textBytes, 0, textSize);
                addedItem.TextContentType = newItem.TextUpload.ContentType;
                addedItem.Text = textBytes;
                addedItem.txtB = true;
            }

            // Photo
            if (newItem.PhotoUpload != null)
            {
                var photoSize = newItem.PhotoUpload.ContentLength;
                byte[] photoBytes = new byte[photoSize];
                newItem.PhotoUpload.InputStream.Read(photoBytes, 0, photoSize);
                addedItem.Photo = photoBytes;
                addedItem.PhotoContentType = newItem.PhotoUpload.ContentType;
                addedItem.imageB = true;
            }


            // Audio
            if (newItem.AudioUpload != null)
            {
                var audioSize = newItem.AudioUpload.ContentLength;
                byte[] audioBytes = new byte[audioSize];
                newItem.AudioUpload.InputStream.Read(audioBytes, 0, audioSize);
                addedItem.Audio = audioBytes;
                addedItem.AudioContentType = newItem.AudioUpload.ContentType;
                addedItem.audioB = true;
            }


            // Video
            if (newItem.VideoUpload != null)
            {
                var videoSize = newItem.VideoUpload.ContentLength;
                byte[] videoBytes = new byte[videoSize];
                newItem.VideoUpload.InputStream.Read(videoBytes, 0, videoSize);
                addedItem.Video = videoBytes;
                addedItem.VideoContentType = newItem.VideoUpload.ContentType;
                addedItem.videoB = true;
            }


            ds.SaveChanges();

            var var1 = addedItem;

            return (addedItem == null) ? null : mapper.Map<VocalBase>(addedItem);
        }

        public VocalBase VocalEditForm(VocalEditForm newItem)
        {
            var o = ds.VocalDocs.Find(newItem.Id);

            if (o == null)
                return null;

            ds.Entry(o).CurrentValues.SetValues(newItem);
            ds.SaveChanges();

            return mapper.Map<VocalBase>(o);
        }

        public VocalText VocalText(int id)
        {
            var o = ds.VocalDocs.Find(id);
            return (o == null) ? null : mapper.Map<VocalText>(o);
        }

        public VocalPhoto GetVocalPhoto(int id)
        {
            var o = ds.VocalDocs.Find(id);
            return (o == null) ? null : mapper.Map<VocalPhoto>(o);
        }

        public VocalAudio VocalAudio(int id)
        {
            var o = ds.VocalDocs.Find(id);
            return (o == null) ? null : mapper.Map<VocalAudio>(o);
        }

        public VocalVideo VocalVideo(int id)
        {
            var o = ds.VocalDocs.Find(id);
            return (o == null) ? null : mapper.Map<VocalVideo>(o);
        }

        public bool VocalDelete(int id)
        {
            var item = ds.VocalDocs.Find(id);
            if (item == null)
                return false;
            else
            {
                ds.VocalDocs.Remove(item);
                ds.SaveChanges();
                return true;
            }
        }







        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;
            
            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new RoleClaim { Name = "Admin" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Teacher" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Student" });
                ds.SaveChanges();
                done = true;
            }

            if (ds.ActingDocs.Count() == 0)
            {
                // Add role claims here
                ds.ActingDocs.Add(new ActingDoc { Title = "Zolushka" });
                ds.ActingDocs.Add(new ActingDoc { Title = "Gore t uma" });
                ds.ActingDocs.Add(new ActingDoc { Title = "Drakon" });

                ds.SaveChanges();
                done = true;
            }

            if (ds.DanceDocs.Count() == 0)
            {
                // Add role claims here
                ds.DanceDocs.Add(new DanceDoc { Title = "Tanec 1" });
                ds.DanceDocs.Add(new DanceDoc { Title = "Palanez" });
                ds.DanceDocs.Add(new DanceDoc { Title = "Waltz" });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}