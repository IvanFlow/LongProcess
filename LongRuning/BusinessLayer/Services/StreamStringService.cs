using System.Text;

namespace LongRuning.BusinessLayer.Services
{
    public sealed class StreamStringService : IResult
    {
        private readonly string textToEncode = "";

        public StreamStringService(string text)
        {
            textToEncode = text;
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "text/plain";

            var textEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(textToEncode));

            foreach (var c in textEncoded.ToCharArray())
            {
                Random rnd = new Random();
                int delayInSeconds = rnd.Next(1, 5);
                await Task.Delay(delayInSeconds * 1000);

                await httpContext.Response.WriteAsync(c.ToString());

                //await httpContext.Response.Body.FlushAsync();

            }
        }
    }
}
