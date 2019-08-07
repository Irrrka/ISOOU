using ISOOU.Data.Common.Models;
using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISOOU.Services.Models
{
    public class QuestionServiceModel : IMapFrom<Question>, IMapTo<Question>
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public virtual SystemUserServiceModel User { get; set; }
    }
}