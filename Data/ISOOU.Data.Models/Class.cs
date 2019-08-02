namespace ISOOU.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ISOOU.Data.Common.Models;

    public class Class : BaseModel<int>
    {
        [Required]
        public ClassLanguageType Profile { get; set; }

        public int FreeSpots { get; set; }

        public virtual ICollection<SchoolClass> SchoolClasses { get; set; }

    }
}
