using System;
using System.Threading.Tasks;

namespace WorldTimeAPI
{
    internal static class SyncHelper
    {
        public static void TryInvokeMethodSync(Func<Task> method)
        {
            var task = Task.Run(async () => await method().ConfigureAwait(false));
            task.Wait();
        }

        public static T TryInvokeMethodSync<T>(Func<Task<T>> method) 
        {
            var task = Task.Run(async () => await method().ConfigureAwait(false));
            task.Wait();
            return task.Result;
        }
    }
}