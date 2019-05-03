using CustomTokenBasedAuthentication.Business.Enums;

namespace CustomTokenBasedAuthentication.Business.Models
{
    public class ResponseDetails
    {
        public bool Success { get; set; }

        public MessageType MessageType { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }
}
