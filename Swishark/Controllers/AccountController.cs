using Infrastructure.Entities;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Account;
using Swishark.Models;
using Swishark.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Swishark.Controllers
{
	public class AccountController : Controller
    {
        [HttpGet]
        [Route("signup")]
        public ActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("list", "project");

            return View();
        }

        [HttpPost]
        [Route("signup")]
        public async Task<JsonResult> SignUp(PageModel pModel, string s)
        {
            AccountHelper helper = new AccountHelper();
            var code = helper.Create(pModel.Data);

            if ((int)code == 200)
            {
                await Authenticate(new Json().From(pModel.Data)["Email"]);
            }

            return new JsonResult(code);
        }



        [HttpGet]
        [Route("signin")]
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("list", "project");

            return View();
        }

        [HttpPost]
        [Route("signin")]
        public async Task<JsonResult> SignIn(PageModel pModel, string s)
        {
            AccountHelper helper = new AccountHelper();
            var code = helper.Auth(pModel.Data);

            if ((int)code == 200)
            {
                await Authenticate(new Json().From(pModel.Data)["Email"]);
            }

            return new JsonResult(code);
        }



        /// <summary>
        /// Создаёт куку.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}