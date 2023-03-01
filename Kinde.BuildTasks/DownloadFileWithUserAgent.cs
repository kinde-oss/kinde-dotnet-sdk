using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;
using Task = Microsoft.Build.Utilities.Task;




namespace Kinde.BuildTasks
{
    /// <summary>
    /// Represents a task that can download a file.
    /// </summary>
    public sealed class DownloadFileWithUserAgent : Microsoft.Build.Utilities.Task, ICancelableTask
    {
        [Required]
        public string SourceUrl { get; set; }
        [Required]
        public string DestinationPath { get; set; }

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();


        public async Task<bool> ExecuteAsync()
        {
            var token = cancellationTokenSource.Token;
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");
                    var fileBytes = await client.GetByteArrayAsync(SourceUrl);
                    File.WriteAllBytes(DestinationPath, fileBytes);
                }
                return true;

            }
            catch (TaskCanceledException ex) when (ex.CancellationToken == token)
            {
                return false;
            }

        }
        public void Cancel()
        {
            cancellationTokenSource.Cancel();
        }

        public override bool Execute()
        {
            return ExecuteAsync().GetAwaiter().GetResult();
        }
    }
}