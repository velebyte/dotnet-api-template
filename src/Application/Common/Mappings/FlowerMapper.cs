using Application.Features.Flowers;
using Domain.Entities;

namespace Application.Common.Mappings;

internal static class FlowerMapper
{
    internal static FlowerResponse Map(this Flower flower)
    {
        return new FlowerResponse(
            flower.Id,
            flower.Name,
            flower.Type,
            flower.Sightings.Select(s => s.Map()).ToList());
    }

    internal static SightingResponse Map(this Sighting sighting)
    {
        return new SightingResponse(
            sighting.Id,
            sighting.Location,
            sighting.Description);
    }
}
