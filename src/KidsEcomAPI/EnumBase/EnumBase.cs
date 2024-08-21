using System.ComponentModel;

namespace KidsEcomAPI.EnumBase
{
    public enum Cart_Status
    {
        [Description("Chưa Thanh Toán")]
        paid = 1,
        [Description("Đã Thanh Toán")]
        NotYetPaid = 0,
        [Description("Chưa Đặt Hàng")]
        NoCarts = -1,
       
    }
}
