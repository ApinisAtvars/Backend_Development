namespace Exercise1.RouteGroups;

public static class CarRouteGroup
{

    public static RouteGroupBuilder GroupCars(this RouteGroupBuilder group)
    {
        group.MapGet("", async (IMySqlService service, IMapper mapper) =>
        {
            var cars = await service.GetCars();
            var mappedCars = mapper.Map<List<CarDTO>>(cars);
            return Results.Ok(mappedCars);
        });

        group.MapPost("", async (IMySqlService service, Car car) =>
        {
            await service.AddCar(car);
            return Results.Ok();
        });

        return group;
    }
}