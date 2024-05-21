using BuisinessLayer.Interface;
using BuisinessLayer.Service;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RepositaryLayer.Context;
using RepositaryLayer.Interface;
using RepositaryLayer.Service;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IUserBL, UserServiceBL>();
builder.Services.AddScoped<IUserRL, UserServiceRL>();
builder.Services.AddScoped<IBookRL, BookServiceRL>();
builder.Services.AddScoped<IBookBL, BookServiceBL>();
builder.Services.AddScoped<IAddressRL, AddressServiceRL>();
builder.Services.AddScoped<IAddressBL, AddressServiceBL>();
builder.Services.AddScoped<IOrderRL, OrderServiceRL>();
builder.Services.AddScoped<IOrderBL, OrderServiceBL >();
builder.Services.AddScoped<IWishListRL, WishListServiceRL>();
builder.Services.AddScoped<IWishListBL, WishListServiceBL>();
builder.Services.AddScoped<IShoppingCartRL, ShoppingCartServiceRL>();
builder.Services.AddScoped<IShoppingCartBL, ShoppingCartServiceBL>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Get the secret key from the configuration
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"]);

// Add JWT Bearer authentication options
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Validate the JWT signature
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            // For simplicity in development, disable issuer and audience validation
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    // Define the Swagger document metadata
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add security definition for JWT authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Add security requirements for Swagger endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDistributedMemoryCache();
//jwt
// Add JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");

//session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");
app.UseCors();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:4200", "https://localhost:7004")
//                   .AllowAnyMethod()
//                   .AllowAnyHeader()
//                   .AllowAnyOrigin();
//        });
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:7004")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithHeaders(HeaderNames.ContentType);
    });
}

// Configure the HTTP request pipeline
app.UseHttpsRedirection();

app.UseSession();
// Enable authentication middleware
app.UseAuthentication();
// Enable authorization middleware
app.UseAuthorization();
// Map controller routes
app.MapControllers();
// Execute the request pipeline
app.Run();
