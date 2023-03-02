using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiVL.Context;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApiVL.Models;
using Microsoft.AspNetCore.Builder;
using WebApiVL.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionSTring = builder.Configuration.GetConnectionString("VelocidadLecturaDB");
builder.Services.AddDbContext <VelocidadLecturaContext>(options => options.UseSqlServer(connectionSTring));
// Add services to the container.
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddIdentityCore<Usuario>().AddRoles<IdentityRole>();//acafalta el dbcontext
//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<VelocidadLecturaContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApiVL", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiVL v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
//builder.Services.AddIdentityCore<Usuario>().AddRoles<IdentityRole>().AddEntityFrameworkStores<VelocidadLecturaContext>();
//builder.Services.AddIdentity<> AddDefaultIdentity<ApplicationUser>().AddUserStore<DBContext>();

