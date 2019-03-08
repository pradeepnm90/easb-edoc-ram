using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IGlobalReContextManager : IBaseManager
    {
        void Initialize(GlobalReContext globalReContext);

        GlobalReContext GlobalReContext { get; }

        void ResetContext();
    }
}
