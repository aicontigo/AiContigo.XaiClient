﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AiContigo.XaiClient.Extensions
{
    public static class XaiClientServiceCollectionExtensions
    {
        private const string XaiHttpClient = "XaiHttpClient";

        public static IServiceCollection AddXaiClient(this IServiceCollection services, Action<Configuration.XaiClientOptions> configure)
        {
            var options = new Configuration.XaiClientOptions();
            configure(options);

            services.AddHttpClient(XaiHttpClient)
                .ConfigureHttpClient(client =>
                {

                });

            services.AddSingleton<Http.IHttpClient>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(XaiHttpClient);
                return new Http.HttpClientWrapper(httpClient);
            });

            // ILogger will be provided by the consuming application
            services.AddSingleton(provider =>
            {
                var httpClient = provider.GetRequiredService<Http.IHttpClient>();
                var logger = provider.GetRequiredService<ILogger<XaiApiClient>>();
                return new XaiApiClient(options.ApiKey, options.BaseUrl, options.DefaultModel, httpClient, logger);
            });

            return services;
        }
    }
}
