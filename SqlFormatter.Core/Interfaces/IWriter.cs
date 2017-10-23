namespace SqlFormatter.Core.Interfaces
{
    public interface IWriter
    {
        void Save(object value, object destination);
    }
}
