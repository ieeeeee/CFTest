using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OA.Basis;
using OA.Services;
using WebGrease.Css.Extensions;

namespace OA.Web.App_Start
{
    public class AutoMapperConfig
    {
        private static MapperConfiguration _mapperConfiguration;

        public static void Register()
        {
            var moduleInitializers = new ModuleInitializer[]
            {
                new OAModuleInitializer(),
                new OAJobInitializer()

            };
            _mapperConfiguration = new MapperConfiguration(cfg =>
              {
                  moduleInitializers.ForEach(item => item.LoadAutoMapper(cfg));
              });
        }

        public static MapperConfiguration GetMapperConfiguration()
        {
            if (_mapperConfiguration == null)
                Register();

            return _mapperConfiguration;
        }
    }
}