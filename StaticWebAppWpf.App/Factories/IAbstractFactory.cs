namespace StaticWebAppWpf.App.Factories
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}