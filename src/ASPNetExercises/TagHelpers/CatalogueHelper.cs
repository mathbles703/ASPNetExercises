using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Text;
using ASPNetExercises.ViewModels;
namespace ASPNetExercises.TagHelpers
{
    // You may need to install the Microsoft.AspNet.Razor.Runtime package into your project
    [HtmlTargetElement("catalogue", Attributes = CategoryIdAttribute)]
    public class CatalogueHelper : TagHelper
    {
        private const string CategoryIdAttribute = "category";
        [HtmlAttributeName(CategoryIdAttribute)]
        public string CategoryId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CatalogueHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_session.GetObject<MenuItemViewModel[]>("menu") != null && Convert.ToInt32(CategoryId) > 0)
            {
                var innerHtml = new StringBuilder();
                MenuItemViewModel[] menu = _session.GetObject<MenuItemViewModel[]>("menu");
                innerHtml.Append("<div class=\"col-xs-12\" style=\"font-size:x-large;\"><span>Catalogue</span></div>");
                foreach (MenuItemViewModel item in menu)
                {
                    if (item.CategoryId == Convert.ToInt32(CategoryId))
                    {
                        innerHtml.Append("<div id=\"item\" class=\"col-sm-3 col-xs-12 text-center\" style=\"border:solid;\">");
                        innerHtml.Append("<span class=\"col-xs-12\"><img src=\"/img/burger.jpg\" /></span>");
                        innerHtml.Append("<p id=descr" + item.Id + " data-description=\"" + item.Description + "\">");
                        innerHtml.Append("<span style=\"font-size:large;\">" + item.Description.Substring(0, 10) + "...</span></p><div>");
                        innerHtml.Append("<span>For Nutritional Info.<br />Click Details</span></div>");
                        innerHtml.Append("<div style=\"padding-bottom: 10px;\"><a href=\"#details_popup\" data-toggle=\"modal\" class=\"btn btn-default\"");
                        innerHtml.Append(" id=\"modalbtn\" data-id=\"" + item.Id + "\">Details</a>");
                        innerHtml.Append("<input type=\"hidden\" id=\"mcal" + item.Id + "\" value=\"" + item.CAL + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"mcarb" + item.Id + "\" value=\"" + item.CARB + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"mchol" + item.Id + "\" value=\"" + item.CHOL + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"mfat" + item.Id + "\" value=\"" + item.FAT + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"mfbr" + item.Id + "\" value=\"" + item.FBR + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"mpro" + item.Id + "\" value=\"" + item.PRO + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"msalt" + item.Id + "\" value=\"" + item.SALT + "\"/></div></div>");
                    }
                }
                output.Content.SetHtmlContent(innerHtml.ToString());
            }
        }
    }
}