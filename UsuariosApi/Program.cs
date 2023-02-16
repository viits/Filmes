using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Conectando Banco
var ConnectionString = builder.Configuration.GetConnectionString("UsuarioConnection");

var secrety = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SecretyKey"));
// builder.ConfigureAppConfiguration((context, build) => build.AddUserSecrets<Program>());

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
);

builder.Services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
    opt => opt.SignIn.RequireConfirmedEmail = true
).AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<UsuarioService, UsuarioService>();
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<LogoutService, LogoutService>();
builder.Services.AddScoped<EmailService, EmailService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
