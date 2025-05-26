using BlogPlatform.API.Endpoints;
using BlogPlatform.Application.UseCases;
using BlogPlatform.Domain.Interfaces;
using BlogPlatform.Domain.Services;
using BlogPlatform.Persistence.Data;
using BlogPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();


builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<CreateBlogPostUseCase>();
builder.Services.AddScoped<UpdateBlogPostUseCase>();
builder.Services.AddScoped<GetBlogPostUseCases>();

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
app.MapBlogPostEndpoints();

app.Run();
