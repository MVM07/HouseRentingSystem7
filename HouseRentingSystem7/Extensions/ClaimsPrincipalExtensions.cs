namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }
    }
}
