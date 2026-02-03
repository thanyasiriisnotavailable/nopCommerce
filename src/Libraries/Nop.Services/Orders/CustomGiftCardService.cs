using Nop.Core.Domain.Orders;
using Nop.Data;

namespace Nop.Services.Orders;
public class CustomGiftCardService : ICustomGiftCardService
{
    private readonly IRepository<CustomGiftCard> _giftCardRepository;

    public CustomGiftCardService(IRepository<CustomGiftCard> giftCardRepository)
    {
        _giftCardRepository = giftCardRepository;
    }

    public async Task InsertAsync(CustomGiftCard giftCard)
    {
        await _giftCardRepository.InsertAsync(giftCard);
    }

    public async Task<IList<CustomGiftCard>> GetByOrderIdAsync(int orderId)
    {
        return await _giftCardRepository.Table
            .Where(x => x.OrderId == orderId)
            .ToListAsync();
    }
}
