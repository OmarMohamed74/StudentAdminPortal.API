using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Repositories;
using StudentAdminPortal.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext configuration

var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StudentAdminContext>(options =>
options.UseSqlServer(connectionStrings));

//AutoMapper Service
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
