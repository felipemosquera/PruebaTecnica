using apiPruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace apiPruebaTecnica.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/person")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class PersonController : ApiController
    {
        Models.PruebaTecnicaEntities db = new Models.PruebaTecnicaEntities();

        [HttpGet]
        [Route("getPersons")]
        public IHttpActionResult getPersons()
        {
            try
            {
                return Ok(db.Person.ToList());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getPersonById/{id}")]
        public IHttpActionResult getPersonById(int id)
        {
            try
            {
                var person = db.Person.Find(id);
                if (person == null) return NotFound();
                return Ok(person);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addNewPerson")]
        public IHttpActionResult addPerson(Person person)
        {
            try
            {
                var checkPersonExist = db.Person.Where(x => x.Email == person.Email).FirstOrDefault();
                if (checkPersonExist != null)
                {
                    return BadRequest("email already exist");
                }
                person.CreateAt = DateTime.Now;
                db.Person.Add(person);
                db.SaveChanges();
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("editPerson/{id}")]
        public IHttpActionResult editPerson(Person person, int id)
        {
            try
            {
                var oldPerson = db.Person.Find(id);
                if (oldPerson == null) return NotFound();
                if (id != person.Id) return BadRequest("id not match");
                db.Entry(oldPerson).CurrentValues.SetValues(person);
                db.SaveChanges();
                return Ok(person);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
