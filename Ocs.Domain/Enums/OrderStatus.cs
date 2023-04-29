namespace Ocs.Domain.Enums;

public enum OrderStatus
{
    New,

    AwaitPaid,

    Paid,

    SentForDelivery,

    Delivered,

    Completed
}