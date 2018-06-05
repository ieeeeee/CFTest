namespace OA.Models
{
    public class TreeDto
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool open { get; set; }

        ///// <summary>
        ///// 是否是父节点
        ///// </summary>
        //public bool isParent { get; set; }
    }
}