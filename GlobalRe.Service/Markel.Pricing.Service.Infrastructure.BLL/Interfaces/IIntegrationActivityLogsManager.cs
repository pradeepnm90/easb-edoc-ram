namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    public interface IIntegrationActivityLogsManager : IBaseManager
    {
        string Log<T>(string source, T? referenceKey, string messageType, object priorEntity, object request, string onBehalfOf = null) where T : struct;
        string Log(string source, string referenceKey, string messageType, object priorEntity, object request, string onBehalfOf = null);
        void Update(string integrationLogKey, string status, string message = null, object modifiedEntity = null);
    }
}
