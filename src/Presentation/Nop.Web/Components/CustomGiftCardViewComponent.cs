using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Common;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Order;

namespace Nop.Web.Components;

public class CustomGiftCardViewComponent : NopViewComponent
{
    private readonly IWorkContext _workContext;
    private readonly IGenericAttributeService _genericAttributeService;

    public CustomGiftCardViewComponent(
        IWorkContext workContext,
        IGenericAttributeService genericAttributeService)
    {
        _workContext = workContext;
        _genericAttributeService = genericAttributeService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var customer = await _workContext.GetCurrentCustomerAsync();

        var model = new CustomGiftCardModel
        {
            RecipientName = await _genericAttributeService
                .GetAttributeAsync<string>(customer, "CustomGiftCard.RecipientName"),
            Message = await _genericAttributeService
                .GetAttributeAsync<string>(customer, "CustomGiftCard.Message"),
            Style = await _genericAttributeService
                .GetAttributeAsync<string>(customer, "CustomGiftCard.Style")
        };

        return View(model);
    }
}
