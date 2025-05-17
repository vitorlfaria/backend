namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Response model for DeleteSale operation.
/// </summary>
public class DeleteSaleResponse
{
    /// <summary>
    /// Indicates whether the deletion was successful.
    /// </summary>
    public bool Success { get; set; }

    public DeleteSaleResponse(bool success) => Success = success;
}