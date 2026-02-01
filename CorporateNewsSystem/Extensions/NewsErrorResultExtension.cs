using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions
{
    public static class NewsErrorResultExtension
    {
        public static IActionResult ToActionResult<T>(
                                    this NewsResult<T> result,
                                    ControllerBase controller)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(result);
            }

            var error = result.Error!;

            return controller.StatusCode(
                error.Code,
               result);
        }
    }
}
