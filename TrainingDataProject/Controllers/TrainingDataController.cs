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
using TrainingDataProject.Models;

namespace TrainingDataProject.Controllers
{
    public class TrainingDataController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/TrainingData
        public IQueryable<TrainingData> GetTrainingDatas()
        {
            return db.TrainingDatas;
        }

        // GET: api/TrainingData/5
        [ResponseType(typeof(TrainingData))]
        public IHttpActionResult GetTrainingData(int id)
        {
            TrainingData trainingData = db.TrainingDatas.Find(id);
            if (trainingData == null)
            {
                return NotFound();
            }

            return Ok(trainingData);
        }

        // PUT: api/TrainingData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrainingData(int id, TrainingData trainingData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingData.ID)
            {
                return BadRequest();
            }

            db.Entry(trainingData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingDataExists(id))
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

        // POST: api/TrainingData
        [ResponseType(typeof(TrainingData))]
        public IHttpActionResult PostTrainingData(TrainingData trainingData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TrainingDatas.Add(trainingData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trainingData.ID }, trainingData);
        }

        // DELETE: api/TrainingData/5
        [ResponseType(typeof(TrainingData))]
        public IHttpActionResult DeleteTrainingData(int id)
        {
            TrainingData trainingData = db.TrainingDatas.Find(id);
            if (trainingData == null)
            {
                return NotFound();
            }

            db.TrainingDatas.Remove(trainingData);
            db.SaveChanges();

            return Ok(trainingData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainingDataExists(int id)
        {
            return db.TrainingDatas.Count(e => e.ID == id) > 0;
        }
    }
}