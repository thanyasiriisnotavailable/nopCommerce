using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Common;
using Nop.Web.Models.Order;

namespace Nop.Web.Controllers;

public class CustomGiftCardController : BasePublicController
{
    private readonly IWorkContext _workContext;
    private readonly IGenericAttributeService _genericAttributeService;

    public CustomGiftCardController(
        IWorkContext workContext,
        IGenericAttributeService genericAttributeService)
    {
        _workContext = workContext;
        _genericAttributeService = genericAttributeService;
    }

    [HttpPost]
    public async Task<IActionResult> Save(CustomGiftCardModel model)
    {
        var customer = await _workContext.GetCurrentCustomerAsync();

        await _genericAttributeService.SaveAttributeAsync(
            customer, "CustomGiftCard.RecipientName", model.RecipientName);

        await _genericAttributeService.SaveAttributeAsync(
            customer, "CustomGiftCard.Message", model.Message);

        await _genericAttributeService.SaveAttributeAsync(
            customer, "CustomGiftCard.Style", model.Style);

        return Ok();
    }
}
