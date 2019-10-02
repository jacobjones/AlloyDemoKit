using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace AlloyDemoKit.Locations
{
    public class LocationContent
    {
        [CultureSpecific]
        [Required]
        [Display(Name = "Name", Order = 10)]
        public virtual string LocationName { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Description", Order = 20)]
        public virtual string Description { get; set; }

        [Required]
        [Display(Name = "Address Line 1", Order = 30)]
        public virtual string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2", Order = 40)]
        public virtual string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "City", Order = 50)]
        public virtual string City { get; set; }

        [Display(Name = "State or County", Order = 60)]
        public virtual string Subdivision { get; set; }

        [Required]
        [Display(Name = "Postal Code", Order = 70)]
        public virtual string PostalCode { get; set; }

        [Required]
        [Display(Name = "Country", Order = 0)]
        public virtual string Country { get; set; }
    }
}