using LongRuning.BusinessLayer.Interfaces;
using LongRuning.BusinessLayer.Services;

namespace LongRuning.Tests.BusinessLayer.Services
{
    public class Base64EncoderServiceTests
    {
        private IBase64EncoderService _base64EncoderService;

        [SetUp]
        public void Setup()
        {
            _base64EncoderService = new Base64EncoderService();
        }

        [Test]
        public void GivenKnowInput_Encode_PredictedResult()
        {
            //Arrange
            var textToEncode = "abcd";

            //Act
            
            var result = _base64EncoderService.Encode(textToEncode);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual("YWJjZA==", result);
        }
    }
}
