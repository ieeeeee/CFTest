using Newtonsoft.Json;
using OA.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models.Filters
{
    /// <summary>
    /// 高级条件过滤器
    /// </summary>
    public class AdvanceFilter : BaseFilter
    {
        /// <summary>
        /// 连接符号
        /// </summary>
        public GroupOperator GroupOperator { get; set; }

        /// <summary>
        /// 规则
        /// </summary>
        public IList<RuleFilter> Rules { get; set; }
    }

    /// <summary>
    /// 规则过滤器
    /// </summary>
    public class RuleFilter
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 连接符号
        /// </summary>
        [JsonProperty("OperatorName")]
        public string Operater { get; set; }

        /// <summary>
        /// 查询值
        /// </summary>
        public string Data { get; set; }
    }

}
