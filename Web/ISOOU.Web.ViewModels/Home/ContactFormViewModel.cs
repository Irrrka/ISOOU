using ISOOU.Services.Mapping;
using ISOOU.Services.Models;

namespace ISOOU.Web.ViewModels.Home
{
    public class ContactFormViewModel : IMapTo<QuestionServiceModel>, IMapFrom<QuestionServiceModel>
    {
        public string UserEmail { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

    }
}
