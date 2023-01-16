using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connString = builder.Configuration.GetConnectionString("AppDbContext");

builder.Services.AddNpgsql<AppDbContext>(connString,
    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));

builder.Services.AddControllers()
    .AddJsonOptions(o => { o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

builder.Services.AddRouting(o =>
{
    o.LowercaseUrls = true;
    o.LowercaseQueryStrings = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(p => p
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200"));

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// use wwwroot dir for html css assets
app.UseStaticFiles();

app.MapControllers();

app.Run();