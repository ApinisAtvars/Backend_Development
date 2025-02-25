namespace Exercise1.RouteGroups;

public static class TravellersRouteGroup
{
    public static RouteGroupBuilder GroupTravellers(this RouteGroupBuilder group)
    {
        group.MapGet("", async (IApplicationService applicationService, IMapper mapper) =>
        {
            var travellers = await applicationService.GetTravellers();    
            var mappedTravellers = mapper.Map<List<TravellerDTO>>(travellers);
            return Results.Ok(mappedTravellers);  // Return mapped DTOs instead of raw travellers
        });
        
        group.MapGet("/{id}", async (IApplicationService applicationService, IMapper mapper, int id) =>
        {
            var traveller = await applicationService.GetTravellerByID(id);
            if (traveller == null)
            {
                return Results.NotFound();
            }
            var mappedTraveller = mapper.Map<TravellerDTO>(traveller);
            return Results.Ok(mappedTraveller);
        });

        group.MapPost("/{fullName}/{passportNumber}", async (IApplicationService applicationService, string fullName, string passportNumber) =>
        {
            try
            {
                await applicationService.AddTraveller(fullName, passportNumber);
                return Results.Created($"/travellers/{fullName}", fullName);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPost("/{travellerId}/destinations/{destinationId}", async (IApplicationService applicationService, int travellerId, int destinationId, IMapper mapper) =>
        {
            var traveller = await applicationService.AddTravellerToDestination(travellerId, destinationId);
            if (traveller == null)
            {
                return Results.NotFound();
            }
            var mappedTraveller = mapper.Map<TravellerDestinationDTO>(traveller);
            return Results.Created($"/travellers/{travellerId}/destinations/{destinationId}", mappedTraveller);
        });

// 
        return group;
    }
}