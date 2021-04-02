using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        // we use the attribute because in other way it would return 0/1/2 and we want to return text
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "PaymentRecieved")]
        PaymentRecieved,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed
    }
}