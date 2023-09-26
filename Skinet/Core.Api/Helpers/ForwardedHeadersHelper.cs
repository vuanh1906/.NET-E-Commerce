using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace Core.Api.Helpers
{
    public static class ForwardedHeadersHelper
    {
        public static ForwardedHeadersOptions GetOptions(string forwardedHostHeaderName, string forwardedHeadersAllowedHosts)
        {
            var options = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
            };

            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();

            if (!string.IsNullOrEmpty(forwardedHostHeaderName)) //set to "X-Original-Host" for azure application gateway.
                options.ForwardedHostHeaderName = forwardedHostHeaderName;

            if (!string.IsNullOrEmpty(forwardedHeadersAllowedHosts))
            {
                options.AllowedHosts = forwardedHeadersAllowedHosts.Split(',');
            }

            return options;
        }
    }
}
