using IclartWebApp.Common.Models;

namespace IclartWebApp.BLL
{
    public interface IClientBLL
    {
        void AddClient(ClientFormModel client);
        void DeleteClient(int id);
        void UpdateClient(ClientFormModel client);
    }
}