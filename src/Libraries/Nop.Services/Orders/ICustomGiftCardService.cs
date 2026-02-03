using Nop.Core.Domain.Orders;

namespace Nop.Services.Orders;
public interface ICustomGiftCardService
{
    Task InsertAsync(CustomGiftCard giftCard);
    Task<IList<CustomGiftCard>> GetByOrderIdAsync(int orderId);
}

