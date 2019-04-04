using System.ComponentModel.DataAnnotations;
using static ClockStore.DTO.Enums;

namespace ClockStore.DTO
{
    public class OfferCard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "لطفا نوع تخفیف را مشخص نمایید")]
        [Display(Name = "نوع تخفیف")]
        public OfferType OfferType { get; set; }

        [Required(ErrorMessage ="لطفا کد تخفیف را مشخص نمایید")]
        [Display(Name ="کد تخفیف")]
        public string OfferCode { get; set; }

        [Display(Name ="مقدار تخفیف")]
        public int OfferAmount { get; set; }

        [Display(Name ="فعال است ؟")]
        public bool Enable { get; set; }
    }
}
