using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClockStore.DTO
{
    public class Enums
    {
        public enum VideoPosition : byte
        {
            [Description("تاره ها")]
            [Display(Name = "تاره ها")]
            NewSeason = 1,
            [Description("حراجی")]
            [Display(Name = "حراجی")]
            Deals = 2,
            [Description("لاکچری")]
            [Display(Name = "لاکچری")]
            Luxtury = 3
        }

        public enum OfferType : byte
        {
            [Description("یکی بخر دوتا ببر")]
            [Display(Name = "یکی بخر دوتا ببر")]
            TwoByOne = 1,
            [Description("درصدی")]
            [Display(Name = "درصدی")]
            percent = 2,
            [Description("مبلغی")]
            [Display(Name = "مبلغی")]
            Price
        }

        public enum PaymentStatus : byte
        {
            [Display(Name = "اطلاعات ارسال شده ناقص است.")]
            Status1 = 1,
            [Display(Name = "و يا مرچنت كد پذيرنده صحيح نيست.IP")]
            Status2 = 2,
            [Display(Name = "با توجه به محدوديت هاي شاپرك امكان پرداخت با رقم درخواست شده ميسر نمي باشد.")]
            Status3 = 3,
            [Display(Name = "سطح تاييد پذيرنده پايين تر از سطح نقره اي است.")]
            Status4 = 4,
            [Display(Name = "درخواست مورد نظر يافت نشد.")]
            Status11 = 11,
            [Display(Name = "امكان ويرايش درخواست ميسر نمي باشد.")]
            Status12 = 12,
            [Display(Name = "هيچ نوع عمليات مالي براي اين تراكنش يافت نشد.")]
            Status21 = 21,
            [Display(Name = "تراكنش نا موفق ميباشد.")]
            Status22 = 22,
            [Display(Name = "رقم تراكنش با رقم پرداخت شده مطابقت ندارد.")]
            Status33 = 33,
            [Display(Name = "سقف تقسيم تراكنش از لحاظ تعداد يا رقم عبور نموده است")]
            Status34 = 34,
            [Display(Name = "اجازه دسترسي به متد مربوطه وجود ندارد.")]
            Status40 = 40,
            [Display(Name = "اطلاعات ارسال شدده مربوط به AdditionalData غیر معتبر می باشد.")]
            Status41 = 41,
            [Display(Name = "مدت زمان معتبر طول عمر شناسه پرداخت بايد بين 30 دقيه تا 45 روز مي باشد.")]
            Status42 = 42,
            [Display(Name = "درخواست مورد نظر آرشيو شده است.")]
            Status54 = 54,
            [Display(Name = "عمليات با موفقيت انجام گرديده است.")]
            Status100 = 100,
            [Display(Name = "عملیات پرداخت موفق بوده و قبلا PaymentVerification تراکنش انجام شده است.")]
            Status101 = 101,

        }

        public enum ProductSex : byte
        {
            [Description("زنانه")]
            [Display(Name = "زنانه")]
            Women = 1,
            [Description("مردانه")]
            [Display(Name = "مردانه")]
            Men = 2,
            [Description("بچگانه")]
            [Display(Name = "بچگانه")]
            Kids = 3
        }

        public enum WatchType : byte
        {
            [Description("آنالوگ")]
            [Display(Name = "آنالوگ")]
            Analogue = 1,
            [Description("دیجیتال")]
            [Display(Name = "دیجیتال")]
            Digital = 2


        }

        public enum Type : byte
        {
            [Description("ساعت مچی")]
            [Display(Name = "ساعت مچی")]
            Watch = 1,
            [Description("ساعت رومیزی")]
            [Display(Name = "ساعت رومیزی")]
            DesktopClock = 2,
            [Description("ساعت دیواری")]
            [Display(Name = "ساعت دیواری")]
            Clock = 3


        }

        public enum MasterCategory : byte
        {
            [Description("کلاسیک")]
            [Display(Name = "کلاسیک")]
            Classic = 1,
            [Description("اسپورت")]
            [Display(Name = "اسپورت")]
            Sport = 2
        }

        public enum Orginal : byte
        {
            [Description("یکبار مصرف")]
            [Display(Name = "یکبار مصرف")]
            Disposable = 1,
            [Description("کپی بالا")]
            [Display(Name = "کپی بالا")]
            HighCopy = 2,
            [Description("کپی")]
            [Display(Name = "کپی")]
            Copy = 3,
            [Description("اصل")]
            [Display(Name = "اصل")]
            Orginal = 3,

        }
        public enum Motor : byte
        {
            [Description("کوارتز")]
            [Display(Name = "کوارتز")]
            Quartz = 1,
            [Description("مکانیکال")]
            [Display(Name = "مکانیکال")]
            Mechanical = 2
        }





    }


}
