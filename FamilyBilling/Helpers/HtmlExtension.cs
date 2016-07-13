using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HtmlTags;
using System.Globalization;

namespace FamilyBilling.Helpers
{
    public enum HtmlTagWidth
    {
        Short,
        Normal,
        Large,
        Full
    }

    public static class HtmlExtension
    {
        public static HtmlTag BillingLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string labelText = "", string customAbbreviation = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            HtmlTag tag = new HtmlTag("label");
            tag.Attr("for", fullName);

            tag.Text(labelText);

                
            return tag;
        }
    }
}