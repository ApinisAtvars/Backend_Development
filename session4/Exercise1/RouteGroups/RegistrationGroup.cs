namespace Exercise1.RouteGroups;

public static class RegistrationRouteGroup
{

    public static RouteGroupBuilder GroupRegistrations(this RouteGroupBuilder group)
    {
        group.MapPost("", async (IMySqlService service, Registration registration) =>
        {
            await service.AddRegistration(registration);
            return Results.Ok();
        });

        group.MapPut("", async (IMySqlService service, Registration registration, IMapper mapper) =>
        {
            Registration editedRegistration = await service.EditRegistration(registration);
            var mappedRegistration = mapper.Map<RegistrationDTO>(editedRegistration);
            return Results.Ok(mappedRegistration);
        });

        group.MapGet("", async (IMySqlService service, IMapper mapper) =>
        {
            var registrations = await service.GetRegistrations();
            var mappedRegistrations = mapper.Map<List<RegistrationDTO>>(registrations);
            return Results.Ok(mappedRegistrations);
        });
        return group;
    }
}