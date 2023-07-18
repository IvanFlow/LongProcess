using LongRuning.BusinessLayer.Interfaces;
using LongRuning.BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace LongRuning.Tests.BusinessLayer.Services
{
    public class StreamStringServiceTests
    {
        private IResult _streamStringService;

        [SetUp]
        public void Setup()
        {
            var textToConvert = "abcd";
            _streamStringService = new StreamStringService(textToConvert);
        }

        [Test]
        public async Task GivenKnowInput_Encode_PredictedResult()
        {
            //Arrange
            var httpRequest = new DefaultHttpContext();
            var initialContentType = httpRequest.Response.ContentType;
            string initialResponseBody = string.Empty;
            string currentResponseBody = string.Empty;
            using (var reader = new StreamReader(httpRequest.Response.Body))
            {
                //Request.Body.Seek(0, SeekOrigin.Begin);
                //body = reader.ReadToEnd();
                initialResponseBody = await reader.ReadToEndAsync();
            }

            //Act
            await _streamStringService.ExecuteAsync(httpRequest);
            var currentContentType = httpRequest.Response.ContentType;
            using (var reader = new StreamReader(httpRequest.Response.Body))
            {
                //Request.Body.Seek(0, SeekOrigin.Begin);
                //body = reader.ReadToEnd();
                currentResponseBody = await reader.ReadToEndAsync();
            }

            //Assert
            Assert.Null(initialContentType);
            Assert.NotNull(currentContentType);
            Assert.AreEqual("text/plain", currentContentType);
            
        }
    }
}
