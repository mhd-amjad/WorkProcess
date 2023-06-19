using Dapr.Actors.Runtime;
using WorkProcess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICounterActorService, CounterActorService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprSidekick(builder.Configuration);

    var actorConfig = builder.Configuration.GetSection("MyActorConfig");
    var actorOptions = new ActorRuntimeOptions();
    actorConfig.Bind(actorOptions);

    builder.Services.AddActors(options => {
        options.Actors.RegisterActor<MyActor>();
        }) ;
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Register actors handlers that interface with the Dapr runtime.
    endpoints.MapActorsHandlers();
});
app.Run();