using ASPNETCoreMVCTraining.Interfaces;

namespace ASPNETCoreMVCTraining.Services;

public class OtherService : ITransientService, IScopedService, ISingletonService
{
    private Guid serviceId;
    public OtherService()
    {
        serviceId = Guid.NewGuid();
    }

    public Guid GetId()
    {
        return serviceId;
    }
}
