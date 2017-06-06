using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Parkix.Shared.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parkix.Shared.Helpers;
using Parkix.Shared.Entities.Uaa;
using System.Net.Http.Headers;
using Parkix.UI.Models.Dashboard;

namespace Parkix.UI.Controllers
{
    public class DashboardController : Controller
    {
        public string ConfigureToken { get; set; }
        public string ReportToken { get; set; }
        private static readonly HttpClient configureClient = new HttpClient();
        private static readonly HttpClient reportClient = new HttpClient();

        public static DashboardModel dashModel = new DashboardModel();

        public string GetOAuthToken(string userName, string secret)
        {

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", userName),
                new KeyValuePair<string, string>("grant_type","client_credentials")
            });

            HttpClient junkClient;
            string response = "";
            using (junkClient = new HttpClient())
            {
                junkClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, secret))));
                response = junkClient.PostAsync("https://a7cc7c64-503e-43df-ab3a-18c97ef60505.predix-uaa.run.aws-usw02-pr.ice.predix.io/oauth/token", content).Result.Content.ReadAsStringAsync().Result;
            }

            dynamic tokenData = JObject.Parse(response);

            return tokenData["access_token"].Value;
        }

        // GET: Dashboard.
        public ActionResult Index()
        {
            dashModel.ParkingName = "Cal Poly Parking Infrastructure";
            ConfigureToken = GetOAuthToken("configure_client", "WMEv8luHI662207td9YFw4SIAuUy9nsM79VKLh4HQOOA7hzFvHTf0t3FcZCkmdtb9aWswGpifS0BPbUqJKvXMtcS2oBG0B9GoIcxf22Jh4J7kmg46fKrGYZVsATfCBdx");
            ReportToken = GetOAuthToken("report_client", "F8dQA3fOqzdXdZsknaobYKiWIG7xFi71JRmhQMGahIhH09ObiF9nzmTmO0hd71j9NHNJzj1G73KvAO5t9It2sibnB17vMPMlf7fGFlXvIA7PjZDpA1WlOyBUR21RSj1t");
            var lotsRequest = new HttpRequestMessage(HttpMethod.Get, "https://cp-parkix-configure.run.aws-usw02-pr.ice.predix.io/api/lots");
            lotsRequest.Headers.Add("authorization", ConfigureToken);
            var lotResult = configureClient.SendAsync(lotsRequest).Result.Content.ReadAsStringAsync().Result;
            dashModel.Lots = JArray.Parse(lotResult).ToObject<List<string>>();
            ViewData["token"] = ConfigureToken;

            return View(dashModel);
        }

        public IActionResult Sensor(string id)
        {

            ViewData["id"] = id;
            //we need to get this data dynamically
            return View(dashModel);
        }

        public IActionResult Config(string id)
        {
            ViewData["id"] = id;
            //we need to get this image dynamically. 
            ViewData["image"] = "../../../images/test23.jpg";
            return View(dashModel);
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendConfig([FromBody] string guid, [FromBody] string points)
        {
            // POST the data from here to Joe's server

            return RedirectToAction("Config/" + guid);
        }

        // POST: Dashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}