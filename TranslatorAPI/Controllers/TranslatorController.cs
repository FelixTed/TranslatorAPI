using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslatorAPI.Models;
using TranslatorAPI.Data;

namespace TranslatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslatorController : ControllerBase
    {
        private readonly ApiContext _context;
        public TranslatorController(ApiContext context)
        {
            _context = context;
        }
        [HttpPost]
        public JsonResult CreateEdit(User user)
        {
            if (user.Id == null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return new JsonResult("User created successfully");
            }
            else
            {
                var existingUser = _context.Users.Find(user.Id);
                if(existingUser == null)
                {
                    return new JsonResult(NotFound());
                }
                existingUser = user;
              
            }
            _context.SaveChanges();
            return new JsonResult(Ok(user));
        }
    }
}
