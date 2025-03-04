namespace Exercise1.RouteGroups;

public static class DestinationsRouteGroup
{
    public static RouteGroupBuilder GroupDestinationsVersion1(this RouteGroupBuilder group)
    {
        group.MapGet("", async (IApplicationService applicationService, IMapper mapper) =>
        {
            var destinations = await applicationService.GetDestinations();
            var mappedDestinations = mapper.Map<List<DestinationDTO>>(destinations);
            return Results.Ok(mappedDestinations);
        });

        group.MapPost("", async (IApplicationService applicationService, IMapper mapper, Destination destination) =>
        {
            var newDestination = await applicationService.AddDestination(destination);
            var mappedDestination = mapper.Map<DestinationDTO>(newDestination);
            return Results.Created($"/destinations/{mappedDestination.DestinationId}", mappedDestination);
        });

        return group;
    }

    public static RouteGroupBuilder GroupDestinationsVersion2(this RouteGroupBuilder group)
    {
        group.MapGet("", async (IApplicationService applicationService, IMapper mapper) =>
        {
            var destinations = await applicationService.GetDestinations();
            var mappedDestinations = mapper.Map<List<DestinationDTOV2>>(destinations);

            return Results.Ok(mappedDestinations);
        });

        
        return group;
    }
}