using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LectoresAA.Models;

namespace LectoresAA.Controllers
{
    [Authorize]
    public class PublicacionsController : Controller
    {
        private LectoresAAContainer db = new LectoresAAContainer();

        // GET: Publicacions
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var publicaciones = db.Publicaciones.Include(p => p.Tipo);
                return View(publicaciones.ToList());
            }else
            {
                var publicaciones = db.Publicaciones.Where(x=>x.User==User.Identity.Name).Include(p => p.Tipo);
                return View(publicaciones.ToList());
            }
        }

        // GET: Publicacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // GET: Publicacions/Create
        public ActionResult Create()
        {
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nombre");
            return View();
        }

        // POST: Publicacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Autor,Titulo,Descripcion,Fecha,User,TipoId")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                db.Publicaciones.Add(publicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nombre", publicacion.TipoId);
            return View(publicacion);
        }

        // GET: Publicacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nombre", publicacion.TipoId);
            return View(publicacion);
        }

        // POST: Publicacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Autor,Titulo,Descripcion,Fecha,User,TipoId")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicacion).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nombre", publicacion.TipoId);
            return View(publicacion);
        }

        // GET: Publicacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // POST: Publicacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publicacion publicacion = db.Publicaciones.Find(id);
            db.Publicaciones.Remove(publicacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
