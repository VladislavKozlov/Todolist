using System;
using System.Text;
using System.Web.Mvc;
using Todolist.ViewModels;

namespace Todolist.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, TasksPaginInfo tasksPaginInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= tasksPaginInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == tasksPaginInfo.PageNumber)
                {
                    tag.AddCssClass("paginLink");
                }
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}