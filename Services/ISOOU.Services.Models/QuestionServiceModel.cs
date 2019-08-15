namespace ISOOU.Services.Models
{
    using ISOOU.Data.Common.Models;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class QuestionServiceModel : IMapFrom<Question>, IMapTo<Question>
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string SystemUserId { get; set; }

        public SystemUserServiceModel SystemUser { get; set; }
    }
}