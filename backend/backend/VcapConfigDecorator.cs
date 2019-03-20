using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace backend
{
    public class VcapConfigDecorator : IConfiguration
    {
        private readonly IConfiguration configuration;

        public VcapConfigDecorator(IConfiguration configuration)
        {
            this.configuration = configuration;

            string vcapServices = Environment.GetEnvironmentVariable("VCAP_SERVICES");
            if (vcapServices != null)
            {
                dynamic json = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(vcapServices);
                if (json.ContainsKey("compose-for-mysql"))
                {
                    var uri = new Uri((string)json["compose-for-mysql"][0].credentials.uri);
                    var host = uri.Host;
                    var port = uri.Port;
                    var user = uri.UserInfo.Split(":")[0];
                    var password = uri.UserInfo.Split(":")[1];
                    var db = uri.LocalPath.TrimStart('/');
                    this.configuration["ConnectionStrings:Default"] = $"Server={host};Port={port};Uid={user};Pwd={password};Database={db};Charset=utf8;";
                }
            }
        }

        public string this[string key] { get => configuration[key]; set => configuration[key] = value; }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return configuration.GetChildren();
        }

        public IChangeToken GetReloadToken()
        {
            return configuration.GetReloadToken();
        }

        public IConfigurationSection GetSection(string key)
        {
            return configuration.GetSection(key);
        }
    }
}
