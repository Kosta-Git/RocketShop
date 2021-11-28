namespace Models.DTO;

public class CoinDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Identifier { get; set; }

    public CoinDto( Guid id, string name, string identifier )
    {
        Id         = id;
        Name       = name;
        Identifier = identifier;
    }
}