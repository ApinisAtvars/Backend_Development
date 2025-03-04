namespace Exercise1.RouteGroups;


public static class GuidesRouteGroup
{
    public static RouteGroupBuilder GroupGuides(this RouteGroupBuilder group)
    {
        group.MapGet("", async (IApplicationService applicationService, IMapper mapper) =>
        {
            var guides = await applicationService.GetGuides();
            var mappedGuides = mapper.Map<List<GuideDTO>>(guides);
            return Results.Ok(mappedGuides);
        });

        group.MapGet("-with-tours", async (IApplicationService applicationService, IMapper mapper) =>
        {
            var guidesWithTours = await applicationService.GetGuidesWithTours();
            var mappedGuidesWithTours = mapper.Map<List<GuideTourDTO>>(guidesWithTours);
            return Results.Ok(guidesWithTours);
        });

        group.MapGet("/{id}", async (int id, IApplicationService applicationService, IMapper mapper, bool? includeTours = false) =>
        {
            var guide = await applicationService.GetGuideByID(id, includeTours ?? false);
            
            if (guide == null)
                return Results.NotFound();
            
            if (includeTours ?? false)
            {
                var detailedGuide = mapper.Map<GuideTourDTO>(guide);
                return Results.Ok(detailedGuide);
            }
            else
            {
                var basicGuide = mapper.Map<GuideDTO>(guide);
                return Results.Ok(basicGuide);
            }
        });

        group.MapGet("/download", async (IApplicationService applicationService, IMapper mapper) =>
            {
                var guides = await applicationService.GetGuides();
                var mappedGuides = mapper.Map<List<GuideDTO>>(guides);

                // Convert to CSV
                using var memoryStream = new MemoryStream();
                using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                csv.WriteRecords(mappedGuides);
                writer.Flush();

                return Results.File(memoryStream.ToArray(), "text/csv", "guides.csv");
            });

        return group;
        }
}