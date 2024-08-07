using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KidsEcomAPI.Apikey
{
    public class Fillter : IAuthorizationFilter
    {
        private readonly Check _check;
        public Fillter(Check check)
        {
            _check = check;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           string? api = context.HttpContext.Request.Headers[BienSo.ApiHeaderName].ToString();
            if (string.IsNullOrEmpty(api)) {
                context.Result = new BadRequestResult(); //400
            }
            if (!_check.CheckKey(api)) {
                context.Result = new UnauthorizedResult(); //401
            }
        }
    }
}
