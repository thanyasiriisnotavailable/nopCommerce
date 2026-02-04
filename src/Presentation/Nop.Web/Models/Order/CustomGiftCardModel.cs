using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Order;

public partial record CustomGiftCardModel : BaseNopModel
{
    public string RecipientName { get; set; }

    public string Message { get; set; }

    public string Style { get; set; }
}