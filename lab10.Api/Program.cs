using System.Text;
using lab10.Application;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using lab10.Application.UsesCases.Response.Command;
using lab10.Application.UsesCases.Role.Command;
using lab10.Application.UsesCases.Ticket.Command;
using lab10.Application.UsesCases.UserEntity.Command;
using lab10.Application.UsesCases.UserRole.Command;
using lab10.Domain.Entities;
using Microsoft.OpenApi;

using lab10.Infrastructure.Configuration;
using lab10.Infrastructure.Persistence;
using lab10.Infrastructure.Repositories;
using lab10.Infrastructure.Reports;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Laboratorio 10 - API y reportes Excel", 
        Version = "v1" 
    });

    // Mostramos en Swagger la explicación de cada reporte.
    var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa únicamente tu token JWT (sin escribir la palabra 'Bearer')"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });
});

// Base de Datos
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql
    (builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

//ServicesRegister
builder.Services.AddScoped<IResponseRepository, ResponseRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITIcketRepository, TIcketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IExcelReportService, ExcelReportService>();
//Mediator 
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(ApplicationAssemblyMarker).Assembly));

//Mapper 
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<CreateUserUseCaseCommand, User>();
    cfg.CreateMap<UpdateUserUseCaseCommand, User>();
    cfg.CreateMap<CreateResponseUseCaseCommand, Response>();
    cfg.CreateMap<UpdateResponseUseCaseCommand, Response>();
    cfg.CreateMap<CreateRoleUseCaseCommand, Role>();
    cfg.CreateMap<UpdateRoleUseCaseCommand, Role>();
    cfg.CreateMap<CreateTicketUseCaseCommand, Ticket>();
    cfg.CreateMap<UpdateTicketUseCaseCommand, Ticket>();
    cfg.CreateMap<CreateUserRoleUseCaseCommand, UserRole>();
    cfg.CreateMap<UpdateUserRoleUseCaseCommand, UserRole>();
});

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWTVariable:Issuer"],
            ValidAudience = builder.Configuration["JWTVariable:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTVariable:Secret"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();


if (   app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "mi api");
    c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

