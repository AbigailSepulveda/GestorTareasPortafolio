using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Mvc;
using web_portafolio.Models;

namespace web_portafolio.Controllers {
    public abstract class BaseController : Controller {

        private readonly String baseURL = "http://192.168.0.165:52834/api/";
        public readonly String endPointUser = "User/";
        public readonly String endPointTask = "Task/";

        protected HttpClient getClient(String token = "") {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != "") {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }
            return client;
        }

        protected string[] getData() {
            var identity = (ClaimsIdentity)User.Identity;
            string[] data = null;
            if (identity != null) {
                IEnumerable<Claim> claims = identity.Claims;
                data = new string[]{
                identity.FindFirst(ClaimTypes.Name).Value,
                identity.FindFirst(ClaimTypes.Role).Value,
                identity.FindFirst(ClaimTypes.Email).Value,
                identity.FindFirst(ClaimTypes.GroupSid).Value,
                identity.FindFirst(ClaimTypes.UserData).Value,
                identity.FindFirst(ClaimTypes.Authentication).Value,
                identity.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            }
            return data;
        }

        protected HomeViewModel getHomeViewModel() {
            var data = getData();
            return new HomeViewModel {
                Name = data[0],
                Type = data[1],
                Email = data[2],
                Modules = data[3],
                Unit = data[4],
                Token = data[5],
                Id = data[6]
            };
        }
    }
}