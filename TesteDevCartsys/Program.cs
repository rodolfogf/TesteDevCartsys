using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TesteDevCartsys.Data;
using TesteDevCartsys.Models;
using TesteDevCartsys.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using RazorLight;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:TesteDevCartsysConnection"];


// Add services to the container.

builder.Services.AddDbContext<TesteDevCartsysContext>(opts => 
    opts.UseLazyLoadingProxies().UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString))
    );

builder.Services.AddIdentity<Usuario, IdentityRole>()
.AddEntityFrameworkStores<TesteDevCartsysContext>()
.AddDefaultTokenProviders();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSingleton<IRazorLightEngine>(new RazorLightEngineBuilder()
    .UseFileSystemProject(Directory.GetCurrentDirectory())
    .UseMemoryCachingProvider()
    .Build());

builder.Services.AddSingleton<IConverter, SynchronizedConverter>();

builder.Services.AddSingleton<PdfService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
