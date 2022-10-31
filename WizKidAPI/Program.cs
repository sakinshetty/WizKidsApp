using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WizKidAPI.Client;
using WizKidAPI.Database;
using WizKidAPI.Repository;
using WizKidAPI.Service;
using WizKidAPI.Utilities;
using HttpClientFactory = WizKidAPI.Client.HttpClientFactory;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    //options.AddPolicy(name: MyAllowSpecificOrigins,
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5036")
                        // .AllowAnyMethod()
                        // .AllowAnyOrigin()
                         .AllowAnyHeader()
                         ;
                      });
});

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddScoped<HttpClientFactory>();
builder.Services.AddDbContext<WordDBContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("WordDictoniary")));
//builder.Services.AddDbContext<WordDBContext>();
builder.Services.AddScoped<IWordPredictionService, WordPredicitionService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new APIMapper()); });
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
