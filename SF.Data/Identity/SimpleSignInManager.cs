﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using SF.Core.Abstraction.Events;

namespace SF.Data.Identity
{
    public class SimpleSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        private HttpContext _context;

        public SimpleSignInManager(UserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<TUser>> logger,
            IMediator mediator)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
            _contextAccessor = contextAccessor;
            _mediator = mediator;

          
        }

        internal new HttpContext Context
        {
            get
            {
                var context = _context ?? _contextAccessor?.HttpContext;
                if (context == null)
                {
                    throw new InvalidOperationException("HttpContext must not be null.");
                }
                return context;
            }
            set
            {
                _context = value;
            }
        }

        public override async Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            var userId = await UserManager.GetUserIdAsync(user);
            _mediator.Publish(new UserSignedIn { UserId = long.Parse(userId) });
            //await _mediator.SendAsync(new AsyncValidationRequestHandler();
            await base.SignInAsync(user, isPersistent, authenticationMethod);
        }
    }


}
