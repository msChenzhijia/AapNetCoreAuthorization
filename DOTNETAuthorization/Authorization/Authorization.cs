using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DOTNETAuthorization.Authorization
{
    /// <summary>
    /// 安全认证过滤器
    /// </summary>
    public class Authorization : IActionFilter, IAuthorizationFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //允许匿名访问
            if (context.HttpContext.User.Identity.IsAuthenticated||context.Filters.Any(item=>item is IAuthorizationFilter))
            {
                return;
            }
            var httpcontext = context.HttpContext;
            var claimsIdentity = httpcontext.User.Identity as ClaimsIdentity;
            var request = context.HttpContext.Request;
            var authorization = request.Headers["Authorization"].ToString();
            if (authorization!=null&&authorization.Contains("BasicAuth"))
            {
                //获取请求头中传递的ticket
                var current_ticket = authorization.Split(" ")[1];
                //校验ticket并获取用户信息
                var userInfo= TicketEncryption
            }
        }
    }
}
