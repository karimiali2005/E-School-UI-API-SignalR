using Es.DataLayerCore.DTOs.ReportCard;
using ESchool.Need;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.Controllers
{
    [Authorize(AuthenticationSchemes = "AccountAuth")]
    public class ReportCardParentController : Controller
    {
        private readonly IReportCardService _reportCardService;

        public ReportCardParentController(IReportCardService reportCardService)
        {
            _reportCardService = reportCardService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ReportCardParentShow(string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
            var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);

            var students = await _reportCardService.ReportCardParentStudentShow(userId,userTypeId);

            var reportCardParentViewModel = new ReportCardParentViewModel();
            reportCardParentViewModel.ReportCardParentStudentShowResult = students;

            if (students.Count() == 1)
            {
                ViewBag.UserId = students.FirstOrDefault().UserID;
                ViewBag.UserFullName = students.FirstOrDefault().FullName;
                reportCardParentViewModel.ReportCardStudentShowResult = await _reportCardService.ReportCardStudentShow(students.FirstOrDefault().UserID);
            }
            else
            {
                reportCardParentViewModel.ReportCardStudentShowResult = null;
            }
            return View(reportCardParentViewModel);
        }

        public async Task<IActionResult> ReportCardParentDetailShow(int id)
        {
            try
            {
                var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);
                var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
                var students = await _reportCardService.ReportCardParentStudentShow(userId, userTypeId);

                ViewBag.UserId = id;

                if (students.Any(s => s.UserID == id))
                {

                    var reportCards = await _reportCardService.ReportCardStudentShow(id);

                    return View(reportCards);
                }
                else
                    RedirectToAction("RoomChat", "Member");
                return null;
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return View("Error");
            }
        }

        public async Task<IActionResult> ReportCardParentDetailScore(int id,int userId, string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;

            var userId2 = UserContext.GetClaimValueInteger(ClaimName.UserId);
            var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
            var students = await _reportCardService.ReportCardParentStudentShow(userId2, userTypeId);
            if (students.Any(s => s.UserID == userId))
            {
                var reportCardDetail = await _reportCardService.ReportCardStudentDetailShow(id, userId);
                return View(reportCardDetail);
            }
            else
                RedirectToAction("RoomChat", "Member");
            return null;
        }

    }
}