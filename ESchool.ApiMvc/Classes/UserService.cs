using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ESchool.ApiMvc.Classes
{
    public class UserService
    {
        public  async System.Threading.Tasks.Task<UserPicViewModel> UserGetByIdAsync(int userId)
        {
            var userPicViewModel = new UserPicViewModel();
            var connectionString = ConfigurationManager.ConnectionStrings["ApiMvcContext"].ConnectionString;
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UserGetByID", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserID", userId));


                    await sql.OpenAsync();
                   

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userPicViewModel.userID = (int)reader["UserID"];
                            userPicViewModel.userPicName = reader["PicName"].ToString();
                        }
                    }


                }
            }
            return userPicViewModel;
        }
    }
}