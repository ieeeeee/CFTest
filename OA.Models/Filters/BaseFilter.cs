namespace OA.Models.Filters
{
    //基本过滤器
    public class BaseFilter
    {
        //排序字段
        public string sidx { get; set; }

        //类型
        public string sord { get; set; }

        //当前页码
        public int page { get; set; }

        //每页显示的数量
        public int rows { get; set; }

        //搜索关键字
        public string keywords { get; set; }

    }
}