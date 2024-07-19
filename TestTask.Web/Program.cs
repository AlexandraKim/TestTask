using Microsoft.EntityFrameworkCore;
using TestTask.Infrastructure.Extensions;
using TestTask.Infrastructure.Persistence;
using TestTask.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddIdentity();

// builder.Services.AddScoped<IAuthService, AuthService>();
// // builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
//
// builder.Services.AddScoped<IProductService, ProductService>();
// builder.Services.AddScoped<IProductChangeService, ProductChangeService>();

builder.Services.AddHttpContextAccessor();
VatValue.Value = builder.Configuration.GetSection("VatValue").GetValue<double>("Value");
builder.Services.AddAutoMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("../swagger/v1/swagger.json", "API");
  c.RoutePrefix = "api";
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  "default",
  "{controller=Home}/{action=Index}/{id?}");

// ApplyMigration();
app.Run();

// void ApplyMigration()
// {
//   using (var scope = app.Services.CreateScope())
//   {
//     var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     if (_db.Database.GetPendingMigrations().Any()) _db.Database.Migrate();
//   }
// }