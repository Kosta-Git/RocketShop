namespace Models.DTO;

public class CoinCreateDto
{
    public CoinCreateDto( string name, string identifier )
    {
        Name       = name;
        Identifier = identifier;
    }

    public string Name { get; set; }
    public string Identifier { get; set; }
}