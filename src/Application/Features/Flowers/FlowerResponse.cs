namespace Application.Features.Flowers;

public record FlowerResponse(
    Guid Id,
    string Name,
    string Type,
    List<SightingResponse> Sightings);

public record SightingResponse(
    Guid Id,
    string Location,
    string Description);
