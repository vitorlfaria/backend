using Ambev.DeveloperEvaluation.Common.Interfaces;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events.SaleEvents;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system.
/// </summary>
public class Sale : BaseEntity, ISale<SaleItem>
{
    /// <summary>
    /// Gets or sets the number of the sale.
    /// This is a unique identifier for the sale within the system.
    /// </summary>
    public int Number { get; set; } = 0;

    /// <summary>
    /// Gets or sets the date and time when the sale was made.
    /// This property is automatically set to the current UTC date and time when the sale is created.
    /// </summary>
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the unique identifier of the customer associated with the sale.
    /// This property is used to link the sale to a specific customer in the system.
    /// </summary>
    public Guid CustomerId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the denormination of the customer associated with the sale.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// This property represents the total value of all products included in the sale.
    /// </summary>
    public decimal TotalAmount { get; set; } = 0;

    /// <summary>
    /// Gets or sets the total number of items sold in the sale.
    /// This property represents the sum of the quantities of all products included in the sale.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the sale was made.
    /// This property is used to associate the sale with a specific branch location in the system.
    /// </summary>
    public Guid BranchId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the denormination of the branch where the sale was made.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of product identifiers included in the sale.
    /// This property is used to track the products that were sold in this transaction.
    /// </summary>
    public List<SaleItem> SaleItems { get; set; } = [];

    /// <summary>
    /// Gets or sets a flag indicating whether the sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }

    string ISale<SaleItem>.Id => Id.ToString();
    DateTime ISale<SaleItem>.SaleDate => SaleDate;
    decimal ISale<SaleItem>.TotalAmount => TotalAmount;
    Guid ISale<SaleItem>.CustomerId => CustomerId;
    Guid ISale<SaleItem>.BranchId => BranchId;
    List<SaleItem> ISale<SaleItem>.Products => SaleItems;
    bool ISale<SaleItem>.Canceled => Canceled;
    int ISale<SaleItem>.Number => Number;

    /// <summary>
    /// Calculates the total amount of the sale based on the sale items.
    /// </summary>
    /// <remarks>
    /// <listheader>The calculation includes:</listheader>
    /// <list type="bullet">Total amount of all sale items</list>
    /// <list type="bullet">Total number of items sold</list>
    /// </remarks>
    public void CalculateTotals()
    {
        TotalAmount = SaleItems.Sum(item => item.TotalPrice);
        TotalItems = SaleItems.Sum(item => item.Quantity);
    }

    public void Created()
    {
        AddDomainEvent(new SaleCreated(Id));
    }

    public void Modify()
    {
        AddDomainEvent(new SaleModified(Id));
    }

    public void Cancel()
    {
        Canceled = true;
        AddDomainEvent(new SaleCanceled(Id));
    }

    public void ItemCanceled(Guid productId)
    {
        var item = SaleItems.FirstOrDefault(i => i.ProductId == productId);
        SaleItems.Remove(item);
        AddDomainEvent(new ItemCancelled(Id, item.ProductId));
    }

    /// <summary>
    /// Perfoms validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Customer ID not null or empty</list>
    /// <list type="bullet">Branch ID not null or empty</list>
    /// <list type="bullet">Sale date not null and not in the future</list>
    /// <list type="bullet">Total amount greater than zero</list>
    /// <list type="bullet">Sale items not null or empty</list>
    /// <list type="bullet">All sale items have a quantity greater than zero</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}

