using System;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Micros.Funny.Service.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error-local-development")]
        public ActionResult<ErrorDevResponse> ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            if (exception is CustomException ex)
            {
                Response.StatusCode = (int)ex.StatusCode;
                return new ErrorDevResponse(ex);
            }

            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return new ErrorDevResponse(exception);

            //return Problem(
            //    detail: context.Error.StackTrace,
            //    title: context.Error.Message);
        }


        [Route("error")]
        public ActionResult<ErrorResponse> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            if (exception is CustomException ex)
            {
                Response.StatusCode = (int) ex.StatusCode;
                return new ErrorResponse(ex);
            }

            Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return new ErrorResponse(exception);
        }
    }

    public class ErrorResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ErrorCode { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public ErrorResponse(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
        }

        public ErrorResponse(CustomException ex) : this((Exception)ex)
        {
            ErrorCode = ex.ErrorCode;
        }
    }

    public class ErrorDevResponse : ErrorResponse
    {
        public string StackTrace { get; set; }

        public ErrorDevResponse(Exception ex) : base(ex)
        {
            StackTrace = ex.ToString();
        }

        public ErrorDevResponse(CustomException ex) : this((Exception)ex)
        {
            ErrorCode = ex.ErrorCode;
        }
    }

    public class CustomException : Exception
    {
        public CustomException()
        { }

        public CustomException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public CustomException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public CustomException(HttpStatusCode statusCode, string message, Exception inner) : base(message, inner)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }

        public string ErrorCode { get; } = "err-0000";
    }

    public class ValidationException : CustomException
    {
        public ValidationException() : base(HttpStatusCode.BadRequest)
        {  }

        public ValidationException(string message) : base(HttpStatusCode.BadRequest, message)
        { }

        public ValidationException(string message, Exception inner) : base(HttpStatusCode.BadRequest, message, inner)
        { }
    }

    public class OperationException : CustomException
    {
        public OperationException() : base(HttpStatusCode.InternalServerError)
        { }

        public OperationException(string message) : base(HttpStatusCode.InternalServerError, message)
        { }

        public OperationException(string message, Exception inner) : base(HttpStatusCode.InternalServerError, message, inner)
        { }
    }
}