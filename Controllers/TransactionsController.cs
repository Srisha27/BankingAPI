using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Transactions;
using WebAPIProject.Domain.Context;
using WebAPIProject.Domain.Enum;
using WebAPIProject.Domain.Models;
using WebAPIProject.DTO;
using WebAPIProject.Service.Interface;
using WebAPIProject.Service.Mapper;
using Transaction = WebAPIProject.Domain.Models.Transaction;

namespace WebAPIProject.Controllers
{
   //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
      //  private readonly BankDb _context;
        private readonly ITransactionService _transactions;
        private readonly IMapper _mapper;
        //private int transactionId;

        public TransactionsController(ITransactionService transactions, IMapper mapper)
        {
           // _context = context;
            _transactions = transactions;
            _mapper = mapper;

        }

        /// <summary>
        /// Get All Transaction Details
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <response code ="200">Get By ID Transactions </response>
        [HttpGet("GetAllTransactions")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<List<Transaction>>> GetAllTransaction()
        {
            try
            {
                var trans = await _transactions.GetAllTransactionsAsync();
                if (trans.Count == 0)
                {
                    return NoContent();
                }
                return Ok(trans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


            /// <summary>
            /// Get All Transaction Details
            /// </summary>
            /// <param name="AccountNumber"></param>
            /// <response code ="200">Get By ID Transactions </response>
         [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<List<Transaction>>> GetByIDTransactions(long AccountNumber)
        {
            try
            {
                var Transaction = await _transactions.GetTransactionsByIdAsync(AccountNumber); 
                if (Transaction == null) 
                {
                    
                    return NoContent();
                }
                return Ok(Transaction);
               // return Ok(await _transactions.GetTransactionsByIdAsync(AccountNumber));
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex);
            }

        }
        /// <summary>
        /// Generate the Transactions
        /// </summary>
        /// <param name="transactionDTO"></param>
        /// <param name="type"></param>
        /// <response code ="200">Created Transactions to be shown</response>

        [HttpPost("CreateTransaction")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]

        public async Task<ActionResult> DoTransaction(TransactionDto transactionDTO, TransType type)
        {
            try
            {
                var res = await _transactions.DoTransaction(_mapper.Map<TransactionMapping>(transactionDTO), type);
                return Ok("Successful Transaction");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex);
            }


        }
        /// <summary>
        /// It to be given Recent five Transactions
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <response code ="200">Recent 5 Transactions will be appear</response>

        [HttpGet("Last5TransactionId")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<List<Transaction>>> Get5Transactions(long AccountNumber)
        {
            try
            {
                //var list = await _context.Customers.FindAsync(AccountNumber);
                //if (list == null)
                //    return NotFound();
                //var Transaction = await _context.Transactions.Where(x => x.AccountNumber == AccountNumber).OrderByDescending(y => y.TransactionId).Take(5).ToListAsync();
                //return Ok(Transaction);

                var Transaction = await _transactions.Get5Transactions(AccountNumber);
                if (Transaction == null)
                {

                    return NoContent();
                }
                return Ok(Transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex);
            }

        }


        /// <summary>
        /// It will be search the Transaction Date
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="Fromdate"></param>
        /// <param name="Todate"></param>
        /// <response code ="200">Search Transaction By Date</response>


        [HttpGet("SearchTransactionDate")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<List<Transaction>>> GetByTransactionDate(long AccountNumber, DateTime Fromdate, DateTime Todate)
        {
            try
            {
                var Transaction = await _transactions.GetByTransactionDate(AccountNumber, Fromdate, Todate);

                if (Transaction == null)
                {

                    return NoContent();
                }
                return Ok(Transaction);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }



    }
}

