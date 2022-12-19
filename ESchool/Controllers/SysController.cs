using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Controllers
{
    public class SysController : Controller
    {
        public FileResult JS()
        {
            string content = "";
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jssite/main.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jssite/reg.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jssite/login.js"));            

            return GetFile(content, "text/javascript");
        }


        public FileResult CSS()
        {
            string content = "";
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csssite/main.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csssite/lds_roller.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csssite/top.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csssite/footer.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csssite/form.css"));            

            return GetFile(content, "text/css");
        }


        public FileResult JSUser()
        {
            string content = "";
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jssite/main.js"));                     
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jssite/reg.js"));                     
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jssite/login.js"));                     
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jsuser/account.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jsuser/chat.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jsuser/jquery.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jsuser/chatfile.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/slidemenu.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/form.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/grid.js"));

            return GetFile(content, "text/javascript");
        }


        public FileResult CSSUser()
        {
            string content = "";
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/main.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/alertmessage.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/header.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/footer.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/lds_roller.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/container.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jssite/reg.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/form.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/grid.css"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/chat.css"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cssuser/chatusers.css"));            

            return GetFile(content, "text/css");
        }


        public FileResult JSPanel()
        {
            string content = "";
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jssite/main.js"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/slidemenu.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/form.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/grid.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/login.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/chat.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanel.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelStudent.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelParent.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelTeacher.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelCourse.js"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelHoliday.js"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelRoom.js"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelRoomDetail.js"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPreRegistration.js"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelGallery.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelArticle.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelIntroduction.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelReportCard.js"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/jspanel/sPanelCeremony.js"));

            return GetFile(content, "text/javascript");
        }


        public FileResult CSSPanel()
        {
            string content = "";
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/main.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/alertmessage.css"));
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/header.css"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/footer.css"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/lds_roller.css"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/container.css"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/form.css"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/grid.css"));            
            content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/csspanel/list.css"));                                

            return GetFile(content, "text/css");
        }

        public FileResult JSCropper(int id = 1)
        {
            string content = "";
            switch (id)
            {
                case 1:
                    content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cropper/cropper.js"));
                    break;
                case 2:
                    content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cropper/croppr.js"));
                    break;
            }
            return GetFile(content, "text/javascript");
        }


        public FileResult CSSCropper(int id = 1)
        {
            string content = "";
            switch(id)
            {
                case 1:
                    content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cropper/cropper.css"));
                    break;
                case 2:
                    content += System.IO.File.ReadAllText(Path.GetFullPath("wwwroot/cropper/croppr.css"));
                    break;
            }
            return GetFile(content, "text/css");
        }


        private FileResult GetFile(string body, string type = "text/css")
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(body);
            MemoryStream stream = new MemoryStream(byteArray);
            return File(stream, type);
        }
    }
}