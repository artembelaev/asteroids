using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidGame
{
    public static class AsyncOperationExtensions
    {
        public static Task AsTask(this AsyncOperation asyncOperation, CancellationToken cancellationToken = default)
        {
            if (asyncOperation == null)
                throw new ArgumentNullException(nameof(asyncOperation));

            var tcs = new TaskCompletionSource<object>();

            // Handle completion of the asynchronous operation
            void HandleCompletion(AsyncOperation op)
            {
                asyncOperation.completed -= HandleCompletion; // Unsubscribe from the event

                if (cancellationToken.IsCancellationRequested)
                    tcs.SetCanceled();
                else if (asyncOperation.isDone)
                    tcs.SetResult(null);
                else
                    tcs.SetException(new InvalidOperationException("AsyncOperation was not successfully completed."));
            }

            // Subscribe to the completion event
            asyncOperation.completed += HandleCompletion;

            // Handle cancellation request
            cancellationToken.Register(() =>
            {
                asyncOperation.completed -= HandleCompletion; // Unsubscribe from the event
                tcs.SetCanceled();
            });

            return tcs.Task;
        }
    }





}