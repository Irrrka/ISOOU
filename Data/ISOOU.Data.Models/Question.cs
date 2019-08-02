using ISOOU.Data.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISOOU.Data.Models
{
    public class Question : BaseModel<int>
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public string SystemUserId { get; set; }

        public virtual SystemUser User { get; set; }
    }
}