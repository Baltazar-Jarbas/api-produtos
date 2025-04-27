namespace Produtos.Domain.Providers
{
    public class DbSettingsProvider
    {
        public string AppConnectionString { get; set; }
    }

    public class DefaultValuesProvider
    {
        public const int VarcharLenght = 2000;
    }
}
