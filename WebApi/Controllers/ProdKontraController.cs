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
    public class ProdKontraController : ApiController
    {
        private DBModelK db = new DBModelK();

        // GET: api/ProdKontra
        public IQueryable<ProdKontra> GetProdKontra()
        {
            return db.ProdKontra;
        }

        // GET: api/ProdKontra/5
        [ResponseType(typeof(ProdKontra))]
        public IHttpActionResult GetProdKontra(int id)
        {
            ProdKontra prodKontra = db.ProdKontra.Find(id);
            if (prodKontra == null)
            {
                return NotFound();
            }

            return Ok(prodKontra);
        }

        // PUT: api/ProdKontra/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProdKontra(int id, ProdKontra prodKontra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prodKontra.NIP)
            {
                return BadRequest();
            }

            db.Entry(prodKontra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdKontraExists(id))
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

        // POST: api/ProdKontra
        [ResponseType(typeof(ProdKontra))]
        public IHttpActionResult PostProdKontra(ProdKontra prodKontra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProdKontra.Add(prodKontra);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProdKontraExists(prodKontra.NIP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = prodKontra.NIP }, prodKontra);
        }

        // DELETE: api/ProdKontra/5
        [ResponseType(typeof(ProdKontra))]
        public IHttpActionResult DeleteProdKontra(int id)
        {
            ProdKontra prodKontra = db.ProdKontra.Find(id);
            if (prodKontra == null)
            {
                return NotFound();
            }

            db.ProdKontra.Remove(prodKontra);
            db.SaveChanges();

            return Ok(prodKontra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdKontraExists(int id)
        {
            return db.ProdKontra.Count(e => e.NIP == id) > 0;
        }
    }
}