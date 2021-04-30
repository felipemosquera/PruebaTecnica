using apiPruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/user")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        apiPruebaTecnica.Models.PruebaTecnicaEntities db = new apiPruebaTecnica.Models.PruebaTecnicaEntities();

        [HttpGet]
        [Route("getUsers")]
        public IHttpActionResult getUsers()
        {
            try
            {
                return Ok(db.Users.ToList());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        public IHttpActionResult getUserById(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPost]
        [Route("AddNewUser")]
        public IHttpActionResult AddNewUser(Users user)
        {
            var crypt = new SHA256Managed();
            try
            {
                var person = db.Person.Find(user.IdPerson);
                if (person != null)
                {
                    user.Password = GetHashString(user.Password);
                    user.CreateAt = DateTime.Now;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Person not Found");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        [HttpPut]
        [Route("EditUser/{id}")]
        public IHttpActionResult EditPropertyUser(Users user, int id)
        {
            try
            {
                var oldUser = db.Users.Find(id);
                if (oldUser == null) return NotFound();
                if (user.Password != oldUser.Password) user.Password = GetHashString(user.Password);
                db.Entry(oldUser).CurrentValues.SetValues(user);
                db.SaveChanges();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
