using API.Contexts;
using API.Repositories;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyContext _context;
        public IConfiguration _configuration;
        UserRepo _repo;

        public UsersController(MyContext myContext, IConfiguration config, UserRepo repo)
        {
            _context = myContext;
            _configuration = config;
            _repo = repo;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<GetUserVM>> Get()
        {
            var data = await _repo.getAll();
            return data;
        }

        [HttpGet("{id}")]
        public GetUserVM Get(int id)
        {
            var data = _repo.getId(id);
            return data;
        }

        [HttpPost]
        public IActionResult Create(GetUserVM getUserVM)
        {
            var data = _repo.Create(getUserVM);
            if (data == true)
            {
                return Ok(new { msg = "Successfully Created" });
            }
            return BadRequest(new { msg = "Not Success" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(GetUserVM getUserVM, int id)
        {
            var data = _repo.Update(getUserVM, id);
            if (data == true)
            {
                return Ok(new { msg = "Successfully Updated" });
            }
            return BadRequest(new { msg = "Not Success" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _repo.Delete(id);
            if (data == true)
            {
                return Ok(new { msg = "Successfully Deleted" });
            }
            return BadRequest(new { msg = "Not Success" });
        }
    }
}
