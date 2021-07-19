using DocumentGenerator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CPFController : ControllerBase
    {
        private readonly IDocumentService _cpfService;

        public CPFController(CPFService cpfService)
        {
            _cpfService = cpfService;
        }

        [HttpGet]
        [Route("isvalid/{cpf?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetIsValidAsync([FromRoute] string cpf)
        {
            if(cpf == null) NotFound();
            try
            {
                var isValid = await _cpfService.IsValid(cpf);
                return Ok(isValid);
            }
            catch(ArgumentException ex)
            {
                //return StatusCode(500, ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> GetCreateCPFAsync()
        {
            var cpf = await _cpfService.Create();
            return Ok(cpf);
        }

        [HttpGet]
        [Route("createlist/{lenght:int?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        public async Task<IActionResult> GetCreateListAsync([FromRoute] int? lenght = null)
        {
            if (lenght == null) lenght = 100;

            var cpfs = await _cpfService.CreateList((int)lenght);
            return Ok(cpfs);
        }
    }
}
