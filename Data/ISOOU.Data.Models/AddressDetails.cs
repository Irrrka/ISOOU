namespace ISOOU.Data.Models
{
    public class AddressDetails
    {
        public AddressDetails()
        {
            this.Current = this.Permanent;
        }

        public int Id { get; set; }

        public string Permanent { get; set; }

        public string Current { get; set; }

        public virtual District District { get; set; }

        public string Quarter { get; set; }
    }
}