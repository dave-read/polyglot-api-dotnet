using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;

namespace ApiDotNet.Controllers
{
    [Route("/api/hello")]
    public class HelloController
    {
        private static int invokeCount = 0;
        [HttpGet]
        public string Get()
        {
            invokeCount++;
            var osDescription = RuntimeInformation.OSDescription;
            var machineName = Environment.MachineName;

            // TimeZoneInfo has unique Ids per OS
            // Querying the wrong string will throw
            TimeZoneInfo tzInfo = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                tzInfo = TimeZoneInfo.FindSystemTimeZoneById("America/Los_Angeles");

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                tzInfo = TimeZoneInfo.FindSystemTimeZoneById("America/Los_Angeles");

            else
                tzInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);

            var message = string.Format("Hello from api-dotnet. Running on [Host:{0}][OS:{1}][Tz:{2}][InvokeCount:{3}]",
                machineName, 
                osDescription,
                tzInfo.ToString(),
                invokeCount);

            return message;
        }
    }
}
