using CustomTokenBasedAuthentication.Business.Enums;
using CustomTokenBasedAuthentication.Business.Models;

namespace CustomTokenBasedAuthentication.Business.Helpers
{
    public static class StaticHelpers
    {
        public static ResponseDetails SetResponSeDetails(bool success, object data, MessageType messageType,
            string message = "Exception Encountered")
        {
            return new ResponseDetails
            {
                Success = success,
                Data = data,
                Message = message,
                MessageType = messageType
            };
        }
    }
}
