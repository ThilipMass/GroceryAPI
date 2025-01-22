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
    public class HandleBookingController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        // Public constructor to inject ApplicationDbContext
        public HandleBookingController(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }

        // Get all booking details
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookingDetails = await _dbContext.BookingDetail.ToListAsync();
            
            // Check if the list is empty or null
            if (bookingDetails == null || !bookingDetails.Any())
                return BadRequest("No details found.");
            
            return Ok(bookingDetails);
        }

        // Register a new hotel staff member
        [HttpPost]
        public async Task<ActionResult<BookingDetail>> Register([FromBody]BookingDetail details)
        {
            if (details == null)
            {
                return BadRequest("Invalid booking details.");
            }
           // Check if the user already exists
            var isBookingIDExists = await _dbContext.BookingDetail
                .Where(x => x.BookingID == details.BookingID)
                .FirstOrDefaultAsync();

            if (isBookingIDExists == null)
            {
                
             // var _user =  _mapper.Map<User>(user);
            //   _user.CustomerID=
                // Add the new hotel staff to the database
                await _dbContext.BookingDetail.AddAsync(details);
                await _dbContext.SaveChangesAsync();

                return Ok("Booking successful" );
            }

            return BadRequest("Booking ID already exists.");
        }
        [HttpDelete("{BookingID}")]
        public async Task<IActionResult> Delete(int BookingID)
        {
            if(BookingID==0)
                return BadRequest("Please Provide Booking ID...");
            var bookings = await _dbContext.BookingDetail.FindAsync(BookingID);
             _dbContext.Entry(bookings).State = EntityState.Detached;
            if(BookingID!=0)
            {
               
                _dbContext.BookingDetail.Remove(bookings);
                await _dbContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
