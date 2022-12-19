using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESchool.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(AuthenticationSchemes = "AdminAuth")]
    public class StatisController : Controller
    {
        private readonly IPreRegistrationService _preRegistrationService;

        public StatisController(IPreRegistrationService preRegistrationService)
        {
            _preRegistrationService = preRegistrationService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _preRegistrationService.StatisticsShow();


            return View(result);
        }
    }
}