namespace HelloDependencyInjection.Services.Shopping;

internal class StoreSettings
{
    public string Foo { get; }

    public StoreSettings()
    {
        Foo = $"Bar Hash: {GetHashCode()}";
    }
}