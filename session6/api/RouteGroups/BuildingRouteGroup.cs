namespace EnergySystem.Api.RouteGroups;

public static class BuildingRouteGroup
{
    public static RouteGroupBuilder BuildingGroup(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMongoService mongoService) =>
        {
            try
            {
                // TODO add BuildingNotFoundException
                var buildings = await mongoService.GetBuildings();
                return Results.Ok(buildings);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        group.MapPost("/", async (IMongoService mongoService, BuildingDTO building) =>
        {
            try
            {
                // TODO add BuildingAlreadyExistsException
                // TODO add FluentValidation for BuildingDTO
                string buildingId = await mongoService.AddBuilding(building);
                return Results.Created($"/buildings/{buildingId}", building);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        group.MapGet("/{buildingId}", async (IMongoService mongoService, string buildingId) =>
        {
            try
            {
                // TODO add BuildingNotFoundException
                var building = await mongoService.GetBuilding(buildingId);
                return Results.Ok(building);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        group.MapPost("/{buildingId}/sensors", async (IMongoService mongoService, string buildingId, SensorDTO sensor) =>
        {
            try
            {
                // TODO add BuildingNotFoundException
                // TODO add SensorAlreadyExistsException
                // TODO add FluentValidation for SensorDTO
                await mongoService.AddSensorToBuilding(buildingId, sensor);
                return Results.Created();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
        
        return group;
    }
}