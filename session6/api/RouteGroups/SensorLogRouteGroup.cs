namespace EnergySystem.Api.RouteGroups;

public static class SensorLogRouteGroup
{
    public static RouteGroupBuilder SensorLogGroup(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (IMongoService mongoService, SensorLogDTO sensorLog) =>
        {
            try
            {
                // TODO add FluentValidation for SensorLogDTO
                string sensorLogId = await mongoService.AddSensorLog(sensorLog);
                return Results.Created($"/sensorlogs/{sensorLogId}", sensorLog);
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

        group.MapGet("/", async (IMongoService mongoService) =>
        {
            try
            {
                var sensorLogs = await mongoService.GetSensorLogs();
                return Results.Ok(sensorLogs);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        group.MapGet("/stats/{buildingId}/{sensorId}", async (IMongoService mongoService, string buildingId, string sensorId) =>
        {
            try
            {
                var stats = await mongoService.GetStatsForSensorLog(buildingId, sensorId);
                var result = new
                {
                    Min = stats[0],
                    Max = stats[1],
                    Avg = stats[2]
                };
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        return group;
    }
}