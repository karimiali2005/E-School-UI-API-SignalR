using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Net;

namespace ESchool.Api.Need
{
    public class Codes
    {
        public static string GetPathAndFilename(string filename)
        {
            return AppSettingClass.GetPathStoreFiles() + filename;
        }
        public static string GetMimeType(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return "";

            filename = GetPathAndFilename(filename);
            string contentType = "";
            new FileExtensionContentTypeProvider().TryGetContentType(filename, out contentType);
            return contentType;
        }

        public static bool CopyFile(string addressCopy, string addressPaste, string fileName, string FileToCopy, string userName, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(addressCopy + fileName);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;

                    request.Credentials = new NetworkCredential(userName, password);
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    Upload(addressPaste + FileToCopy, ToByteArray(responseStream), userName, password);
                    responseStream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return false;
        }

        public static Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                ms.Write(chunk, 0, bytesRead);
            }

            return ms.ToArray();
        }

        public static bool Upload(string FileName, byte[] Image, string FtpUsername, string FtpPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    System.Net.FtpWebRequest clsRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(FileName);
                    clsRequest.Credentials = new System.Net.NetworkCredential(FtpUsername, FtpPassword);
                    clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                    System.IO.Stream clsStream = clsRequest.GetRequestStream();
                    clsStream.Write(Image, 0, Image.Length);

                    clsStream.Close();
                    clsStream.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return false;

        }

        public static bool DeleteFile(string addressCopy, string fileName, string userName, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(addressCopy + fileName);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;

                    request.Credentials = new NetworkCredential(userName, password);
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    response.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return false;
        }


        public static string GetRandomFileName(string filename)
        {
            string extension = System.IO.Path.GetExtension(filename);
            string newName = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            return newName + extension;
        }
        public static string GetPathStoreProfileImage(string fileName, bool isThumb = true)
        {
            if (string.IsNullOrEmpty(fileName) || fileName == "null")
            {
                return Path.GetFullPath("wwwroot/imagemember/picstucdent.png");
            }
            else
            {
                string path = SettingContext.PathStoreProfileImage.Instance.Path1;
                if (isThumb)
                    return path + "ImageUserThumb\\" + fileName;
                else
                    return path + "ImageUser\\" + fileName;
            }

        }
        public static byte[] filePicDefault = null;
        public static byte[] ConvertImageToArray(string fn, bool isThumb = true)
        {


            if (string.IsNullOrEmpty(fn))
            {
                if (filePicDefault == null)
                {
                    var fullpath = Path.GetFullPath("wwwroot/imagemember/picstucdent.png");
                    FileStream stream = File.OpenRead(fullpath);
                    byte[] fileBytes = new byte[stream.Length];

                    stream.Read(fileBytes, 0, fileBytes.Length);
                    stream.Close();
                    filePicDefault = fileBytes;
                }

                return filePicDefault;
            }
            else
            {
                var fullpath = Codes.GetPathStoreProfileImage(fn, isThumb);
                FileStream stream = File.OpenRead(fullpath);
                byte[] fileBytes = new byte[stream.Length];

                stream.Read(fileBytes, 0, fileBytes.Length);
                stream.Close();
                return fileBytes;
            }


        }
    }
}
