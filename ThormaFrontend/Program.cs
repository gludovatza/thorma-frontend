using Microsoft.Extensions.DependencyInjection;
using ThormaFrontend.Infrastructure;
using ThormaFrontend.Services;

namespace ThormaFrontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<AuthPageFilter>();
            // Add services to the container.
            builder.Services.AddRazorPages()
            .AddMvcOptions(options =>
            {
                options.Filters.AddService<AuthPageFilter>();
            });

            builder.Services.AddHttpClient("ThormaApi", c =>
            {
                c.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
                new HttpClientHandler { UseProxy = false }
            )
            .AddHttpMessageHandler<JwtBearerHandler>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<FestokApi>();
            builder.Services.AddScoped<KepekApi>();
            builder.Services.AddScoped<FeladatokApi>();
            builder.Services.AddScoped<AuthSession>();
            builder.Services.AddScoped<AuthApi>();
            builder.Services.AddScoped<AdminApi>();

            builder.Services.AddTransient<JwtBearerHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapRazorPages();

            app.Run();
        }
    }
}
