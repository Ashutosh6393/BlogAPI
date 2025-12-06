using System.Text;
using MegaBlogAPI;
using MegaBlogAPI.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MegaBlogAPI.Services.Interface;
using MegaBlogAPI.Repository.Interface;
using MegaBlogAPI.Services.Implementation;
using MegaBlogAPI.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MegaBlogAPI.Services.Interfaces;
using MegaBlogAPI.Services.Implementations;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.



builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Check Authorization header first
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // If not found, check cookie
            if (string.IsNullOrEmpty(token))
            {
                token = context.Request.Cookies["jwt"];
            }
            context.Token = token;
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});
builder.Services.AddScoped(typeof(IUserClaimsService), typeof(UserClaimsService));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IPostRepository), typeof(PostRepository));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
builder.Services.AddScoped(typeof(IPostService), typeof(PostService));

builder.Services.AddControllers();
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
