using Autofac;
using Castle.Core.Configuration;
using Data.Datas;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisPlatformRepo>().As<IRedisPlatformRepo>();
        }
    }
}
