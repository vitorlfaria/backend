using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Gets or sets the number of the sale.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the customer associated with the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the denormination of the customer associated with the sale.
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the denormination of the branch where the sale was made.
    /// </summary>
    public string BranchName { get; set; }

    /// <summary>
    /// Gets or sets the list of product identifiers included in the sale.
    /// </summary>
    public List<SaleItemDto> SaleItems { get; set; } = [];

    /// <summary>
    /// Gets or sets a flag indicating whether the sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }

    /// <summary>
    /// Validates the CreateSaleCommand using CreateSaleCommandValidator.
    /// </summary>
    /// <returns>A ValidationResultDetail containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

public class SaleItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int Discount { get; set; }
    public decimal TotalPrice { get; set; }
}