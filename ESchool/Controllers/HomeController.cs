using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.AspNetCore.Http;

using System.IO;
using ESchool.Need;
using System.Net.Http.Headers;
using System.Net;
using System.Web;
using Es.DataLayerCore.DTOs.Home;
using Es.DataLayerCore.DTOs.Article;

namespace ESchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Article = await _homeService.ArticleShowTop();

            return View(homeViewModel);
        }

        //public async Task<IActionResult> Contact()
        //{
        //    return View();
        //}

        public async Task<IActionResult> PreRegistration()
        {
            return View();
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            return filename;
        }

        [HttpPost]
        public async Task<IActionResult> PreRegistration(IFormFile pic,string classRoom, string irnational, string firstName, string lastName, string fatherName, string fatherNumber, string motherFullName, string motherNumber)
        {

            try
            {
                var result = await _homeService.PreRegistrationExists(irnational);
                string savefilename = "";
                if (result == false)
                {


                    if (pic != null)
                    {
                        var address = SettingContext.PathStoreFiles.Instance.Address;
                        var fullAddressFtp = address;


                        fullAddressFtp += SettingContext.PathStoreFiles.Instance.PreRegistrationPic;




                        string filename = ContentDispositionHeaderValue.Parse(pic.ContentDisposition).FileName.Trim('"');
                        filename = this.EnsureCorrectFilename(filename);

                        savefilename = Codes.GetRandomFileName(filename);
                        var requestUri = fullAddressFtp + savefilename;



                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential(SettingContext.PathStoreFiles.Instance.UserId, SettingContext.PathStoreFiles.Instance.Password);
                        // request.UsePassive is true by default.




                        /*string savefilename = Codes.GetFileName(filename);
                        using (var output = new FileStream(Codes.GetPathAndFilename(savefilename), FileMode.Create))
                        {
                            await file.CopyToAsync(output);
                        }*/
                        using (Stream requestStream = request.GetRequestStream())
                        {
                            await pic.CopyToAsync(requestStream);
                        }
                    }


                    var preRegistration = new PreRegistration()
                    {
                        ClassRoom=classRoom,
                        Irnational = irnational,
                        FirstName = firstName,
                        LastName = lastName,
                        FatherName = fatherName,
                        FatherNumber = fatherNumber,
                        MotherFullName = motherFullName,
                        MotherNumber = motherNumber,
                        Pic = savefilename,

                    };
                    await _homeService.PreRegistrationInsert(preRegistration);
                    return Json(new { resultMessage = "دانش اموز گرامی ثبت نام شما تکمیل شد" });
                }
                else
                {
                    return Json(new { resultMessage = "دانش اموز گرامی قبلا شما ثبت نام کردید" });
                }



            }
            catch (Exception ex)
            {
                return Json(new { resultMessage = "در ثبت اطلاعات مشکلی وجود دارد لطفا دوباره امتحان کنید" });

            }


        }

        public async Task<IActionResult> About()
        {
            return View();
        }


        public async Task<IActionResult> Gallery()
        {
            var result =await  _homeService.GalleryShowAll();
            return View(result);
        }

        public async Task<IActionResult> Gallery_Detail(int id)
        {
            var result =await _homeService.GalleryDetailShow(id);
            return View(result);
        }

        public async Task<IActionResult> Article_Detail(int id)
        {
            await _homeService.ArticleCountorIncrease(id);
            var result = new ArticleViewModel
            {
                Article = await _homeService.ArticleGetById(id),
                ArticleShowTops = await _homeService.ArticleShowTop()
            };
            return View(result);
        }

        public async Task<IActionResult> IntroductionGroup(int id)
        {
            var result = await _homeService.IntroductionGroupShow(id);
            return View(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> Contact(int teacherId, string teacherTitle, string picName, string picNameShort)
        //{
        //    //try
        //    //{
        //    //    var userTypeId = UserContext.GetClaimValueInteger(ClaimName.UserTypeId);
        //    //    var userId = UserContext.GetClaimValueInteger(ClaimName.UserId);

        //    //    var roomChatGroup = new RoomChatGroup()
        //    //    {
        //    //        RoomChatGroupCreateDateTime = DateTime.Now,
        //    //        RoomChatGroupDelete = false,
        //    //        RoomChatGroupVisible = true,
        //    //        RoomChatGroupTitle = teacherTitle,
        //    //        TeacherId = (userTypeId == 1 ? teacherId : userId),
        //    //        StudentId = (userTypeId == 1 ? userId : teacherId),
        //    //        RoomChatGroupType = 2

        //    //    };

        //    //    await _roomChatService.RoomChatGroupInsert(roomChatGroup);






        //    //    return Json(new { roomChatGroupModel = roomChatGroup });
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    ex.ToSaveElmah();
        //    //    return View("Error");
        //    //}
        //    return View();

        //}

    }


    
}
