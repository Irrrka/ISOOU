namespace ISOOU.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ISOOU.Data.Common.Models;
    using ISOOU.Data.Models.Enums;

    public class District : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
