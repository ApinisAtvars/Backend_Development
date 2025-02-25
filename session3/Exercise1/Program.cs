var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get the connection string from the appsettings.Development.json file.
/*
In the Dependency Injection container, create a new instance of the ApplicationContext class and register it as a service.
*/
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(typeof(Program));

// Slower at startup, but with one line of code you have covered all the validators
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<ITravellerRepository, TravellerRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

var app = builder.Build();

// app.MapGet("/travellers", async (IApplicationService applicationService, IMapper mapper) =>
// {
//     var travellers = await applicationService.GetTravellers();    
//     var mappedTravellers = mapper.Map<List<TravellerDTO>>(travellers);
//     return Results.Ok(mappedTravellers);  // Return mapped DTOs instead of raw travellers
// });

// app.MapGet("/travellers/{id}", async (IApplicationService applicationService, IMapper mapper, int id) =>
// {
//     var traveller = await applicationService.GetTravellerByID(id);
//     if (traveller == null)
//     {
//         return Results.NotFound();
//     }
//     var mappedTraveller = mapper.Map<TravellerDTO>(traveller);
//     return Results.Ok(mappedTraveller);
// });

// app.MapGet("/destinations", async (IApplicationService applicationService, IMapper mapper) =>
// {
//     var destinations = await applicationService.GetDestinations();
//     var mappedDestinations = mapper.Map<List<DestinationDTO>>(destinations);
//     return Results.Ok(mappedDestinations);
// });

// app.MapGet("/guides", async (IApplicationService applicationService, IMapper mapper) =>
// {
//     var guides = await applicationService.GetGuides();
//     var mappedGuides = mapper.Map<List<GuideDTO>>(guides);
//     return Results.Ok(mappedGuides);
// });

// app.MapGet("/guides-with-tours", async (IApplicationService applicationService, IMapper mapper) =>
// {
//     var guidesWithTours = await applicationService.GetGuidesWithTours();
//     var mappedGuidesWithTours = mapper.Map<List<GuideTourDTO>>(guidesWithTours);
//     return Results.Ok(guidesWithTours);
// });

// app.MapGet("/guides/{id}", async (IApplicationService applicationService, IMapper mapper, int id, bool? includeTours) =>
// {
//     var guide = await applicationService.GetGuideByID(id, includeTours.Value);
//     if (guide == null)
//     {
//         return Results.NotFound();
//     }
//     if (includeTours == true)
//     {
//         var mappedGuide = mapper.Map<GuideTourDTO>(guide);
//         return Results.Ok(mappedGuide);
//     }
//     else
//     {
//         var mappedGuide = mapper.Map<GuideDTO>(guide);
//         return Results.Ok(mappedGuide);
//     }
    
// });

// app.MapGet("/guides/{id}", async (int id, IApplicationService applicationService, IMapper mapper, bool? includeTours = false) =>
// {
//     var guide = await applicationService.GetGuideByID(id, includeTours.Value);
    
//     if (guide == null)
//         return Results.NotFound();
    
//     if (includeTours.Value)
//     {
//         var detailedGuide = mapper.Map<GuideTourDTO>(guide);
//         return Results.Ok(detailedGuide);
//     }
//     else
//     {
//         var basicGuide = mapper.Map<GuideDTO>(guide);
//         return Results.Ok(basicGuide);
//     }
// });
// app.MapGet("/guides/{id}", async (int id, IApplicationService applicationService, IMapper mapper, bool? includeTours = false) =>
// {
//     var guide = await applicationService.GetGuideByID(id, includeTours ?? false);
    
//     if (guide == null)
//         return Results.NotFound();
    
//     if (includeTours ?? false)
//     {
//         var detailedGuide = mapper.Map<GuideTourDTO>(guide);
//         return Results.Ok(detailedGuide);
//     }
//     else
//     {
//         var basicGuide = mapper.Map<GuideDTO>(guide);
//         return Results.Ok(basicGuide);
//     }
// });

// app.MapPost("/traveller/{fullName}/{passportNumber}", async (IApplicationService applicationService, string fullName, string passportNumber) =>
// {
//     try
//     {
//         await applicationService.AddTraveller(fullName, passportNumber);
//         return Results.Created($"/travellers/{fullName}", fullName);
//     }
//     catch (Exception ex)
//     {
//         return Results.BadRequest(ex.Message);
//     }
// });

// app.MapPost("/destinations", async (IApplicationService applicationService, IMapper mapper, Destination destination) =>
// {
//     var newDestination = await applicationService.AddDestination(destination);
//     var mappedDestination = mapper.Map<DestinationDTO>(newDestination);
//     return Results.Created($"/destinations/{mappedDestination.DestinationId}", mappedDestination);
// });

// app.MapPost("/travellers/{travellerId}/destinations/{destinationId}", async (IApplicationService applicationService, int travellerId, int destinationId, IMapper mapper) =>
// {
//     var traveller = await applicationService.AddTravellerToDestination(travellerId, destinationId);
//     if (traveller == null)
//     {
//         return Results.NotFound();
//     }
//     var mappedTraveller = mapper.Map<TravellerDestinationDTO>(traveller);
//     return Results.Created($"/travellers/{travellerId}/destinations/{destinationId}", mappedTraveller);
// });

app.MapGroup("/travellers").GroupTravellers();

app.MapGroup("/guides").GroupGuides();

app.MapGroup("/destinations").GroupDestinations();

app.Run();
