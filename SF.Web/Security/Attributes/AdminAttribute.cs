﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace SF.Web.Security.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    /// <summary>
    /// When applied to an action or a controller, intercepts any request to check whether it applies to the admin site.
    /// If so it marks the request as such and ensures the user as the right to access it.
    /// </summary>
    public class AdminAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Apply(context.HttpContext);

            return base.OnActionExecutionAsync(context, next);
        }

        public static void Apply(HttpContext context)
        {
            // The value isn't important, it's just a marker object
            context.Items[typeof(AdminAttribute)] = null;
        }

        public static bool IsApplied(HttpContext context)
        {
            object value;
            return context.Items.TryGetValue(typeof(AdminAttribute), out value);
        }
    }
}
