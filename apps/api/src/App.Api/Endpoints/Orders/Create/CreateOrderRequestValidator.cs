using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Libs.Core.Encoders;
using MongoDB.Driver;
using LiteDbContext = FwksLabs.Boilerplate.Infra.LiteDb.Abstractions.IDatabaseContext;
using MongoDbContext = FwksLabs.Boilerplate.Infra.MongoDb.Abstractions.IDatabaseContext;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Orders.Create;

public sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator(
        MongoDbContext mongoDbContext,
        LiteDbContext liteDbContext)
    {
        RuleFor(x => x.CustomerId).Must(CustomerExists).WithMessage("A Customer with this Id doesn't exist.");

        RuleForEach(x => x.Products).MustAsync(ProductsExistAsync).WithMessage("A Product with this Id doesn't exist.");

        bool CustomerExists(string customerId) =>
            liteDbContext.Customers.Exists(x => x.Id == customerId.Decode());

        async Task<bool> ProductsExistAsync(OrderProductRequest orderProduct, CancellationToken cancellationToken) =>
            await mongoDbContext.Products.Find(x => x.Id == orderProduct.ProductId.Decode()).AnyAsync(cancellationToken);
    }
}