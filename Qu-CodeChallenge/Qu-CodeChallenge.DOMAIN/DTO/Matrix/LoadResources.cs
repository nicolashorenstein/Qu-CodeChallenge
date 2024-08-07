namespace Qu_CodeChallenge.DOMAIN.DTO.Matrix;

public class LoadResources
{
    public int XSize { get; set; } = 64;
    public int YSize { get; set; } = 64;
    public List<string> Words { get; set; }
}