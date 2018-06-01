using System.ComponentModel.DataAnnotations;

namespace OA.Models
{
    public class BaseInfoDto
    {
        public int BaseClassID { get; set; }

        [Display(Name = "字典分类名称")]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string BaseClassName { get; set; }


        public int BaseInfoID { get; set; }

        [Display(Name = "字典名称")]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string BaseName { get; set; }

        [Display(Name = "字典值")]
        [Required(ErrorMessage = Message.Required)]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string BaseValue { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public int IsDeleted { get; set; }
    }
}