using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using WebAPIProject.Domain.Models;
using WebAPIProject.DTO;
using WebAPIProject.Service.Interface;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private readonly ICustomerService _trans;
        //private readonly IMapper _mapper;
        //private IAmountTransferService @object;

        //public ValuesController(IAmountTransferService trans, IMapper mapper)
        //{
        //    _trans = trans;
        //    _mapper = mapper;
        //}



        [HttpGet("GetAll")]
        //public async Task<ActionResult<List<Customer>>> GetAll()
        //{
        public async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();



            var response = await httpClient.GetAsync("https://localhost:7038/api/Customer");



            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
            }
        }



    }
}
