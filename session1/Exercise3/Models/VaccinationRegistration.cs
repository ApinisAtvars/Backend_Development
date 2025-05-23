namespace Exercise3.Models;

public class VaccinationRegistration
{
    public Guid VaccinatinRegistrationId { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? EMail { get; set; }
    public int YearOfBirth { get; set; }
    public string? VaccinationDate { get; set; }
    public Guid VaccinTypeId { get; set; }
    public Guid VaccinationLocationId { get; set; }
}
