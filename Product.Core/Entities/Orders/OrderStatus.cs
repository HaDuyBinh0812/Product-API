﻿using System.Runtime.Serialization;

namespace Product.Core.Entities.Orders
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived, 

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed      
    }
}