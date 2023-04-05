using Doppler.Files.Api.DopplerSecurity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDopplerSecurity();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter the token into field as 'Bearer {token}'",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme },
                            },
                            Array.Empty<string>()
                        }
                    });

    var baseUrl = builder.Configuration.GetValue<string>("BaseURL");
    if (!string.IsNullOrEmpty(baseUrl))
    {
        c.AddServer(new OpenApiServer() { Url = baseUrl });
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
