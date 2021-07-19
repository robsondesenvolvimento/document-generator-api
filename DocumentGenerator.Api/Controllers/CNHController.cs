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
    public class CNHController : ControllerBase
    {
        private readonly IDocumentService _cnhService;

        public CNHController(CNHService cnhService)
        {
            _cnhService = cnhService;
        }

        [HttpGet]
        [Route("isvalid/{cnh}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetIsValidAsync([FromRoute] string? cnh)
        {
            if (cnh == null) NotFound();
            try
            {
                var isValid = await _cnhService.IsValid(cnh);
                return Ok(isValid);
            }
            catch (ArgumentException ex)
            {
                //return StatusCode(500, ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> GetCreateCNHAsync()
        {
            var cpf = await _cnhService.Create();
            return Ok(cpf);
        }

        [HttpGet]
        [Route("createlist/{lenght:int?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        public async Task<IActionResult> GetCreateListAsync([FromRoute] int? lenght = null)
        {
            if (lenght == null) lenght = 100;

            var cnhs = await _cnhService.CreateList((int)lenght);
            return Ok(cnhs);
        }
    }
}
