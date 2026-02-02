namespace DikePay.Modules.Catalog.Shared.Contracts.v1.DTOs
{
    public record ProductResponse(
        Guid Id,
        string Version,
        string Code,
        string Sku,
        string Name,
        decimal Price,
        decimal Stock,
        decimal StockMin,
        string TaxProductCode,
        string Unit,
        string ProductType,
        string TaxInventoryType,
        bool AllowsDecimals,
        bool HasSerialNumber,
        bool HasBatchNumber,
        bool TrackStock,
        bool IsOpenPrice,
        string Currency,
        decimal DiscountPercentage,
        string TaxAffectationType,
        string Status,
        DateTime CreatedAt,
        string UserCreatedAt,
        DateTime UpdatedAt,
        string UserUpdateddAt
    );
}