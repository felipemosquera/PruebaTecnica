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

    [RoutePrefix("api/Auth")]
    [EnableCors(origins: "http://localhost:4200",headers:"*",methods:"*")]
    public class AuthController : ApiController
    {
        apiPruebaTecnica.Models.PruebaTecnicaEntities db = new apiPruebaTecnica.Models.PruebaTecnicaEntities();

        [HttpPost]
        [Route("login")]
        public IHttpActionResult login(AuthModel authModel)
        {
            try
            {
                var person = db.Person.Where(x => x.Email == authModel.email).FirstOrDefault();
                if (person == null) return BadRequest("email not found");
                var user = db.Users.Where(x => x.IdPerson == person.Id).FirstOrDefault();
                string passwordHash = GetHashString(authModel.password);
                if (user.Password == passwordHash) return Ok(person);
                return BadRequest("Wrong Password");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult logout(Users user)
        {
            return Ok("logout");
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
