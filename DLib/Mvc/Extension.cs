using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DLib.Mvc
{
    public static class Extension
    {
        public static MvcHtmlString EnumDropDownList<TModel, TEnum>(
                    this HtmlHelper<TModel> htmlHelper,
                    string name,
                    TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
                                        .Cast<TEnum>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem()
                                                {
                                                    Text = value.ToString(),
                                                    Value = (Convert.ToInt32(value)).ToString(),
                                                    Selected = (value.Equals(selectedValue))
                                                };
            //return SelectExtensions.DropDownListFor(htmlHelper, expression, items);
            return SelectExtensions.DropDownList(htmlHelper, name, items);
        }
    }
}