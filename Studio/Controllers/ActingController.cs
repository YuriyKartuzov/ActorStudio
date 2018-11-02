using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Studio.Controllers
{
    public class ActingController : Controller
    {
        Manager m = new Manager();
        // GET: Acting
        public ActionResult Index()
        {
            var o = m.ActingGetAll();
            return View(o);
        }

        // GET: Acting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Acting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acting/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Acting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Acting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Acting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Acting/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
