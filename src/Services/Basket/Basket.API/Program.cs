var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//Add services to the container.
app.MapGet("/", () => "Hello World!");

//Configure the HTTP request pipeline.

app.Run();
