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
    public class DriversController : ApiController
    {
        private CModel db = new CModel();

        // GET: api/Drivers
        public IQueryable<Driver> GetDriver()
        {
            return db.Driver;
        }

        // GET: api/Drivers/22832
        [ResponseType(typeof(Driver))]
        public async Task<IHttpActionResult> GetDriver(int login)
        {
            Driver driver = await db.Driver.FirstOrDefaultAsync(x => x.login == login);
            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        // PUT: api/Drivers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDriver(Guid id, Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.userid)
            {
                return BadRequest();
            }

            db.Entry(driver).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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

        // POST: api/Drivers
        [ResponseType(typeof(Driver))]
        public async Task<IHttpActionResult> PostDriver(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Driver.Add(driver);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DriverExists(driver.userid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = driver.userid }, driver);
        }

        // DELETE: api/Drivers/5
        [ResponseType(typeof(Driver))]
        public async Task<IHttpActionResult> DeleteDriver(Guid id)
        {
            Driver driver = await db.Driver.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            db.Driver.Remove(driver);
            await db.SaveChangesAsync();

            return Ok(driver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DriverExists(Guid id)
        {
            return db.Driver.Count(e => e.userid == id) > 0;
        }
    }
}