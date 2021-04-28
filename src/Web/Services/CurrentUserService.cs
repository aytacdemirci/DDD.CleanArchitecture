using System.Linq;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            if (long.TryParse(GetHeaderValue(httpContextAccessor, "X-UserId"), out long userId))
            {
                UserId = userId;
            }
        }

        public long? UserId { get; }

        private string GetHeaderValue(IHttpContextAccessor httpContextAccessor, string headerKey)
        {
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerKey, out StringValues headerValues))
            {
                return headerValues.First();
            }

            return string.Empty;
        }
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}
