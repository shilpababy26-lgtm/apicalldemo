var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options => //Define which origins (client URLs) are allowed
{
    options.AddPolicy("AllowMvcApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:7000/") // MVC app URL (MVC client URL)
                  .AllowAnyHeader()
                  .AllowAnyMethod(); //Enables all HTTP verbs and headers
        });
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
