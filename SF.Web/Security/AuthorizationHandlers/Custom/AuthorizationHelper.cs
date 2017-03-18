using SF.Core.Common;
using SF.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SF.Web.Security.AuthorizationHandlers.Custom
{
    public class AuthorizationHelper : IAuthorizationHelper
    {
        private readonly ISiteContext _siteContext;
        public AuthorizationHelper(SiteContext currentSite)
        {
            _siteContext = currentSite;
        }
        public async Task AuthorizeAsync(IEnumerable<ISFAuthorizeAttribute> authorizeAttributes)
        {
            if (!_siteContext.EnabledAuthorization)
            {
                return;
            }

            //if (!AbpSession.UserId.HasValue)
            //{
            //    throw new AbpAuthorizationException(
            //        LocalizationManager.GetString(AbpConsts.LocalizationSourceName, "CurrentUserDidNotLoginToTheApplication")
            //        );
            //}

            foreach (var authorizeAttribute in authorizeAttributes)
            {
                
              //  await PermissionChecker.AuthorizeAsync(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
            }
        }

        public async Task AuthorizeAsync(MethodInfo methodInfo)
        {
            if (!_siteContext.EnabledAuthorization)
            {
                return;
            }

            if (AllowAnonymous(methodInfo))
            {
                return;
            }

            //Authorize
            await CheckPermissions(methodInfo);
        }
        private async Task CheckPermissions(MethodInfo methodInfo)
        {
            var authorizeAttributes =
                ReflectionUtility.GetAttributesOfMemberAndDeclaringType(
                    methodInfo
                ).OfType<ISFAuthorizeAttribute>().ToArray();

            if (!authorizeAttributes.Any())
            {
                return;
            }

            await AuthorizeAsync(authorizeAttributes);
        }
        private static bool AllowAnonymous(MethodInfo methodInfo)
        {
            return ReflectionUtility.GetAttributesOfMemberAndDeclaringType(methodInfo)
                .OfType<ISFAllowAnonymousAttribute>().Any();
        }
    }
}
