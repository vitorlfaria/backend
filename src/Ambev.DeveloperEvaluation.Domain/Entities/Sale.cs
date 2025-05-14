using System;
using Ambev.DeveloperEvaluation.Common.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Common;

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
    /// Gets or sets the total amount of the sale.
    /// This property represents the total value of all products included in the sale.
    /// </summary>
    public decimal TotalAmount { get; set; } = 0;

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the sale was made.
    /// This property is used to associate the sale with a specific branch location in the system.
    /// </summary>
    public Guid BranchId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the list of product identifiers included in the sale.
    /// This property is used to track the products that were sold in this transaction.
    /// </summary>
    public List<SaleItem> SaleItems { get; set; } = [];

    /// <summary>
    /// Gets or sets a flag indicating whether the sale is canceled.
    /// </summary>
    public bool Canceled { get; set; }

    /// <summary>
    /// Gets or sets the customer associated with the sale.
    /// This property is used to link the sale to a specific customer in the system.
    /// </summary>
    public virtual User Customer { get; set; } = new();

    /// <summary>
    /// Gets or sets the branch where the sale was made.
    /// This property is used to associate the sale with a specific branch location in the system.
    /// </summary>
    public virtual Branch Branch { get; set; } = new();

    string ISale<SaleItem>.Id => Id.ToString();
    DateTime ISale<SaleItem>.SaleDate => SaleDate;
    decimal ISale<SaleItem>.TotalAmount => TotalAmount;
    Guid ISale<SaleItem>.CustomerId => CustomerId;
    Guid ISale<SaleItem>.BranchId => BranchId;
    List<SaleItem> ISale<SaleItem>.Products => SaleItems;
    bool ISale<SaleItem>.Canceled => Canceled;
    int ISale<SaleItem>.Number => Number;
}

