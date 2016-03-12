using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DLib.Mvc
{
    public static class Extension
    {
        public static MvcHtmlString DropDownList<TModel, TEnum>(
                    this HtmlHelper<TModel> htmlHelper,
                    string name,
                    TEnum selectedValue) where TEnum : struct
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
                                        .Cast<TEnum>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem()
                                                {
                                                    Text = value.ToString(),
                                                    Value = EnumUtil.GetCode<TEnum>(value),
                                                    Selected = (value.Equals(selectedValue))
                                                };
            return SelectExtensions.DropDownList(htmlHelper, name, items);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty, TEnum>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    TEnum selectedValue) where TEnum : struct
        {
            return DropDownListFor<TModel, TProperty, TEnum>(htmlHelper, expression, selectedValue, EnumUtil.GetCode<TEnum>);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty, TEnum>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    TEnum selectedValue,
                    Func<TEnum, string> getValue) where TEnum : struct
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
                                        .Cast<TEnum>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem()
                                                {
                                                    Text = value.ToString(),
                                                    Value = getValue(value),
                                                    Selected = (value.Equals(selectedValue))
                                                };
            return SelectExtensions.DropDownListFor(htmlHelper, expression, items);
        }
    }
}