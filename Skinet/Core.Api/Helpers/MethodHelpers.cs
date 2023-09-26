using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.Api.Helpers
{
    public class MethodHelpers
    {
        public static T SafeInvoke<T>(T task, ILogger logger)
        {
            var result = default(T);

            try
            {
                result = task;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }

            return result;
        }

        public static async Task<T> SafeInvoke<T>(Task<T> task, ILogger logger)
        {
            var result = default(T);

            try
            {
                result = await task;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }

            return result;
        }

        public static async Task<bool> SafeInvoke(Task task, ILogger logger)
        {
            var isSuccessful = false;

            try
            {
                await task;
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }

            return isSuccessful;
        }
    }
}
