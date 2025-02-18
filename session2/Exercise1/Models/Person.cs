namespace Exercise1.Models;

public class Person
{
    [Key] // This makes the Entity Extractor think this is the ID column, but it would think so by default, cause it's called `Id`
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    // Adding a question mark after the property name makes it nullable
    public string Email { get; set; }
}