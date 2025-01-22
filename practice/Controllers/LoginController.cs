using AutoMapper;
using DBConnection;
using Microsoft.AspNetCore.Mvc;  // Ensure you are using the right namespace for MVC
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using practice.models;
using System.Linq;  // Add the missing using directive for LINQ

namespace RegisterHandler
{
    [Route("api/[controller]")]  // Fully qualify the Route attribute from MVC
    [ApiController]
    public class HandleLoginController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IMapper _mapper;

        // Public constructor to inject ApplicationDbContext
        public HandleLoginController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
          //  _mapper=mapper;
        }

        // Register a new hotel staff member
        [HttpPost]
        public async Task<ActionResult<Login>> ValidateUser([FromBody]Login userLogin)
        {
            if (userLogin == null)
            {
                return BadRequest("Invalid staff details.");
            }

            // Check if the user already exists
            var isUserExists = await _dbContext.UserDetails
                .FirstOrDefaultAsync(user=>user.Name.ToLower()==userLogin.Name.ToLower() && user.PhoneNumber == userLogin.PhoneNumber);

            if (isUserExists!=null)
            {
                return Ok("Login successful" );
            }

            return BadRequest("Invalid User credientials");
        }
    }
}
