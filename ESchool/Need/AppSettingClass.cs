using System.IO;

namespace ESchool.Need
{
    public class AppSettingClass
    {
      
        public static bool Login(string username, string password)
        {
            return (SettingContext.AdminPanel.Instance.Username == username &&
                   SettingContext.AdminPanel.Instance.Password == password);
        }

        public static string GetPathStoreFiles()
        {
            string path = Codes.GetDomainName().ToLower().Contains("localhost")
                ? SettingContext.PathStoreFiles.Instance.Path1
                : SettingContext.PathStoreFiles.Instance.Path2;
            return path;
           
        }
        

       
    }

}
