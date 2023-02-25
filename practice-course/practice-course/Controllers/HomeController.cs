using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practice_course.Controllers
{
    public class HomeController:Controller
    {
        public JsonResult Index()
        {
            return Json(
                new
                {
                    status = "Success",
                    message= "Response Text"
                }
                );
        }
    }
}
