using EmployeeManagement.API.Shared;

namespace EmployeeManagement.API.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToProblemDetails(this Result result)
        {
            if (result.IsSuccess) throw new InvalidOperationException();

            return Results.Problem(
                statusCode: GetStatusCode(result.Error.ErrorType),
                title: GetTitle(result.Error.ErrorType),
                extensions: new Dictionary<string, object?>
                {
                    {"errors", new[] {result.Error } }
                }
            );
        }

        static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        static string GetTitle(ErrorType errorType) =>
     errorType switch
     {
         ErrorType.Validation => "Bad Request",
         ErrorType.Unauthorized => "Unauthorized",
         ErrorType.Forbidden => "Forbidden",
         ErrorType.NotFound => "Not Found",
         ErrorType.Conflict => "Conflict",
         _ => "Internal Server Error"
     };

        public static async Task<T> Match<T>(this Task<Result> resulttask, Func<T> OnSuccess, Func<T> OnFailure)
        {
            Result result = await resulttask;

            return result.IsSuccess ? OnSuccess() : OnFailure();
        }

        public static T Match<T>(this Result result, Func<T> OnSuccess, Func<T> OnFailure)
        {
            return result.IsSuccess ? OnSuccess() : OnFailure();
        }
    }


}
