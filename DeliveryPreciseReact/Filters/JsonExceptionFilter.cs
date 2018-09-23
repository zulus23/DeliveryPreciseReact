using DeliveryPreciseReact.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeliveryPreciseReact.Filters
{
    public class JsonExceptionFilter :IExceptionFilter
    {
        private readonly IHostingEnvironment _evn;

        public JsonExceptionFilter(IHostingEnvironment evn)
        {
            _evn = evn;
        }

        
        public void OnException(ExceptionContext context)
        {
            ApiError error = new ApiError();

            if (_evn.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;    
            }
            else
            {
                error.Message = "A Server error occurred."; 
                error.Detail = context.Exception.Message;
            }

            


            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}