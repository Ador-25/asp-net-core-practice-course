using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practice_course
{
    // contains main method
    // .net initially starts as console app
    // main configs and start as web app
    public class Program
    {
        public static void Main(string[] args)
        {
            // starts as a console app
            CreateHostBuilder(args).Build().Run();
        }


        // CreateWebHostBuilder = passes cmd args
        // Returns an object that implements IWebHostBuilder
        // Object it returns -> runs build -> calls run -> runs the app after building
        // Extension Method - >
        // List<int> n= new List<int>{2,4,3,4,2};
        // IEnumerable <int> evens = nums.WHere(n=> n%2==0)
        // Where doesnt belong to List<T> , but still works
        // bcs LINQ's standard query ops are implemented in Enumerable class as extension methods
        // Startup class => configures services required by the app

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)     // calls UseIIS behind
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
