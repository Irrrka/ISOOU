namespace ISOOU.Data.Models
{
    using System.ComponentModel;

    public enum AdmissionProcedureStatus
    {
        Finished = 10,

        [Description("Първо класиране")]
        First = 1,
        [Description("Второ класиране")]
        Second = 2,
        [Description("Трето класиране")]
        Third = 3,
    }
}