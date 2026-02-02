namespace Nop.Core.Domain.Orders;
public partial class CustomGiftCard : BaseEntity
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public string RecipientName { get; set; }

    public string Message { get; set; }

    public string Style { get; set; }

    public DateTime CreatedOnUtc { get; set; }
}
