using EbookStore.Extensions;
using EbookStore.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddGlobalizationConfig()
        .AddMvcConfiguration()
       .AddIdentityConfiguration();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
