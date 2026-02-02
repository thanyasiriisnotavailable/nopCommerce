using FluentMigrator;
using Nop.Core.Domain.Orders;
using Nop.Data.Extensions;

namespace Nop.Data.Migrations.UpgradeTo500;
[NopSchemaMigration("2026-02-02 11:00:00", "Create CustomGiftCard table")]
public class CreateCustomGiftCardTable : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.TableFor<CustomGiftCard>();
    }
}