﻿using AutoMapper;
using OA.Basis;
using OA.Basis.Extentions;
using OA.Data.Entity;
using OA.Models;
using SimpleInjector;
using System.Linq;

namespace OA.Services
{
    public class OAModuleInitializer:ModuleInitializer
    {
        /// <summary>
        /// 加载SimpleInject配置
        /// </summary>
        /// <param name="container"></param>
        public override void LoadIoc(Container container)
        {
            var registrations =
                from type in typeof (OAModuleInitializer).Assembly.GetTypes()
                where
                    type.Namespace != null && (type.Namespace.IsNotBlank() &&
                                               type.Namespace.StartsWith("OA.Services.AppServices") &&
                                               type.GetInterfaces().Any(x => x.Name.EndsWith("Service")) &&
                                               type.GetInterfaces().Any())
                select new { Service = type.GetInterfaces().First(), Implementation = type };

            foreach (var reg in registrations)
            {
                container.Register(reg.Service, reg.Implementation, Lifestyle.Scoped);
            }
        }
        //加载AutoMapper配置
        public override void LoadAutoMapper(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<B_UserEntity, UserDto>().ReverseMap();
        }
    }
}