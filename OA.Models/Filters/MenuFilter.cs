using OA.Models.Enum;

namespace OA.Models.Filters
{
    public  class MenuFilter:BaseFilter
    {
        //排除的类型
        public MenuType? ExcludeType { get; set; }
    }
}