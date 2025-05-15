namespace Ambev.DeveloperEvaluation.Application.Products.GetPaginatedProducts;

/// <summary>
/// Represents a product in a paginated list.
/// </summary>
public class GetPaginatedProductsResult
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The price of the product.
    /// </summary>
    public decimal Price { get; set; }
}