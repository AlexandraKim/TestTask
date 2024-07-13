namespace TestTask.Middleware;

public static class AutoMapperConfiguration
{
  public static WebApplicationBuilder AddConfiguredAutoMapper(this WebApplicationBuilder builder)
  {
    var mapper = MappingConfig.RegisterMaps().CreateMapper();
    builder.Services.AddSingleton(mapper);
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    return builder;
  }
}