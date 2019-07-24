using ISOOU.Data.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISOOU.Data.Models
{
    public class Question : BaseModel<int>
    {
        [Required]
        [MinLength(3)]
        public string Subject { get; set; }

        [Required]
        [MinLength(10)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public string SystemUserId { get; set; }

        public virtual SystemUser User { get; set; }
    }
}