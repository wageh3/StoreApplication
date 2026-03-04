namespace Store.G02.Domain.Entities.Orders
{
    public enum OrderStatus
    {
        Pending = 0,
        PaymentSuccessful = 1,
        PaymentFailed = 2,
    }
}