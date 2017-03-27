﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:					2015-07-09
// Last Modified:			2016-09-22
// 

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SF.Web.Navigation
{
    public static class TreeNodeExtensions
    {
        /// <summary>
        /// finds the first child node whose Url property contains the urlToMatch
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="urlToMatch"></param>
        /// <returns></returns>
        public static TreeNode<NavigationNode> FindByUrl(
            this TreeNode<NavigationNode> currentNode,
            IUrlHelper urlHelper,
            string urlToMatch, 
            string urlPrefix = "")
        {
            Func<TreeNode<NavigationNode>, bool> match = delegate (TreeNode<NavigationNode> n)
            {
                if(n == null) { return false; }
                if (string.IsNullOrEmpty(urlToMatch)) { return false; }

                if (!string.IsNullOrEmpty(n.Value.Url))
                {
                    if (n.Value.Url.IndexOf(urlToMatch, StringComparison.OrdinalIgnoreCase) >= 0)
                    { return true; }
                }



                if ((urlToMatch.EndsWith("Index"))&&(n.Value.Action == "Index"))
                {
                   if(!n.Value.Url.EndsWith("/") && (!n.Value.Url.Contains("Index")))
                   {
                        var u = n.Value.Url + "/Index";
                        if (u.IndexOf(urlToMatch, StringComparison.OrdinalIgnoreCase) >= 0)
                        { return true; }
                   }

                }

                string targetUrl = string.Empty;
                if (!string.IsNullOrEmpty(n.Value.NamedRoute))
                {
                    targetUrl = urlHelper.RouteUrl(n.Value.NamedRoute);
                    if (targetUrl.IndexOf(urlToMatch, StringComparison.OrdinalIgnoreCase) >= 0)
                    { return true; }
                }

                if ((!string.IsNullOrEmpty(n.Value.Action))&& (!string.IsNullOrEmpty(n.Value.Controller)))
                {
                    targetUrl = urlHelper.Action(n.Value.Action, n.Value.Controller, new { area = n.Value.Area });
                    if (targetUrl.IndexOf(urlToMatch, StringComparison.OrdinalIgnoreCase) >= 0)
                    { return true; }
                }

                if ((urlPrefix.Length > 0)&&(n.Value.Url.Length > 0))
                {
                    targetUrl = n.Value.Url.Replace("~/", "~/" + urlPrefix + "/");
                    if(targetUrl.IndexOf(urlToMatch,StringComparison.OrdinalIgnoreCase) >= 0)
                    { return true; }      
                }

                

                return false;
            };

            return currentNode.Find(match);
        }

        /// <summary>
        /// this would be called as a secondary check if current node not found by FindByUrl
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="urlHelper"></param>
        /// <param name="urlToMatch"></param>
        /// <param name="urlPrefix"></param>
        /// <returns></returns>
        public static TreeNode<NavigationNode> FindByUrlStartsWith(
            this TreeNode<NavigationNode> currentNode,
            IUrlHelper urlHelper,
            string urlToMatch,
            string urlPrefix = "")
        {
            Func<TreeNode<NavigationNode>, bool> match = delegate (TreeNode<NavigationNode> n)
            {
                if (n == null) { return false; }
                if (string.IsNullOrEmpty(urlToMatch)) return false;
                if (string.IsNullOrEmpty(n.Value.Url)) { return false; }

                if (urlToMatch.StartsWith(n.Value.Url)) { return true; }

                

                return false;
            };

            return currentNode.Find(match);
        }

        /// <summary>
        /// finds the first child node whose url exactly matches the provided urlToMatch
        /// note that Url usually starts with ~/
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="urlToMatch"></param>
        /// <returns></returns>
        public static TreeNode<NavigationNode> FindByUrlExact(this TreeNode<NavigationNode> currentNode, string urlToMatch)
        {
            Func<TreeNode<NavigationNode>, bool> match = delegate (TreeNode<NavigationNode> n)
            {
                return (n.Value.Url == urlToMatch);
            };

            return currentNode.Find(match);
        }

        public static TreeNode<NavigationNode> FindByKey(this TreeNode<NavigationNode> currentNode, string key)
        {
            Func<TreeNode<NavigationNode>, bool> match = delegate (TreeNode<NavigationNode> n)
            {
                return (n.Value.Key == key);
            };

            return currentNode.Find(match);
        }

        /// <summary>
        /// finds the first child node that matches the provided controller and action name
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TreeNode<NavigationNode> FindByControllerAndAction(this TreeNode<NavigationNode> currentNode, string controller, string action)
        {
            Func<TreeNode<NavigationNode>, bool> match = delegate (TreeNode<NavigationNode> n)
            {
                return ((n.Value.Controller == controller)&&(n.Value.Action == action));
            };

            return currentNode.Find(match);
        }

        public static List<TreeNode<NavigationNode>> GetParentNodeChain(
            this TreeNode<NavigationNode> currentNode, 
            bool includeCurrentNode,
            bool includeRoot)
        {
            List<TreeNode<NavigationNode>> list = new List<TreeNode<NavigationNode>>();
            if(includeCurrentNode)
            {
                list.Add(currentNode);
            }

            TreeNode<NavigationNode> parentNode = currentNode.Parent;
            while(parentNode != null)
            {
                if(includeRoot ||(!parentNode.Value.IsRootNode))
                {
                    list.Add(parentNode);
                }
               
                parentNode = parentNode.Parent;
            }

            // this is used for breadcrumbs
            // so we want the sort from parent down to current node
            list.Reverse();

            return list;
        }

        public static bool EqualsNode(this TreeNode<NavigationNode> currentNode, TreeNode<NavigationNode> nodeToMatch)
        {
            if(currentNode.Value.Key == nodeToMatch.Value.Key) { return true; }
            if (currentNode.Value.Controller == nodeToMatch.Value.Controller && (currentNode.Value.Controller.Length > 0))
            {
                if (currentNode.Value.Action == nodeToMatch.Value.Action && (currentNode.Value.Action.Length > 0))
                {
                    return true;
                }
                    
            }
            if((nodeToMatch.Value.NamedRoute.Length > 0)
                &&(nodeToMatch.Value.NamedRoute == currentNode.Value.NamedRoute)) { return true; }

            if(
                (!string.IsNullOrEmpty(currentNode.Value.Url))
                && (!string.IsNullOrEmpty(nodeToMatch.Value.Url))
                )
            {
                if (currentNode.Value.Url == nodeToMatch.Value.Url) { return true; }
            }
            

            return false;
        }

        public static bool EqualsNode(this NavigationNode currentNode, NavigationNode nodeToMatch)
        {
            if (currentNode.Key == nodeToMatch.Key) { return true; }
            if (currentNode.Controller == nodeToMatch.Controller && (currentNode.Controller.Length > 0))
            {
                if (currentNode.Action == nodeToMatch.Action && (currentNode.Action.Length > 0))
                {
                    return true;
                }

            }
            if ((nodeToMatch.NamedRoute.Length > 0)
                && (nodeToMatch.NamedRoute == currentNode.NamedRoute)) { return true; }

            if(!string.IsNullOrEmpty(nodeToMatch.Url))
            {
                if (!string.IsNullOrEmpty(currentNode.Url))
                {
                    if (currentNode.Url == nodeToMatch.Url) { return true; }
                }
            }
            
            return false;
        }



        public static string ToJsonIndented(this TreeNode<NavigationNode> node)
        {
            return JsonConvert.SerializeObject(
                node,
                Formatting.Indented,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
                );
        }

        public static string ToJsonCompact(this TreeNode<NavigationNode> node)
        {
            return JsonConvert.SerializeObject(
                node,
                Formatting.None,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
                );
        }
    }
}
