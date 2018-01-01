using System;
using System.Net;
using System.Security;

namespace QhTemplate.ApplicationCore.Exceptions
{
    public class UserFriendlyException:Exception
    {
        public HttpStatusCode HttpStatus { get; }

        public UserFriendlyException(string message,HttpStatusCode httpStatus=HttpStatusCode.InternalServerError):base(message)
        {
            this.HttpStatus = httpStatus;
        }
    }
}