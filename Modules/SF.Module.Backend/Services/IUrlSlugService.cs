using SF.Core.Abstraction.Service;
using SF.Entitys;

namespace SF.Module.Backend.Services
{
    public interface IUrlSlugService : IBaseService
    {
        UrlSlugEntity Get(long entityId, long entityTypeId);

        void Add(string name, long entityId, long entityTypeId);

        void Update(string newName, long entityId, long entityTypeId);

        void Remove(long entityId, long entityTypeId);
    }
}
