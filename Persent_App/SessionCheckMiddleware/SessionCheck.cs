namespace Persent_App.SessionCheckMiddleware
{
    public class SessionCheck
    {
        private readonly RequestDelegate _next;

        public SessionCheck(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // اگر سشن خالی باشد و کاربر احراز هویت شده باشد، سشن را پاک کنید و به صفحه ورود هدایت کنید
            if (context.Session.GetString("IsLoggedIn") == null && context.User.Identity.IsAuthenticated)
            {
                context.Session.Clear(); // پاک کردن سشن‌ها
                context.Response.Redirect("/Account/Login");
                return; // جلوگیری از ادامه پردازش درخواست
            }

            await _next(context); // ادامه پردازش درخواست
        }
    }
}
 

