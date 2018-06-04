using AutoMapper;
using OA.Basis;
using OA.Basis.Extentions;
using OA.Data.Entity;
using OA.Interfaces.Task;
using OA.Models;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Services
{
    public class OAJobInitializer: ModuleInitializer
    {
        /// <summary>
        /// 加载SimpleInject配置
        /// </summary>
        /// <param name="container"></param>
        public override void LoadIoc(Container container)
        {
            var registrations =
               from type in typeof(OAJobInitializer).Assembly.GetTypes()
               where
                   type.Namespace != null && (type.Namespace.IsNotBlank() &&
                                              type.Namespace.StartsWith("OA.Services.TaskServices") &&
                                              type.GetInterfaces().Any(x => x == typeof(IRecurringTask)))
               select type;

            foreach (var jobType in registrations)
            {
                container.Register(jobType, jobType, Lifestyle.Scoped);
            }
        }
        //加载AutoMapper配置
        public override void LoadAutoMapper(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<W_PlanListEntity, PlanDto>().ReverseMap();

        }

    }
}
