﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Abstraction.Data;
using SF.Entitys;
using SF.Data;

namespace SF.Web.Components
{
    public class UrlSlugRoute : IRouter
    {
        private readonly IRouter _target;

        public UrlSlugRoute(IRouter target)
        {
            _target = target;
        }

        public async Task RouteAsync(RouteContext context)
        {
            var requestPath = context.HttpContext.Request.Path.Value;

            if (!string.IsNullOrEmpty(requestPath) && requestPath[0] == '/')
            {
                // Trim the leading slash
                requestPath = requestPath.Substring(1);
            }

            var unitOfWork = context.HttpContext.RequestServices.GetService<IBaseUnitOfWork>();

            // Get the slug that matches.
            var urlSlug = await unitOfWork.BaseWorkArea.UrlSlug.Query().Include(x => x.EntityType).FirstOrDefaultAsync(x => x.Slug == requestPath);

            // Invoke MVC controller/action
            var oldRouteData = context.RouteData;
            var newRouteData = new RouteData(oldRouteData);
            newRouteData.Routers.Add(_target);

            // If we got back a null value set, that means the URI did not match)
            if (urlSlug == null)
            {
                return;
            }

            newRouteData.Values["controller"] = urlSlug.EntityType.RoutingController;
            newRouteData.Values["action"] = urlSlug.EntityType.RoutingAction;
            newRouteData.Values["id"] = urlSlug.EntityId;

            context.RouteData = newRouteData;
            await _target.RouteAsync(context);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }
    }
}
