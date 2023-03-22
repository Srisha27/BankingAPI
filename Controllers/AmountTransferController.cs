using AutoMapper;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Domain.Enum;
using WebAPIProject.Domain.Models;
using WebAPIProject.DTO;
using WebAPIProject.Service.Interface;
using WebAPIProject.Service.Mapper;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmountTransferController : ControllerBase
    {
        private readonly IAmountTransferService _trans;
        private readonly IMapper _mapper;
        private IAmountTransferService @object;

        public AmountTransferController(IAmountTransferService trans, IMapper mapper)
        {
            _trans = trans;
            _mapper = mapper;
        }

        //public AmountTransferController(IAmountTransferService @object)
        //{
        //    this.@object = @object;
        //}

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<AmountTransferDto>>> GetAll()
        {
            try
            {
                var customer = await _trans.GetAllAmountTransactionsAsync();
                //var Mapper = _mapper.Map<AmountTransferMapping>(customer);
                if (customer.Count==0)
                {
                    return NoContent();
                }

                return Ok(customer);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPost("DoTransferAmount")]

        public async Task<ActionResult> DoTransferAmount(AmountTransferDto amountTransfer, Transfer transType)
        {
            try
            {
                var trans = await _trans.TranferAmountAsync(_mapper.Map<AmountTransferMapping>(amountTransfer), transType);
                return Ok("Transaction Successfull");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetRecent5Transaction")]
        public async Task<ActionResult<List<AmountTransfer>>> GetRecent5Transaction(long AccountNumber)
        {
            try
            {
                var Transaction = await _trans.GetRecentTransactionAsync(AccountNumber);
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

        [HttpGet("GetByTransactionDate")]

        public async Task<ActionResult<List<AmountTransfer>>> GetByTransactionDate(long AccountNumber, DateTime Fromdate, DateTime Todate)
        {
            try
            {
                var Transaction = await _trans.GetByDateTransactionAsync(AccountNumber, Fromdate, Todate);

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
