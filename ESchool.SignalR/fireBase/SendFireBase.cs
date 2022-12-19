using Es.Server;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.SignalR.fireBase
{
    public static class SendFireBase
    {



        #region ~( Methods )~
        public static async Task<bool> SendNotificationAsync(ChatMessage chatMessage)
        {
            try
            {
                List<string> deviceIds = new List<string>();
                using (SqlConnection sql = new SqlConnection(Startup._connection))
                {
                    using (SqlCommand cmd = new SqlCommand("UserFireBaseShow", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@RoomChatGroupID", chatMessage.groupId));
                        cmd.Parameters.Add(new SqlParameter("@UserID", chatMessage.senderId));


                        await sql.OpenAsync();


                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                deviceIds.Add(reader["TokenFireBase"].ToString());
                                //listToken += reader["TokenFireBase"].ToString() + ",";
                                //listtoken.Add(reader["TokenFireBase"].ToString());
                                //z = reader["TokenFireBase"].ToString();
                            }
                        }

                        //  listToken = listToken.Remove(listToken.Length - 1, 1);
                        // listToken += "]";

                    }
                }

                //  title = "پیغام جدید";
                //  body = body + " ...";
                string webadress = "https://fcm.googleapis.com";
                string senderId = "";
                string serverkey = "";
                string token = "";

                using (var client = new HttpClient())
                {


                    // var setting = _userService.GetSettingFireBase();


                    client.BaseAddress = new Uri(webadress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                        $"key={serverkey}");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={senderId}");


                    var chatjson = JsonConvert.SerializeObject(chatMessage);
                    var jsonlist = JsonConvert.SerializeObject(deviceIds);
                   
                    var datamesg = new
                    {
                        registration_ids = deviceIds,
                       
                        data = new
                        {

                            body = chatjson,
                        },

                        priority = "high"
                    };

                    var json = JsonConvert.SerializeObject(datamesg);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var result = await client.PostAsync("/fcm/send", httpContent);
                    return result.StatusCode.Equals(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
