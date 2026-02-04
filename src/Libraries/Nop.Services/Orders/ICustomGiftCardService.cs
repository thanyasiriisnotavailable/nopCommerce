using Nop.Core.Domain.Orders;

namespace Nop.Services.Orders;
public interface ICustomGiftCardService
{
    Task InsertAsync(CustomGiftCard giftCard);
    Task<CustomGiftCard?> GetByOrderIdAsync(int orderId);
}

