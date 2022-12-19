using ESchool.SignalR.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESchool.SignalR
{
    public class Startup
    {
        public static string _connection;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSignalRCore();
            //services.AddCors(options =>
            //{

            //    options.AddPolicy("EnableCors",
            //    builder =>
            //    builder.WithOrigins("http://localhost:4309")
            //    .AllowCredentials()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader());
            //});
            services.AddCors(options =>
            {

                options.AddPolicy("EnableCors",
                    builder =>
                    //builder.SetIsOriginAllowed(_ => true)
                    builder.WithOrigins("https://www.hesabischool.com", "https://hesabischool.com")
                    .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            _connection = Configuration.GetConnectionString("DefaultConnectionString");

            ////services.AddDbContext<ESchoolContext>(options =>
            ////{
            ////    string con = Configuration.GetConnectionString("DefaultConnectionString");
            ////    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            ////}
            ////);

            ////#region SignalR

            //////services.AddSingleton<IRoomChatService, RoomChatService>();
            ////services.AddTransient<IRoomChatService, RoomChatService>();

            ////#endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("EnableCors");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatGroupHub>("/chatGroupHub");
            });

        }

    }
}


