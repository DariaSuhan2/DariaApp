//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Cors;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{ 
    options.AddPolicy(name: "_myAllowSpecificOrigins", 
            builder =>
            {
                builder.WithOrigins("http://localhost:4200") 
                    .AllowAnyHeader() 
                    .AllowAnyMethod(); 
            });
});

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEverything", // This is the open house we talked about!
           builder =>
           {
               builder.AllowAnyOrigin() // Any origin is welcome...
                   .AllowAnyHeader() // With any type of headers...
                   .AllowAnyMethod(); // And any HTTP methods. Such a jolly party indeed!
           });
});*/
var app = builder.Build();


/*builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
        builder.WithOrigins("http://localhost:4200")
        //builder.AllowAnyOrigin())
            .AllowAnyHeader()
            .AllowAnyMethod()));
/*void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("AllowEverything", // This is the open house we talked about!
            builder =>
            {
                builder.AllowAnyOrigin() // Any origin is welcome...
                    .AllowAnyHeader() // With any type of headers...
                    .AllowAnyMethod(); // And any HTTP methods. Such a jolly party indeed!
            });
    });
}*/


/*app.UseCors(builder =>
        builder
        .WithOrigins("http://localhost:4200/")
        .AllowAnyMethod()
        .AllowAnyHeader());*/




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.UseRouting();

//app.UseCors();
app.UseCors("_myAllowSpecificOrigins");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



/*pp.Run(async (context) =>
{
     await context.Response.WriteAsync("Hello World!");
});*/


app.Run();
