using System.Linq;
using System.Threading.Tasks;
using ESchool.Need;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class ReportCardStudentController : Controller
    {
        private readonly IReportCardService _reportCardService;

        public ReportCardStudentController(IReportCardService reportCardService)
        {
            _reportCardService = reportCardService;
        }
        public async Task<IActionResult> Index(string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

            var reportCards = await _reportCardService.ReportCardStudentShow(userId);

            return View(reportCards);

        }


        public async Task<IActionResult> Detail(int id,string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

            var reportCardDetail = await _reportCardService.ReportCardStudentDetailShow(id,userId);

            return View(reportCardDetail);
            

        }

    }
}