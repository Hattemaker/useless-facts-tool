using UselessFactsTool.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddHttpClient<IFactService, FactService>();
builder.Services.AddSingleton<IOptimizelyService, OptimizelyService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.MapControllers();
app.MapGet("/", () => Results.Ok(new { message = "Useless Facts Tool API is running!" }));

app.Run();
