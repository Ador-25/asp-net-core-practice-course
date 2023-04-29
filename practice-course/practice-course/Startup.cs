using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practice_course
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        // services.add comes here
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor(); // test for http reqs
            services.AddMvc(option => option.EnableEndpointRouting = false);

            // AddSingleton() => instance created only once and used throughout the lifetime
            // AddTransient() => a new instance is created every time service is required/ it is requested
            // AddScoped() => a new instance of scope service is created once per req(one per http request often)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.



        //app.use comes here

        // middlewares used here
        // has access to both incoming req & outgoing res
        // may simply pass it to next comp=> short circuiting 
        // may do some process & then pass =>
        // may handle the request & short circuit the rest of the pipeline
        // may process outgoing response as well
        // performs in the order they were in the pipeline





        // configure the request processing pipeline 

        // use routing before using endpoints
        // at first only 2 were used -> devexceptionpage & run

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions dev = 
                    new DeveloperExceptionPageOptions() 
                    { 
                        SourceCodeLineCount = 20
                    };
      
                app.UseDeveloperExceptionPage(dev);
            }


            // if no other thing with only 2 middlewares in start all requests will be handled by this
            // https://localhost:44378/ => returns hello world
            // https://localhost:44378/abc => returns hello world
            // https://localhost:44378/xyz => returns hello world
            // https://localhost:44378/foo.html => returns hello world
            // but app.UseStaticFiles  will return the actual foo.html!
            //
            /*            DefaultFilesOptions df = new DefaultFilesOptions();
                        df.DefaultFileNames.Clear();
                        df.DefaultFileNames.Add("foo.html");
                        app.UseDefaultFiles(df);*/
            //app.UseStaticFiles();

            /*            DefaultFilesOptions df = new DefaultFilesOptions();
                        df.DefaultFileNames.Clear();
                        df.DefaultFileNames.Add("foo.html");*/
            /*FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            app.UseFileServer(fileServerOptions);   // alternative => use static files?*/
            // learn how to block certain file directories
            // & create new ones
            // & maybe authorization?

            
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();   // doesnt have ctrl Home and Action Index
            app.Use(async (context,next) =>
            {
                logger.LogInformation("mw1 in");
                await context.Response.WriteAsync(
                   "default");
                await next();
                logger.LogInformation("mw1 out");
            });
            app.Use(async (context, next) =>
            {
                logger.LogInformation("mw2 in");
                await context.Response.WriteAsync(
                   "mid");
                await next();
                logger.LogInformation("mw2 out");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                   "next");
                logger.LogInformation("mw3 in and out");
            });
        } 
    }
}
