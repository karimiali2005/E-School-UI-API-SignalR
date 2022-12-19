using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ElmahCore;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersioningController : ControllerBase
    {
        private readonly IVersioningService _versioningService;


        public VersioningController(IVersioningService versioningService)
        {
            _versioningService = versioningService;

        }
        [HttpGet("GetLoadVersion")]
        public async Task<IActionResult> GetLoadVersion()
        {
            try
            {
                var versioning = await _versioningService.GetLoadVersion();

                var result = new ObjectResult(versioning)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                return Ok(result);

            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                return BadRequest(new { message = ex.Message });
            }


        }
    }
}