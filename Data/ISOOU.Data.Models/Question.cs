using ISOOU.Data.Common.Models;

namespace ISOOU.Data.Models
{
    public class Question : BaseModel<int>
    {
        public string Subject { get; set; }

        public string Content { get; set; }

        public string SystemUserId { get; set; }

        public virtual SystemUser User { get; set; }
    }
}