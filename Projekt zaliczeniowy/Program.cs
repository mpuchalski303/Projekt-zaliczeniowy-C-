using Microsoft.AspNetCore.Authentication.Cookies;

namespace Projekt_zaliczeniowy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            

            
            //logowanie na ciasteczkach 
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                {
                    
                    
                    options.LoginPath = "/Logowanie";
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //sprawdzanie kim jest osoba logujaca 
            app.UseAuthorization(); //sprawdzamy czy ma dostep

            


            

            app.MapStaticAssets();
            app.MapRazorPages().WithStaticAssets(); //logowanie 

            app.Run();
        }
    }
}
