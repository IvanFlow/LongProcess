using LongRuning.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LongRuning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncoderController : ControllerBase
    {
        private readonly IBase64EncoderService _base64EncoderService;

        public EncoderController(IBase64EncoderService base64EncoderService)
        {
            _base64EncoderService = base64EncoderService ?? throw new ArgumentNullException(nameof(base64EncoderService));
        }

        [HttpGet("EncodeBase64V1")]
        public async Task EncodeBase64(string textToEncode)
        {
            var outputStream = this.Response.Body;

            var textEncoded = _base64EncoderService.Encode(textToEncode);

            Response.Headers.Add("Content-Type", "text/event-stream");


            foreach (var c in textEncoded.ToCharArray())
            {
                Random rnd = new Random();
                int delayInSeconds = rnd.Next(1, 5);
                await Task.Delay(delayInSeconds * 1000);

                byte[] bytes = Encoding.ASCII.GetBytes(c.ToString());

                await outputStream.WriteAsync(bytes);

                await outputStream.FlushAsync();

            }

        }
    }
}
