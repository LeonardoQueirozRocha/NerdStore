using NerdStore.WebApp.MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddDbContextConfiguration(builder.Configuration);
builder.Services.AddMvcConfiguration(builder.Configuration);
builder.Services.AddDependencies();

var app = builder.Build();

app.UseMvcConfiguration(app.Environment);
app.Run();
