using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace Swishark.Util
{
    public class HtmlResult : IActionResult
    {
        string content;
        public HtmlResult(string html)
        {
            content = html;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
			string doctype = "<!DOCTYPE html>",
			title = "<title></title>",
			meta = "<meta charset='utf-8'/>",
			link = "<link rel='stylesheet' href='../../css/style.css'>",
			scriptTop = "",
			scriptBottom = "<script src='../../js/app.js'></script>";

			string head = $"{title} {meta} {link} {scriptTop}",
			body = $"<div class='app_wr' id='appWrapper'>{content} {scriptBottom}</div>",
			footer = "",
			html = $"<html> {doctype} {head} {body} {footer} </html>";

            await context.HttpContext.Response.WriteAsync(html);
        }
    }
}