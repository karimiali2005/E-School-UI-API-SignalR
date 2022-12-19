using System.IO;

namespace ESchool.Api.Need
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

            return SettingContext.PathStoreFiles.Instance.Path1;


        }
        

       
    }

}
