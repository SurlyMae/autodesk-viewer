
using Autodesk.Forge.Client;
using RestSharp;

public class Startup
{
  public Startup(IConfiguration config)
  {
    Configuration = config;
  }

  public IConfiguration Configuration {get;}

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddControllers();
    var clientID = Configuration["APS_CLIENT_ID"];
    var clientSecret = Configuration["APS_CLIENT_SECRET"];
    var bucket = Configuration["APS_BUCKET"];
    if(string.IsNullOrEmpty(clientID) || string.IsNullOrEmpty(clientSecret))
    {
      throw new ApplicationException("Missing required env vars APS_CLIENT_ID or APS_CLIENT_SECRET");
    }
    services.AddSingleton<APS>(new APS(clientID, clientSecret, bucket));
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if(env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseEndpoints(endpoints => {endpoints.MapControllers();});
  }
}