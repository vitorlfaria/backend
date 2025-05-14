using System;

namespace Ambev.DeveloperEvaluation.Common.Interfaces;

/// <summary>
/// Define the contract for representing a sale in the system.
/// </summary>
public interface ISale<T>
    where T : class
{
    /// <summary>
    /// Gets the unique identifier for the sale.
    /// </summary>
    /// <returns>The ID of the sale as a string.</returns>
    public string Id { get; }

    /// <summary>
    /// Gets the date and time when the sale was made.
    /// </summary>
    /// <returns>The date and time of the sale.</returns>
    public DateTime SaleDate { get; }

    /// <summary>
    /// Gets the total amount of the sale.
    /// </summary>
    /// <returns>The total amount of the sale.</returns>
    public decimal TotalAmount { get; }

    /// <summary>
    /// Gets the unique identifier of the customer associated with the sale.
    /// </summary>
    /// <returns>The ID of the customer associated with the sale.</returns>
    public Guid CustomerId { get; }

    /// <summary>
    /// Gets the unique identifier of the branch where the sale was made.
    /// </summary>
    /// <returns>The ID of the branch where the sale was made.</returns>
    public Guid BranchId { get; }

    /// <summary>
    /// Gets the list of product names included in the sale.
    /// </summary>
    /// <returns>The list of product names included in the sale.</returns>
    public List<T> Products { get; }

    /// <summary>
    /// Gets a value indicating whether the sale is canceled.
    /// </summary>
    /// <returns>True if the sale is canceled, false otherwise.</returns>
    public bool Canceled { get; }

    /// <summary>
    /// Gets the number of the sale.
    /// </summary>
    /// <returns>The number of the sale.</returns>
    public int Number { get; }
}
