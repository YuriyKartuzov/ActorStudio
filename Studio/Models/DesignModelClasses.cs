using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studio.Models
{
    public class RoleClaim
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class VocalDoc
    {
        public VocalDoc()
        {
            Docs = new List<VocalMedia>();
            LastModified = DateTime.Now;
            audioB = false;
            txtB = false;
            videoB = false;
            imageB = false;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
        public DateTime LastModified { get; set; }
        public bool audioB { get; set; }
        public bool txtB { get; set; }
        public bool videoB { get; set; }
        public bool imageB { get; set; }
        public ICollection<VocalMedia> Docs;

        [StringLength(200)]
        public string PhotoContentType { get; set; }
        public byte[] Photo { get; set; }

        [StringLength(200)]
        public string AudioContentType { get; set; }
        public byte[] Audio { get; set; }

        [StringLength(200)]
        public string TextContentType { get; set; }
        public byte[] Text { get; set; }

        [StringLength(200)]
        public string VideoContentType { get; set; }
        public byte[] Video { get; set; }


    }

    public class VocalMedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string Ext { get; set; }
        public byte[] Content { get; set; }

        [Required]
        public VocalDoc VocalDoc { get; set; }
    }



    public class DanceDoc
    {
        public DanceDoc()
        {
            Docs = new List<DanceMedia>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<DanceMedia> Docs;
    }

    public class DanceMedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string Ext { get; set; }
        public byte[] Content { get; set; }

        [Required]
        public DanceDoc DanceDoc { get; set; }
    }



    public class ActingDoc
    {
        public ActingDoc()
        {
            Docs = new List<ActingMedia>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<ActingMedia> Docs;
    }

    public class ActingMedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string Ext { get; set; }
        public byte[] Content { get; set; }

        [Required]
        public ActingDoc ActingDoc { get; set; }
    }
}