using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practice_course.Controllers
{
    public class HomeController:Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        public HomeController(IConfiguration config,IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }
        public JsonResult Index()
        {
            return Json(
                new
                {
                    Status = "Success",
                    Message = _config["MyKey"]
                }
                ); ;
            /*            var internalServer = _httpContextAccessor.HttpContext.Request.Host.Value;

                        // Get the external server name
                        var externalServer = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                        // Return the server names in the response
                        return Json(new
                        {
                            InternalServer = internalServer,
                            ExternalServer = externalServer
                        });*/

        }
    }
}
