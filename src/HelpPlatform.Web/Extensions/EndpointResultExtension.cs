using Ardalis.Result;
using FastEndpoints;
using FluentValidation.Results;

namespace HelpPlatform.Web.Extensions;

public static class EndpointResultExtension
{
    public static async Task SendResponse<TResult, TResponse>(this IEndpoint ep, TResult result, Func<TResult, TResponse> mapper) where TResult : Ardalis.Result.IResult
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                await ep.HttpContext.Response.SendAsync(mapper(result));
                break;

            case ResultStatus.Invalid:
                foreach (var error in result.ValidationErrors)
                {
                    ep.ValidationFailures.Add(new ValidationFailure
                    {
                        ErrorCode = error.ErrorCode,
                        ErrorMessage = error.ErrorMessage
                    });
                }
                await ep.HttpContext.Response.SendErrorsAsync(ep.ValidationFailures);
                break;
            case ResultStatus.Created:
                break;
            case ResultStatus.Error:
                break;
            case ResultStatus.Forbidden:
                break;
            case ResultStatus.Unauthorized:
                break;
            case ResultStatus.NotFound:
                break;
            case ResultStatus.NoContent:
                break;
            case ResultStatus.Conflict:
                break;
            case ResultStatus.CriticalError:
                break;
            case ResultStatus.Unavailable:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public static async Task SendNoContent<TResult>(this IEndpoint ep, TResult result) where TResult : Ardalis.Result.IResult
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                await ep.HttpContext.Response.SendNoContentAsync();
                break;

            case ResultStatus.Invalid:
                foreach (var error in result.ValidationErrors)
                {
                    ep.ValidationFailures.Add(new ValidationFailure
                    {
                        ErrorCode = error.ErrorCode,
                        ErrorMessage = error.ErrorMessage
                    });
                }
                await ep.HttpContext.Response.SendErrorsAsync(ep.ValidationFailures);
                break;
            case ResultStatus.Created:
                break;
            case ResultStatus.Error:
                break;
            case ResultStatus.Forbidden:
                break;
            case ResultStatus.Unauthorized:
                break;
            case ResultStatus.NotFound:
                break;
            case ResultStatus.NoContent:
                break;
            case ResultStatus.Conflict:
                break;
            case ResultStatus.CriticalError:
                break;
            case ResultStatus.Unavailable:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
