using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Projekt_zaliczeniowy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));//rejestracja bazy danych 



            //logowanie na ciasteczkach 
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                {
                    
                    
                    options.LoginPath = "/Logowanie";
                });



            var app = builder.Build();


            using (var scope = app.Services.CreateScope()) //admin
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Uzytkownicy.Any())
                {
                    string admin_login = builder.Configuration["AdminCredentials:Login"] ?? "test";
                    string admin_haslo = builder.Configuration["AdminCredentials:Haslo"] ?? "test123";

                    var hasher = new PasswordHasher<Uzytkownik>();

                    var admin = new Uzytkownik
                    {
                        Login = admin_login,
                        CzyAdmin = true
                    };

                    admin.HasloHash = hasher.HashPassword(admin, admin_haslo);

                    context.Uzytkownicy.Add(admin);
                    context.SaveChanges();

                    context.Wyniki.Add(new Wynik { Uzytkownik = admin, MaksymalnaSeria = 0 });
                    context.SaveChanges();
                }
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization(); 

            


            

            app.MapStaticAssets();
            app.MapRazorPages().WithStaticAssets(); //logowanie 

            app.Run();
        }
    }
}
