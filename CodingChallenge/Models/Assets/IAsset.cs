namespace CodingChallenge.Models.Assets
{
    public interface IAsset
    {
        string Key { get; }
        Currency Currency { get; }
        double GetValue();
        IAsset Consolidate(IAsset asset);
    }
}
