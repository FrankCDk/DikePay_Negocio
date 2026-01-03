namespace DikePay.Modules.Catalog.Shared.Contracts.v1.DTOs
{
    public record ProductResponse(
        Guid Id,
        string Code,
        string Name,
        decimal Price,
        decimal Stock,
        string Unit
    );
}
