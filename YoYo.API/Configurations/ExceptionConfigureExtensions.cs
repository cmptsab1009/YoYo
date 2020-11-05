using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoYo.Core.ViewModel;

namespace YoYo.API.Configurations
{
    public static class ExceptionConfigureExtensions
    {
        public static void UseExceptionConfigure(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError(GetExceptionMessage("Global Unhandled Exception", contextFeature.Error));
                        await context.Response.WriteAsync(new HttpResponseErrorModel()
                        {
                            Id = "",
                            Code = "Internal Server Error.",
                            Status = (int)HttpStatusCode.InternalServerError,
                            Title = "",
                            Detail = contextFeature.Error.Message,
                            Path = context.Request.Path,
                            StackTrace = contextFeature.Error.StackTrace,
                        }.ToString()).ConfigureAwait(false);
                    }
                });
            });
        }

        public static string GetExceptionMessage(string gettingFrom, Exception ex)
        {
            return ex == null ? string.Empty : ($"{gettingFrom}, Message : {Regex.Replace(ex.Message, Environment.NewLine, " ")}, StackTrace : {Regex.Replace(ex.StackTrace, Environment.NewLine, " ")}");
        }
    }
}
