using LongRuning.BusinessLayer.Interfaces;
using LongRuning.BusinessLayer.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IBase64EncoderService, Base64EncoderService>();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy
    (
        "AllowOrigin",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("AllowOrigin");
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.MapGet("/Encoder/EncodeBase64V2", (string textToEncode) => new StreamStringService(textToEncode));

app.MapGet("/Encoder/EncodeBase64V3", (string textToEncode) => {
    static async  IAsyncEnumerable<char> GetEncoded64(string textToEncode)
    {
        var textEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(textToEncode));

        foreach (var c in textEncoded.ToCharArray())
        {
            Random rnd = new Random();
            int delayInSeconds = rnd.Next(1, 5);
            await Task.Delay(delayInSeconds * 1000).ConfigureAwait(false);

            yield return c;
        }
    }
    return Results.Ok(GetEncoded64(textToEncode));
});

app.Run();
