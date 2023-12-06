using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SistemaJuridicoWebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddDbContext<SistemaJuridicoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var _secret = builder.Configuration.GetSection("SecretKey");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtBearerOptions =>
{
    jwtBearerOptions.RequireHttpsMetadata = false;
    jwtBearerOptions.SaveToken = true;

    jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret.Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});


builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
