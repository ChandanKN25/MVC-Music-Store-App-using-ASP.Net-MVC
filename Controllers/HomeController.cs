using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;
using WebAPI.Security;

namespace WebAPI.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            // Get most popular albums
            var albums = GetTopSellingAlbums(5);
            return View(albums);
            //ViewBag.Title = "Home Page";

            //return View();
        }


        [CustomAuthorize(Roles = "Admin")]
        public ActionResult EditUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser()
        {
            ViewBag.Title = "Delete user";
            return View();
        }


        [CustomAuthorize(Roles ="Admin")]
          //[Authorize(Roles ="Admin")]
          [HttpPost]
        public ActionResult EditUser(Usersmodel usersmodel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/api/values");
                //HTTP GET

                var responseTask = client.PutAsJsonAsync("values", usersmodel);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();

                    ModelState.AddModelError(string.Empty, readTask.Result);

                }

                else if (result.StatusCode== System.Net.HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError(string.Empty,"User not found");

                }

                
            }


            return View();


        }
        [CustomAuthorize(Roles = "Admin")]
        //  [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteUser(Usersmodel usersmodel)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/api/values/"+usersmodel.user);
                var postTask = client.DeleteAsync(usersmodel.user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.result = "user successfully deleted";
                    return View();
                }

                return View();
            }
            
        }


        public ActionResult UserList()
        {
            List<Usersmodel> listusersmodels = new List<Usersmodel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/api/values");

                //HTTP GET
                var postTask = client.GetAsync("values");
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var listUsersTask = result.Content.ReadAsAsync<List<Usersmodel>>();
                    listUsersTask.Wait();
                    listusersmodels = listUsersTask.Result;

                    return View(listusersmodels);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError(string.Empty, result.ReasonPhrase);
                    return View();

                }

            }

            ModelState.AddModelError(string.Empty, "Server Error, Please contact Admininstrator");
            return View();

        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            iLead2021Entities1 storeDB = new iLead2021Entities1();
            return storeDB.Albums.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
        }

    }
}
