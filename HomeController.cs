
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private const string PushUrl = "https://api.pushover.net/1/messages.json";

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public HttpStatusCode Push(string Message, string TokenKey, string UserKey)
        {
               
            if (string.IsNullOrEmpty(Message) || string.IsNullOrEmpty(TokenKey) || string.IsNullOrEmpty(UserKey))

                return HttpStatusCode.BadRequest;
            var parameters = new NameValueCollection {
                {"token", TokenKey},
                {"user", UserKey},
                {"message", Message}
            };
            using (var client = new WebClient())
            {
                try
                {
                    client.UploadValues(PushUrl, parameters);
                }
                catch (WebException)
                {
                    return HttpStatusCode.BadRequest;

                }
            }
            return HttpStatusCode.OK;

        }

        
    }
}
