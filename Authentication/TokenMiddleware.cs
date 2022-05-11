using StendenCafe.Services;

namespace StendenCafe.Authentication
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserRepository userRepository, TokenHelper tokenHelper)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = tokenHelper.ValidateToken(token);

            if (userId != null)
                context.Items["User"] = userRepository.GetById(userId);

            await _next(context);
        }
    }
}
