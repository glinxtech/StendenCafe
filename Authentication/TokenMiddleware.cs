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

        public async Task Invoke(HttpContext context, UserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = TokenHelper.ValidateToken(token);

            if (userId != null)
            {
                var user = await userRepository.GetById(userId);
                context.Items["User"] = user;
            }


            await _next(context);
        }
    }
}
