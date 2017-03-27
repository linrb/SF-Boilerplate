﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:					2015-07-11
// Last Modified:			2016-05-17
// 

using SF.Web.Navigation.Helpers;
using Microsoft.AspNetCore.Http;

namespace SF.Web.Navigation
{
    public class NavigationNodePermissionResolver : INavigationNodePermissionResolver
    {
        public NavigationNodePermissionResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private IHttpContextAccessor httpContextAccessor;

        public virtual bool ShouldAllowView(TreeNode<NavigationNode> menuNode)
        {
            if (string.IsNullOrEmpty(menuNode.Value.ViewRoles)) { return true; }
            if (menuNode.Value.ViewRoles == "All Users;") { return true; }

            if (httpContextAccessor.HttpContext.User.IsInRoles(menuNode.Value.ViewRoles))
            {
                return true;
            }

            return false;
        }
    }
}
