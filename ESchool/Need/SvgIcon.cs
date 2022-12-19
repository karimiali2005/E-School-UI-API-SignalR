using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.Need
{
    public class SvgIcon
    {
        public static string GetSvg(string filepath)
        {
            try
            {
                string fullpath = filepath.StartsWith("wwwroot") || filepath.StartsWith("/wwwroot") ? System.IO.Path.GetFullPath(filepath) : System.IO.Path.GetFullPath("wwwroot/" + filepath);
                return System.IO.File.ReadAllText(fullpath);
            }
            catch(Exception ex)
            {
                return "";
            }         
        }
    }
}
