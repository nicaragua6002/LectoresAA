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
    public class CapituloesController : Controller
    {
        private LectoresAAContainer db = new LectoresAAContainer();

        // GET: Capituloes
        public ActionResult Index()
        {
            var capitulos = db.Capitulos.Include(c => c.Publicacion);
            return View(capitulos.ToList());
        }

        // GET: Capituloes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitulo capitulo = db.Capitulos.Find(id);
            if (capitulo == null)
            {
                return HttpNotFound();
            }
            return View(capitulo);
        }

        // GET: Capituloes/Create
        public ActionResult Create()
        {
            ViewBag.PublicacionId = new SelectList(db.Publicaciones, "Id", "Autor");
            return View();
        }

        // POST: Capituloes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Fecha,Contenido,PublicacionId")] Capitulo capitulo)
        {
            if (ModelState.IsValid)
            {
                db.Capitulos.Add(capitulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PublicacionId = new SelectList(db.Publicaciones, "Id", "Autor", capitulo.PublicacionId);
            return View(capitulo);
        }

        // GET: Capituloes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitulo capitulo = db.Capitulos.Find(id);
            if (capitulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublicacionId = new SelectList(db.Publicaciones, "Id", "Autor", capitulo.PublicacionId);
            return View(capitulo);
        }

        // POST: Capituloes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Fecha,Contenido,PublicacionId")] Capitulo capitulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capitulo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PublicacionId = new SelectList(db.Publicaciones, "Id", "Autor", capitulo.PublicacionId);
            return View(capitulo);
        }

        // GET: Capituloes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitulo capitulo = db.Capitulos.Find(id);
            if (capitulo == null)
            {
                return HttpNotFound();
            }
            return View(capitulo);
        }

        // POST: Capituloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Capitulo capitulo = db.Capitulos.Find(id);
            db.Capitulos.Remove(capitulo);
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
