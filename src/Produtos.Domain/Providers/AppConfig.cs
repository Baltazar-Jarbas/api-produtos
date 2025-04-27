namespace Produtos.Domain.Providers
{
    public class AppConfig
    {
        public DbSettingsProvider DbContext { get; set; } = new DbSettingsProvider();
    }
}
