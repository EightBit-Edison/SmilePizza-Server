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
using WebApi;

namespace WebApi.Controllers
{
    public class GeopositionsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Geopositions
        public IQueryable<Geoposition> GetGeopositions()
        {
			var res = from element in db.Geopositions
					  group element by element.driver
				  into groups
					  select groups.OrderByDescending(p => p.tracktime).FirstOrDefault();
			//var l = db.Geopositions.OrderByDescending(x => x.tracktime).GroupBy(x => x.driver).Select();
            return res;
        }

        // GET: api/Geopositions/5
        [ResponseType(typeof(Geoposition))]
        public IHttpActionResult GetGeoposition(Guid id)
        {
            Geoposition geoposition = db.Geopositions.Find(id);
            if (geoposition == null)
            {
                return NotFound();
            }

            return Ok(geoposition);
        }

        // PUT: api/Geopositions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGeoposition(Guid id, Geoposition geoposition)
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
                db.SaveChanges();
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
        public IHttpActionResult PostGeoposition(Geoposition geoposition)
        {
            geoposition.geoid = Guid.NewGuid();
            geoposition.lattitude = geoposition.lattitude.Replace(",", ".");
            geoposition.longitude = geoposition.longitude.Replace(",", ".");
            geoposition.tracktime = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Geopositions.Add(geoposition);

            try
            {
                db.SaveChanges();
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
        public IHttpActionResult DeleteGeoposition(Guid id)
        {
            Geoposition geoposition = db.Geopositions.Find(id);
            if (geoposition == null)
            {
                return NotFound();
            }

            db.Geopositions.Remove(geoposition);
            db.SaveChanges();

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
            return db.Geopositions.Count(e => e.geoid == id) > 0;
        }
    }
}