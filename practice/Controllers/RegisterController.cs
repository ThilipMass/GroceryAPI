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
    public class HandleRegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        // Public constructor to inject ApplicationDbContext
        public HandleRegisterController(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }

        // Get all hotel staff details
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = await _dbContext.UserDetails.ToListAsync();
            
            // Check if the list is empty or null
            if (details == null || !details.Any())
                return BadRequest("No details found.");
            
            return Ok(details);
        }

        // Register a new hotel staff member
        [HttpPost]
        public async Task<ActionResult<RegisterUserDTO>> Register([FromBody]RegisterUserDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid staff details.");
            }

            // Check if the user already exists
            var isUserExists = await _dbContext.UserDetails
                .Where(x => x.Name == user.Name)
                .FirstOrDefaultAsync();

            if (isUserExists == null)
            {
                
              var _user =  _mapper.Map<User>(user);
            //   _user.CustomerID=
                // Add the new hotel staff to the database
                await _dbContext.UserDetails.AddAsync(_user);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "Registration successful", staff = user });
            }

            return BadRequest("User name already exists.");
        }
        [HttpGet("{UserName}")]
        public async Task<IActionResult> GetByUserName(string UserName)
        {
            if(UserName==null)
                return BadRequest("Please Enter the user name.");
            User user = await _dbContext.UserDetails.FindAsync(UserName);

            if(user == null)
                return NoContent();
            return Ok(user);
        }
    
        [HttpPut("{UserName}")]
        public async Task<IActionResult> Update(string UserName,User user)
        {
            if(UserName.IsNullOrEmpty())
                return BadRequest("Please Provide User Name...");
            var staff = await _dbContext.UserDetails.FindAsync(UserName);
             _dbContext.Entry(staff).State = EntityState.Detached;
            if(!(staff==null))
            {
                _dbContext.UserDetails.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            return NoContent();
        }

        [HttpDelete("{UserName}")]
        public async Task<IActionResult> Delete(string UserName)
        {
            if(UserName.IsNullOrEmpty())
                return BadRequest("Please Provide User Name...");
            var user = await _dbContext.UserDetails.FindAsync(UserName);
             _dbContext.Entry(user).State = EntityState.Detached;
            if(!(user==null))
            {
               
                _dbContext.UserDetails.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
