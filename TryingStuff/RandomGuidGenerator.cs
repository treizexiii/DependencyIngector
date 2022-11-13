namespace TryingStuff;

public class RandomGuidGenerator : IRandomGuidGenerator
{
    public Guid RandomGuid { get; set; } = Guid.NewGuid();
    public Guid EmptyGuid { get; set; } = Guid.Empty;

    public Guid TranslateGuid(string guid)
    {
        return Guid.TryParse(guid, out var result) ? result : Guid.Empty;
    }
}

public interface IRandomGuidGenerator
{
     Guid RandomGuid { get; set; }
     Guid EmptyGuid { get; set; }

     Guid TranslateGuid(string guid);
}