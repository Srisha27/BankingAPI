using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Domain.Enum;
using WebAPIProject.Service.Mapper;
using WebAPIProject.Domain.Models;
using WebAPIProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using WebAPIProject.Domain.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using Microsoft.Identity.Client;
using Castle.Core.Resource;

namespace WebAPIProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customers;
        private readonly IMapper _mapper;
        //private readonly BankDb _context;

        public CustomerController(ICustomerService customers , IMapper mapper   )
        {


            _customers = customers;
            _mapper = mapper;
            
        }

       
        /// <summary>
        /// To be given all customers details
        /// </summary>
       
        /// <response code ="200">Get all Customers Details.</response>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]

        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
       
        public async Task<ActionResult<List<CustomerMapping>>> GetAllCustomers()
        {
            try
            {
                var customer = await _customers.GetAllCustomersAsync();
                //var Mapper = _mapper.Map<List<CustomerDTO>>(customer);
                if (customer.Count == 0)
                {
                    return NoContent();
                }

                return Ok(customer);

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
            
        }
        /// <summary>
        /// Given the AccountNumber and get the Customer Details
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <response code ="200">Response will be get by Customer Id</response>

        [HttpGet("AccountNumber")]

        [SwaggerResponse(StatusCodes.Status200OK)]
      
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        
        //[HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomerId(long AccountNumber)
        {
            try
            {
                var customer = await _customers.GetCustomerByIdAsync(AccountNumber);
                if (customer == null)
                {
                    return NoContent();
                }

                return Ok(customer);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
            
        }
        #region Post

        /// <summary>
        /// It will create the customer details
        /// </summary>
        /// <param name="customer"></param>
       
        /// <returns></returns>
        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            try {
                if (string.IsNullOrEmpty(customer.AccountHolder))
                    return Problem(statusCode: 400, detail: $"Name should not be empty");

                //if (customer.Email == null)
                //    return Problem(statusCode: 400, detail: $"Please enter a valid Email");
                //if (customer.Password == null)
                //    return Problem(statusCode: 400, detail: $"Please enter a valid Password");
                //if (customer.PhoneNumber == null)
                //    return Problem(statusCode: 400, detail: $"Please enter a valid PhoneNumber");
                //if (customer.CurrentBalnce == null)
                //    return Problem(statusCode: 400, detail: $"Please enter a valid Current Balance");
               
                return Ok(await _customers.CreateCustomerAsync(customer));
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex);
            }
            
            
        }
        #endregion
        /// <summary>
        /// Update the Bank details
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="customer"></param>
        
        /// <response code ="200">Update the Account Details.</response>

        [HttpPut("UpdateAccountdetails")]
        
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<Customer>> UpdateCustomer(long AccountNumber, Customer customer)
        {
            //try { }
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            //try 
            //{
            //    if (string.IsNullOrEmpty(customer.AccountHolder))
            //        return Problem(statusCode: 400, detail: $"Name should not be empty");

            //if (customer.AccountNumber == AccountNumber)
            //    return Problem(statusCode: 400, detail: $"Please enter a valid Account Number");
            //if(customer.Email == null)
            //    return Problem(statusCode: 400, detail: $"Please enter a valid Email");
            //if(customer.Password == null)
            //    return Problem(statusCode: 400, detail: $"Please enter a valid Password");
            //if (customer.PhoneNumber == null)
            //    return Problem(statusCode: 400, detail: $"Please enter a valid PhoneNumber");
            //if (customer.CurrentBalnce == null)
            //    return Problem(statusCode: 400, detail: $"Please enter a valid Current Balance");

            // var details =   await _customers.UpdateCustomerAsync(AccountNumber, customer);
            //    return Ok("Update Successfully");

            //}
            try
            {
                if (customer.AccountHolder == null)
                {
                    return Problem(statusCode: 400, detail: $"Name should not be empty");
                }
                var details = await _customers.UpdateCustomerAsync(AccountNumber,customer);
               
                return Ok("update Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            

        }
        /// <summary>
        /// Delete the Customer Details by Account Number
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <response code ="200">Delete the Customer Details by Id</response>
        [HttpDelete("DeleteAccount")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<IActionResult> DeleteCustomer(long AccountNumber)
        {
            try
            {
                var customer = await _customers.DeleteCustomerAsync(AccountNumber);
                if (customer == null)
                {
                    return Problem(statusCode: 400, detail: $"unable to delete customer: {AccountNumber}");
                }
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        /// <summary>
        /// Show page records
        /// </summary>
        /// <param name="PageNo"></param>
        /// <param name="AccountHolder"></param>
        /// <response code ="200">show particular page.</response>
        //[HttpGet("{PageNo}")]

        //public async Task<ActionResult<List<Customer>>> GetCustomer(int PageNo, string? sort_by, Sorting sort_type, string? AccountHolder = "")
        //{
        //    IQueryable<Customer> query = _context.Customers;
        //    switch (sort_by)
        //    {
        //        case "Sorting":
        //            if (sort_type == Sorting.ascending)
        //            {
        //                query = query.OrderBy(c => c.AccountHolder);

        //            }
        //            else
        //            {
        //                query = query.OrderByDescending(c => c.AccountHolder);
        //            }

        //            break;


        //            //do more


        //    }
           

        //    var customer = await _context.Customers.ToListAsync();
        //    if (AccountHolder != "")
        //    {
        //        customer = await _context.Customers
        //        .Where(b => b.AccountHolder.Contains(AccountHolder))
        //        .ToListAsync();
        //    }
        //    var pageResults = 4f;
        //    var pageCount = Math.Ceiling(customer.Count() / pageResults); var CustomerDetails = customer.Skip((PageNo - 1) * (int)pageResults)
        //    .Take((int)pageResults)
        //    .ToList(); var pagination = new CustomerPaging
        //    {
        //        Customer = CustomerDetails,
        //        CurrentPage = PageNo,
        //        Pages = (int)pageCount,
        //    };
        //    return Ok(pagination);
        //}
        

    }
}



