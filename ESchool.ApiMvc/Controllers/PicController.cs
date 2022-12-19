using ESchool.ApiMvc.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ESchool.ApiMvc.Controllers
{
    public class PicController : ApiController
    {
        UserService _userService = new UserService();
        public static string GetPathStoreProfileImage(string fileName, bool isThumb = true)
        {
            if (string.IsNullOrEmpty(fileName) || fileName == "null")
            {
                return HttpContext.Current.Server.MapPath("~/picstucdent.png");
            }
            else
            {
                string path = "C:\\ProfileImage\\";
                if (isThumb)
                    return path + "ImageUserThumb\\" + fileName;
                else
                    return path + "ImageUser\\" + fileName;
            }

        }
        [Route("GetUserPic")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetUserPicAsync(int userId)
        {
            try
            {

                var user = await _userService.UserGetByIdAsync(userId);
                var fullpath = "";

                if (string.IsNullOrEmpty(user.userPicName))
                {
                  
                       fullpath =HttpContext.Current.Server.MapPath("~/picstucdent.png");
                      
                  
                }
                else
                {
                     fullpath = GetPathStoreProfileImage(user.userPicName, true);
                 
                }

                FileStream stream = File.OpenRead(fullpath);
                byte[] fileBytes = new byte[stream.Length];

                stream.Read(fileBytes, 0, fileBytes.Length);
                stream.Close();
             
                MemoryStream ms = new MemoryStream(fileBytes);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(ms);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                return response;
            }
            catch (Exception ex)
            {
                
                return null;
            }

        }

        [Route("GetUserPicName")]
     
        public async System.Threading.Tasks.Task<UserPicViewModel> GetUserPicNameAsync(int userId)
        {
            try
            {

                
                var user = await _userService.UserGetByIdAsync(userId);

                var userViewModel = new UserPicViewModel()
                {
                    userID = user.userID,
                    userPicName = user.userPicName
                };
                return userViewModel;

            }
            catch (Exception ex)
            {
             
                return null;
            }

        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
