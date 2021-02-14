using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProduktyController : ApiController
    {
        private CRUDDBEntities db = new CRUDDBEntities();

        // GET: api/Produkty
        public IQueryable<Produkty> GetProdukty()
        {
            return db.Produkty;
        }

        // GET: api/Produkty/5
        [ResponseType(typeof(Produkty))]
        public IHttpActionResult GetProdukty(int id)
        {
            Produkty produkty = db.Produkty.Find(id);
            if (produkty == null)
            {
                return NotFound();
            }

            return Ok(produkty);
        }

        // PUT: api/Produkty/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProdukty(int id, Produkty produkty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produkty.IdProduktu)
            {
                return BadRequest();
            }

            db.Entry(produkty).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduktyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Produkty
        [ResponseType(typeof(Produkty))]
        public IHttpActionResult PostProdukty(Produkty produkty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Produkty.Add(produkty);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = produkty.IdProduktu }, produkty);
        }

        // DELETE: api/Produkty/5
        [ResponseType(typeof(Produkty))]
        public IHttpActionResult DeleteProdukty(int id)
        {
            Produkty produkty = db.Produkty.Find(id);
            if (produkty == null)
            {
                return NotFound();
            }

            db.Produkty.Remove(produkty);
            db.SaveChanges();

            return Ok(produkty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProduktyExists(int id)
        {
            return db.Produkty.Count(e => e.IdProduktu == id) > 0;
        }
    }
}