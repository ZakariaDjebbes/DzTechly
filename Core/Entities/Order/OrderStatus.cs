using System.Runtime.Serialization;
namespace Core.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payement Received")]
        PayementReceived,
        [EnumMember(Value = "Payment Being Checked")]
        PaymentChecking,
        [EnumMember(Value = "Payement Failed")]
        PayementFailed
    }
}