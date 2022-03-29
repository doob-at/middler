using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using doob.middler;
using doob.middler.Action.Scripting;
using doob.middler.Action.Scripting.Models;
using doob.middler.Action.UrlRedirect;
using doob.middler.Common.SharedModels.Enums;
using doob.middler.ExtensionMethods;
using doob.middler.Helper;
using doob.Reflectensions;
using doob.Reflectensions.ExtensionMethods;
using doob.Scripter;
using doob.Scripter.Engine.Javascript;
using doob.Scripter.Engine.Powershell;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Xunit;

namespace middler.Tests
{
    public class JavaScriptScriptingActionTest
    {
        IHost _host;
        HttpClient _client;

        public JavaScriptScriptingActionTest()
        {
            _host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {

                            services.AddRouting();

                            services.AddScripter(context =>
                                context
                                    .AddJavaScriptEngine()
                                    .AddPowerShellCoreEngine()
                                    .AddScripterModule<EndpointModule>()
                            );

                            services.AddMiddler(opts =>
                                opts.AddScriptingAction()
                                    .AddUrlRedirectAction()
                                    .SetDefaultAccessMode(AccessMode.Allow)
                            );



                            services.AddSingleton<FileSystemRepo>(sr =>
                                new FileSystemRepo(
                                    PathHelper.GetFullPath("FSRules", Path.GetDirectoryName(this.GetType().Assembly.Location)),
                                    fsmap => fsmap
                                        .Set("js", (info, s) =>
                                        {
                                            var action = new ScriptingAction(sr);
                                            action.Parameters = new ScriptingOptions
                                            {
                                                Language = "JavaScript",
                                                SourceCode = s
                                            };

                                            return action.ToBasicMiddlerAction();
                                        })
                                        .Set("ps1", (info, s) =>
                                        {
                                            var action = new ScriptingAction(sr);
                                            action.Parameters = new ScriptingOptions
                                            {
                                                Language = "PowerShellCore",
                                                SourceCode = s
                                            };

                                            return action.ToBasicMiddlerAction();
                                        })
                                ));
                        })
                        .Configure(app =>
                        {

                            app.UseRouting();

                            app.UseMiddler(builder =>
                                {
                                    builder.AddRepo<FileSystemRepo>();
                                    builder.On("js",
                                        actions => actions.InvokeScript(new ScriptingOptions()
                                        {
                                            Language = "JavaScript",
                                            SourceCode = "var end = require('Endpoint');end.Response.Ok([true, false]);"
                                        }));
                                    builder.On("redirect", actions => actions.RedirectTo("redirected"));
                                }

                            );
                        });
                })
                .Start();

            _client = _host.GetTestClient();
        }

        [Fact]
        public async Task Simple1JavaScript()
        {

            var response = await _client.GetAsync("/js/simple1.js");
            var content = await response.Content.ReadAsStringAsync();
            var val = Json.Converter.ToObject<object[]>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True((bool)val[0]);
            Assert.False((bool)val[1]);
            Assert.Equal(42, val[2].Reflect().To<int>());


        }

        [Fact]
        public async Task Simple1Powershell()
        {

            var response = await _client.GetAsync("/ps/simple1.ps1");
            var content = await response.Content.ReadAsStringAsync();
            var val = Json.Converter.ToObject<object[]>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True((bool)val[0]);
            Assert.False((bool)val[1]);
            Assert.Equal(42, val[2].Reflect().To<int>());


        }

        [Fact]
        public async Task ReturnNotFoundJavaScript()
        {

            var response = await _client.GetAsync("/js/simple2.js");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Fact]
        public async Task ReturnNotFoundPowershell()
        {

            var response = await _client.GetAsync("/ps/simple2.ps1");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Fact]
        public async Task SimpleRedirect()
        {

            var respone = await _client.GetAsync("/redirect");
            Assert.Equal("http://localhost/redirected", respone.Headers.Location?.ToString());

        }
    }
}
