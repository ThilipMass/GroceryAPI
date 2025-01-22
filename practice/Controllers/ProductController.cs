
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
    public class HandleProductController: ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        // Public constructor to inject ApplicationDbContext
        public HandleProductController(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        
        }

        // Get all hotel staff details
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _dbContext.ProductDetails.ToListAsync();
            
            // Check if the list is empty or null
            if (products == null || !products.Any())
                return BadRequest("No details found.");
            
            return Ok(products);
        }

        //Register a new product
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Register([FromBody]ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Invalid Product details.");
            }

            // Check if the product already exists
            var isProductExists = await _dbContext.ProductDetails
                .Where(x => x.ProductName == product.ProductName)
                .FirstOrDefaultAsync();

            if (isProductExists == null)
            {
                
              var _product =  _mapper.Map<Products>(product);
            //   _user.CustomerID=
                // Add the new hotel staff to the database
                await _dbContext.ProductDetails.AddAsync(_product);
                await _dbContext.SaveChangesAsync();

                return Ok("Product added successful");
            }

            return BadRequest("Product already exists.");
        }
        // [HttpGet("{UserName}")]
        // public async Task<IActionResult> GetByUserName(string UserName)
        // {
        //     if(UserName==null)
        //         return BadRequest("Please Enter the user name.");
        //     Products products = await _dbContext.ProductDetails.FindAsync(ProductID);

        //     if(products == null)
        //         return NoContent();
        //     return Ok(products);
        // }
    
        [HttpPut("{ProductID}")]
        public async Task<IActionResult> Update(int ProductID,Products products)
        {
            if(ProductID==0)
                return BadRequest("Please Provide valid Product ID...");
            var product = await _dbContext.ProductDetails.FindAsync(ProductID);
             _dbContext.Entry(product).State = EntityState.Detached;
            if(product!=null)
            {
                _dbContext.ProductDetails.Update(products);
                await _dbContext.SaveChangesAsync();
            }
            return NoContent();
        }

        [HttpDelete("{ProductID}")]
        public async Task<IActionResult> Delete(int ProductID)
        {
            if(ProductID == 0)
                return BadRequest("Please Provide Product ID...");
            var product = await _dbContext.ProductDetails.FindAsync(ProductID);
             _dbContext.Entry(product).State = EntityState.Detached;
            if(!(ProductID==0))
            {
               
                _dbContext.ProductDetails.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
