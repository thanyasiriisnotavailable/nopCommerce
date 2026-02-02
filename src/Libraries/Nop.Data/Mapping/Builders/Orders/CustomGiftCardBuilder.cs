using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Customers;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Orders;
public partial class CustomGiftCardBuilder : NopEntityBuilder<CustomGiftCard>
{
    #region Methods

    /// <summary>
    /// Apply entity configuration
    /// </summary>
    /// <param name="table">Create table expression builder</param>
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(CustomGiftCard.OrderId)).AsInt32().ForeignKey<Order>().NotNullable()
            .WithColumn(nameof(CustomGiftCard.CustomerId)).AsInt32().ForeignKey<Customer>().NotNullable()
            .WithColumn(nameof(CustomGiftCard.RecipientName)).AsString(200).Nullable()
            .WithColumn(nameof(CustomGiftCard.Message)).AsString(int.MaxValue).Nullable()
            .WithColumn(nameof(CustomGiftCard.Style)).AsString(100).NotNullable()
            .WithColumn(nameof(CustomGiftCard.CreatedOnUtc)).AsDateTime().NotNullable();
    }

    #endregion
}
