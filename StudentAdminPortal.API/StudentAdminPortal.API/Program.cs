using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Repositories;
using StudentAdminPortal.API.Repositories.Interfaces;
using StudentAdminPortal.API.Validators;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    // Automatic registration of validators in assembly
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext configuration

var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StudentAdminContext>(options =>
options.UseSqlServer(connectionStrings));

//AutoMapper Service
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//FluentValidation



// CORS

builder.Services.AddCors((options) =>
{
    options.AddPolicy("AngularApplication", (builder) =>
                            {
        builder.WithOrigins("http://localhost:4200/").AllowAnyOrigin()
                                                     .AllowAnyHeader()
                                                     .WithMethods("GET", "POST", "PUT", "DELETE")
                                                     .WithExposedHeaders("*");

                                });

});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AngularApplication");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath,"Resources")),
    RequestPath = "/Resources"
});


app.UseAuthorization();

app.MapControllers();

app.Run();
