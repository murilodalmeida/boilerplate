using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Core.Abstractions.Common;
using FwksLabs.Libs.Core.Contracts.Common;

namespace FwksLabs.Boilerplate.Application.Features.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(
    IDatabaseContext dbContext,
    IValidator<CreateCustomerCommand> validator)
    : ICommandHandler<CreateCustomerCommand, CreateCustomerResult>
{
    public async Task<Result<CreateCustomerResult>> HandleAsync(CreateCustomerCommand command, CancellationToken cancellation)
    {
        var validationResult = await validator.ValidateAsync(command, cancellation);

        if (validationResult.IsValid is false)
            return new(validationResult);

        var customer = command.ToEntity();

        dbContext.Customers.Insert(customer);

        return new(new CreateCustomerResult(customer.Id));
    }
}