namespace SqlFormatter.Core.Interfaces
{
    public interface ITokenImprover
    {
        void Improve<TInput>(TInput token);
    }
}