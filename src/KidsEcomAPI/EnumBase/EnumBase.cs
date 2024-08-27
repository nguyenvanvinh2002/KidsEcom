using System.ComponentModel;

namespace KidsEcomAPI.EnumBase
{
    public enum Cart_Status
    {
        [Description("Chưa giao hàng")]
        paid = 1,
        [Description("Thành công")]
        NotYetPaid = 0,
        [Description("Lỗi")]
        Erorrpaid = -2,
        [Description("Chưa Đặt Hàng")]
        NoCarts = -1,
       
    }
}
