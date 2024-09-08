using api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<APIContext>(options => {
options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")
));});


// Configure CORS to allow requests from your frontend (http://localhost:5174)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:5174"); // Allow requests from frontend
        policyBuilder.AllowAnyHeader(); // Allow any headers
        policyBuilder.AllowAnyMethod();  // Allow any methods (GET, POST, etc.)
        policyBuilder.AllowCredentials(); // Allow credentials (cookies, etc.)
        policyBuilder.WithExposedHeaders("Access-Control-Allow-Origin"); // Expose specific headers
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("AllowFrontend");
app.Run();
