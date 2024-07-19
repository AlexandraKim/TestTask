using TestTask.Application.Extensions;
using TestTask.Core.Utility;
using TestTask.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

VatValue.Value = builder.Configuration.GetSection("VatValue").GetValue<double>("Value");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

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

await app.Services.ApplyMigrationsAsync();

app.Run();