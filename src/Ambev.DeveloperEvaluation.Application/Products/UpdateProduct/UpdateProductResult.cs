namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the result of an update product operation.
/// </summary>
public class UpdateProductResult
{
    /// <summary>
    /// Gets or sets the ID of the updated product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the updated product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the updated product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the updated product.
    /// </summary>
    public decimal Price { get; set; }
}