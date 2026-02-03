using FluentAssertions;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Services.Orders;
using NUnit.Framework;

namespace Nop.Tests.Nop.Services.Tests.Orders;

[TestFixture]
public class CustomGiftCardServiceTests : ServiceTest
{
    private ICustomGiftCardService _customGiftCardService;
    private IRepository<Customer> _customerRepository;
    private IRepository<Order> _orderRepository;

    [OneTimeSetUp]
    public void SetUp()
    {
        _customGiftCardService = GetService<ICustomGiftCardService>();
        _customerRepository = GetService<IRepository<Customer>>();
        _orderRepository = GetService<IRepository<Order>>();
    }

    private async Task<(Customer customer, Order order)> CreateCustomerAndOrderAsync()
    {
        var customer = new Customer
        {
            CustomerGuid = Guid.NewGuid(),
            CreatedOnUtc = DateTime.UtcNow
        };
        await _customerRepository.InsertAsync(customer);

        var order = new Order
        {
            CustomerId = customer.Id,
            BillingAddressId = 1,
            OrderGuid = Guid.NewGuid(),
            CustomOrderNumber = string.Empty,
            CreatedOnUtc = DateTime.UtcNow
        };
        await _orderRepository.InsertAsync(order);

        return (customer, order);
    }

    [Test]
    public async Task ItShouldInsertCustomGiftCard()
    {
        var (customer, order) = await CreateCustomerAndOrderAsync();

        var giftCard = new CustomGiftCard
        {
            OrderId = order.Id,
            CustomerId = customer.Id,
            RecipientName = "John Doe",
            Message = "Happy Birthday",
            Style = "classic",
            CreatedOnUtc = DateTime.UtcNow
        };

        await _customGiftCardService.InsertAsync(giftCard);

        giftCard.Id.Should().BeGreaterThan(0);
        giftCard.CreatedOnUtc
            .Should()
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Test]
    public async Task ItShouldReturnEmptyListWhenOrderIdIsInvalid()
    {
        var result = await _customGiftCardService.GetByOrderIdAsync(0);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Test]
    public async Task ItShouldReturnEmptyListWhenNoGiftCardsForOrder()
    {
        var (customer, order) = await CreateCustomerAndOrderAsync();

        var result = await _customGiftCardService.GetByOrderIdAsync(order.Id);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Test]
    public async Task ItShouldReturnGiftCardsByOrderId()
    {
        var (customer, order) = await CreateCustomerAndOrderAsync();

        await _customGiftCardService.InsertAsync(new CustomGiftCard
        {
            OrderId = order.Id,
            CustomerId = customer.Id,
            RecipientName = "Alice",
            Message = "Congrats",
            Style = "modern"
        });

        await _customGiftCardService.InsertAsync(new CustomGiftCard
        {
            OrderId = order.Id,
            CustomerId = customer.Id,
            RecipientName = "Bob",
            Message = "Enjoy",
            Style = "classic"
        });

        var result = await _customGiftCardService.GetByOrderIdAsync(order.Id);

        result.Should().HaveCount(2);
        result.Select(x => x.RecipientName)
              .Should().BeEquivalentTo("Alice", "Bob");
    }
}
