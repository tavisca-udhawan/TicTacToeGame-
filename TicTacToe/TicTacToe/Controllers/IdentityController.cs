using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [Logging]
    [ExceptionHandler]
    public class IdentityController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Welcome to Tic Tac Toe Game" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Repository repository = new Repository();
            string result = repository.getTokken(id);
            return result;
        }

        // POST api/values
        [HttpPost]

        public ActionResult Post([FromBody]User user)
        {
            Repository repository = new Repository();
            bool result = repository.Add(user);
            if (result == true)
                return Ok("You Registered Successfully!\nYour ID is: '" + user.ID + "'");
            return BadRequest("Process has been failed!");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
