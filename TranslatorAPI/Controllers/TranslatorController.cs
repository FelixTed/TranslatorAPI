using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslatorAPI.Models;
using TranslatorAPI.Data;

namespace TranslatorAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TranslatorController : ControllerBase
    {
        private readonly ApiContext _context;
        public TranslatorController(ApiContext context)
        {
            _context = context;
        }

        // Post or update
        [HttpPost]
        public JsonResult CreateEdit(User user)
        {
            if (user.Id == 0)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return new JsonResult(new { message = "User created successfully", user });
            }
            else
            {
                var existingUser = _context.Users.Find(user.Id);
                if(existingUser == null)
                {
                    return new JsonResult(NotFound());
                }
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                
                _context.SaveChanges();
            }
            return new JsonResult(Ok(user));
        }

        // Get a single user
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Users.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Users.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Users.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Users.ToList();
            return new JsonResult(Ok(result));
        }
    }
}
