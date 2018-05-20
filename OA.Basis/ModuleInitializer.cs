using AutoMapper;
using SimpleInjector;
namespace OA.Basis
{
    //模块初始化
    public abstract class ModuleInitializer
    {
        //加载SimpleInject配置
        public abstract void LoadIoc(Container container);
        //加载AutoMapper配置
        public abstract void LoadAutoMapper(IMapperConfigurationExpression mapperConfig);
    }
}