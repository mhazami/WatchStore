using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClockStore.DTO.Enums;

namespace ClockStore.DTO
{
    public partial class Product
    {
        [Key]
        public Guid ProductId { get; set; }


        [Required(ErrorMessage ="لطفا نام محصول را وارد کنید")]
        [MaxLength(100)]
        [Display(Name ="نام محصول")]
        public string ProductName { get; set; }


        [Required]
        public Guid FileId { get; set; }
        public virtual File File { get; set; }

        
        [Required(ErrorMessage = "لطفا قیمت محصول را وارد کنید")]
        public decimal Price { get; set; }

        public int? Off { get; set; }


        public int Count { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "لطفا کد محصول را وارد کنید")]
        public string Code { get; set; }

        public bool? IsDeals { get; set; }

        public bool? IsNewSeason { get; set; }

        [Required(ErrorMessage = "لطفا زبان را وارد کنید")]
        [MaxLength(5)]
        public string LangId { get; set; }

        public bool? IsBlocked { get; set; }


        public bool? Isluxury { get; set; }

        public OfferType? OfferType { get; set; }


        [Required(ErrorMessage = "لطفا برند محصول را وارد کنید")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        //نوع(آنالوگ / دیجیتال)
        public WatchType? WatchType { get; set; }

        //نوع
        public Enums.Type Type { get; set; }

        //دسته بندی کلی
        public MasterCategory? MasterCategory { get; set; }

        //حنسیت
        public ProductSex? Sex { get; set; }

        //اصالت
        public Orginal? Orginal { get; set; }

        //کشور سازنده
        public string ManufacturingCountry { get; set; }

        //موتور
        public Motor? Motor { get; set; }

        //منبع انرژی
        public string EnergySource { get; set; }


        //فرم صفحه
        public string PageForm { get; set; }

        //رنگ
        public string Color { get; set; }

        //ضخامت بدنه
        public int? ThicknessBody { get; set; }

        //جنس پشت قاب
        public string BackFrame { get; set; }

        //قطر قاب / عرض قاب
        public int? FrameWidth { get; set; }

        //حنس شیشه
        public string Glass { get; set; }

        //جنس بزل
        public string Jerk { get; set; }

        //رنگ بزل
        public string JerkColor { get; set; }

        //جنس بدئه
        public string BodyMaterial { get; set; }

        //رنگ بدنه
        public string BodyColor { get; set; }

        //عرض بدنه
        public int? BodyWidth { get; set; }

        //رنگ صفحه
        public string PageColor { get; set; }

        //نوع قفل
        public string LockType { get; set; }

        //کرنوگراف
        public bool? Cranograph { get; set; }

        //مقاومت در برابر آب (متر)

        public int? WaterProf { get; set; }

        //وزن
        public int? Weight { get; set; }

        public Guid? DesktopId { get; set; }
        public virtual Desktop Desktop { get; set; }

        public Guid? ClockId { get; set; }
        public virtual Clock Clock { get; set; }

    }
}
