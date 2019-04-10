using Infrastructure.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Account;
using Swishark.Models;
using Swishark.Util;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Swishark.Controllers
{
	// [Route("[controller]")]
	public class AccountController : Controller
    {
		[HttpGet]
		[Route("signup")]
		public HtmlResult SignUp(PageModel pModel)
		{
			pModel.Num = (int)Pages.Nums.SignUp;
			return new HtmlResult($"<input class='ds-n' id='pageNum' value='{pModel.Num}'/>");
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
		public HtmlResult SignIn(PageModel pModel)
		{
			pModel.Num = (int)Pages.Nums.SignIn;
			return new HtmlResult($"<input class='ds-n' id='pageNum' value='{pModel.Num}'/>");
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



        // <summary>
        // Часть API. Возвращает страницу.
        //</summary>
        [Route("api/account/{pageUrl}")]
		[HttpGet("{pageUrl}")]
		public HtmlResult Get(string pageUrl, PageModel model)
		{
			switch(pageUrl.ToLower()){
				case "signup":
					model.Num = (int)Pages.Nums.SignUp;
				break;

				case "signin":
					model.Num = (int)Pages.Nums.SignIn;
				break;
			}
			return new HtmlResult($"<input class='ds-n' id='pageNum' value='{model.Num}'/>");
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