using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http;

namespace ESchool.Need
{
  
    public static class SettingContext
    {
        public class ConnectionStrings
        {
            private ConnectionStrings()
            {
            }

            // enable set in singleton property instance to work correctly.
            public static ConnectionStrings Instance { get; set; } = new ConnectionStrings();

            public string DefaultConnectionString { get; set; }
            public string DefaultConnectionString0 { get; set; }
        }

        public class RoomChatSatus
        {
            private RoomChatSatus()
            {
            }
            public static RoomChatSatus Instance { get; set; } = new RoomChatSatus();
            public string CloseChatStudent { get; set; }
            public string CloseChatAll { get; set; }
            public string ChatOffline { get; set; }
            public string ChatDeleteTime { get; set; }
        }

        public class AdminPanel
        {
            private AdminPanel()
            {
            }
            public static AdminPanel Instance { get; set; } = new AdminPanel();
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class PathStoreFiles
        {
            private PathStoreFiles()
            {
            }
            public static PathStoreFiles Instance { get; set; } = new PathStoreFiles();
            public string Path1 { get; set; }
            public string Path2 { get; set; }
            public string Host { get; set; }
            public string Address { get; set; }
            public string UserId { get; set; }
            public string Password { get; set; }
            public string NormalFile { get; set; }
            public string LearnFile { get; set; }
            public string HomeworkFile { get; set; }
            public string ExamFile { get; set; }
            public string ResponseExamFile { get; set; }
            public string PreRegistrationPic { get; set; }
            public string Gallery { get; set; }
            public string Article { get; set; }
            public string Introduction { get; set; }
            public string ReportCard { get; set; }

        }
        public class UploadSize
        {
            private UploadSize()
            {
            }
            public static UploadSize Instance { get; set; } = new UploadSize();
            public string StudentSize { get; set; }
            public string TeacherSize { get; set; }
            public string HomeworkSize { get; set; }
            public string ReportCardSize { get; set; }
        }
        public class PathStoreProfileImage
        {
            private PathStoreProfileImage()
            {
            }
            public static PathStoreProfileImage Instance { get; set; } = new PathStoreProfileImage();
            public string Path1 { get; set; }
            public string Path2 { get; set; }
        }

    }

}
