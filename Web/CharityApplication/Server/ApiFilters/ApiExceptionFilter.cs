using Application.Model;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace CharityApplication.Server.ApiFilters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(ValidationException))
            {
                HandleValidationException(context);
            }
            else if (context.Exception.GetType() == typeof(NotFoundRecordException))
            {
                HandleNotFoundRecordException(context);
            }
            else if (context.Exception.GetType() == typeof(SqlException))
            {
                HandleSqlException(context);
            }
            else if (context.Exception.GetType() == typeof(UnauthorizedAccessException))
            {
                HandleUnauthorizedAccessException(context);
            }
            else if (context.Exception.GetType() == typeof(InsufficientNumberEventParticipantsException))
            {
                HandleInsufficientNumberEventParticipantsException(context);
            }
            else if (context.Exception.GetType() == typeof(ToMuchMembersException))
            {
                HandleToMuchMembersException(context);
            }
            else
            {
                HandleNotSupportedException(context);
            }

            base.OnException(context);
        }

        private static void HandleNotFoundRecordException(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(
                CreateDetailMessage(
                   title: "There is no such data on the given parameters",
                   statusCode: HttpStatusCode.NotFound,
                   instance: context.HttpContext.Request.Path,
                   detail: context.Exception.Message
             ));
        }

        private static void HandleValidationException(ExceptionContext context)
        {
            var ex = context.Exception as ValidationException;
            var failures = ex.Errors
                .Where(x => x != null)
                .GroupBy(x => x.PropertyName)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Select(k => new ErrorModel { ErrorMessage = k.ErrorMessage, EnteredValue = k.AttemptedValue }).ToList().AsReadOnly());
            context.Result = new BadRequestObjectResult(
               CreateDetailMessage(
                   title: "Error in the submitted data",
                   statusCode: HttpStatusCode.BadRequest,
                   instance: context.HttpContext.Request.Path,
                   detail: JsonSerializer.Serialize(failures)
               ));
        }

        private static void HandleInsufficientNumberEventParticipantsException(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(
                CreateDetailMessage(
                   title: "Insufficient number of participants of the event in group",
                   statusCode: HttpStatusCode.Conflict,
                   instance: context.HttpContext.Request.Path,
                   detail: context.Exception.Message
             ));
        }

        private static void HandleToMuchMembersException(ExceptionContext context)
        {
            context.Result = new ObjectResult(
                            CreateDetailMessage(
                               title: "To much members in group or event",
                               statusCode: HttpStatusCode.Conflict,
                               instance: context.HttpContext.Request.Path,
                               detail: context.Exception.Message
                         ));
        }

        private static void HandleSqlException(ExceptionContext context)
        {
            SqlException ex = context.Exception as SqlException;
            context.Result = new ObjectResult(CreateDetailMessage(
                   title: "Database Exception",
                   statusCode: HttpStatusCode.InternalServerError,
                   instance: context.HttpContext.Request.Path,
                   detail: context.Exception.Message
               ));
        }

        private static void HandleNotSupportedException(ExceptionContext context)
        {
            context.Result = new ObjectResult(CreateDetailMessage(
                    title: "An error occurred while processing your request.",
                    statusCode: HttpStatusCode.InternalServerError,
                    instance: context.HttpContext.Request.Path,
                    detail: $"{context.Exception.Message} - Inner Exception: {(context.Exception.InnerException is not null ? context.Exception.InnerException.Message : string.Empty)}"
                ));
        }

        private static void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            context.Result = new UnauthorizedObjectResult(CreateDetailMessage(
                    title: "Unauthorized Access",
                    statusCode: HttpStatusCode.Unauthorized,
                    instance: context.HttpContext.Request.Path,
                    detail: context.Exception.Message
                ));
        }

        private static ProblemDetails CreateDetailMessage(string title, HttpStatusCode statusCode, string instance, string detail)
        {
            return new ProblemDetails
            {
                Title = title,
                Status = (int)statusCode,
                Instance = instance,
                Detail = detail
            };
        }
    }
}