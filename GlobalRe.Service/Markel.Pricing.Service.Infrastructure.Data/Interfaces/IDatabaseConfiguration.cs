namespace Markel.Pricing.Service.Infrastructure.Data.Interfaces
{
    public interface IDatabaseConfiguration
    {
        //void Configure(string connectionString, bool autoDectChangeEnabled);
        void SetTimeout(int seconds);
    }
}
