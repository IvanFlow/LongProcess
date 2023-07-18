using LongRuning.BusinessLayer.Interfaces;
using System.Text;

namespace LongRuning.BusinessLayer.Services
{
    public class Base64EncoderService : IBase64EncoderService
    {
        public string Encode(string textToEncode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(textToEncode));
        }
    }
}
