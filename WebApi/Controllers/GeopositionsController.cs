using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi;

namespace WebApi.Controllers
{
    public class GeopositionsController : ApiController
    {
        private CModel db = new CModel();

        // GET: api/Geopositions
        public IQueryable<Geoposition> GetGeoposition()
        {
            return db.Geoposition;
        }

        // GET: api/Geopositions/5
        [ResponseType(typeof(Geoposition))]
        public async Task<IHttpActionResult> GetGeoposition(Guid id)
        {
            Geoposition geoposition = await db.Geoposition.FindAsync(id);
            if (geoposition == null)
            {
                return NotFound();
            }

            return Ok(geoposition);
        }

        // PUT: api/Geopositions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGeoposition(Guid id, Geoposition geoposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != geoposition.geoid)
            {
                return BadRequest();
            }

            db.Entry(geoposition).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeopositionExists(id))
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

        // POST: api/Geopositions
        [ResponseType(typeof(Geoposition))]
        public async Task<IHttpActionResult> PostGeoposition(Geoposition geoposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Geoposition.Add(geoposition);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GeopositionExists(geoposition.geoid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = geoposition.geoid }, geoposition);
        }

        // DELETE: api/Geopositions/5
        [ResponseType(typeof(Geoposition))]
        public async Task<IHttpActionResult> DeleteGeoposition(Guid id)
        {
            Geoposition geoposition = await db.Geoposition.FindAsync(id);
            if (geoposition == null)
            {
                return NotFound();
            }

            db.Geoposition.Remove(geoposition);
            await db.SaveChangesAsync();

            return Ok(geoposition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GeopositionExists(Guid id)
        {
            return db.Geoposition.Count(e => e.geoid == id) > 0;
        }
    }
}