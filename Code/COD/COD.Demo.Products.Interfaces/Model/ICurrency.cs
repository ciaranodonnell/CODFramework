namespace COD.Demo.Products.Interfaces.Model
{
    public interface ICurrency
    {
        string Name { get; }
        string Code { get; }
        decimal StandardRate { get; }
    }
}