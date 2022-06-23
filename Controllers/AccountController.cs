using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult test()
        {

            return View();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usersmodel usersmodel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/api/values/" + usersmodel.user);
                //HTTP GET
                var postTask = client.GetAsync(usersmodel.user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //
                    var listUsersTask = result.Content.ReadAsAsync<Usersmodel>();
                    listUsersTask.Wait();
                    var dbUserDetails = listUsersTask.Result;
                    if (usersmodel.password == dbUserDetails.password)
                    {
                        FormsAuthentication.SetAuthCookie(usersmodel.user, false);
                        return RedirectToAction("Index", "Home");

                    }
                    else  
                    {
                        ModelState.AddModelError(string.Empty, "user name or password invalid!");
                        return View();

                    }

                }
                else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError(string.Empty, "user name or password invalid!");
                    return View();

                }

            }

            ModelState.AddModelError(string.Empty, "Server Error, Please contact administrator");
            return View(usersmodel);

        }

        public ActionResult LogOut()
        {
            if (User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Usersmodel usersmodel)
        {
            using (var client = new HttpClient())
           {
                client.BaseAddress = new Uri("https://localhost:44358/api/values");

                var postTask = client.PostAsJsonAsync("values", usersmodel);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.result = "User successfully created.";
                    return View();
                    //return RedirectToAction("Index");
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError(string.Empty, "User already exists...");
                    return View();
                }


                ModelState.AddModelError(string.Empty, "server error.please contact administrator");
                return View(usersmodel);


            }




        }
    }
}