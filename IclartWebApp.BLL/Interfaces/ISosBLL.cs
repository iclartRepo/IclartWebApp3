using System.Collections.Generic;
using IclartWebApp.Common.Models;

namespace IclartWebApp.BLL
{
    public interface ISosBLL
    {
        void AddSos(SOSFormModel model);
        void DiscardOrders(int sosId, List<int> orderIds, List<int> customOrderIds);
        void UpdateSOS(SOSFormModel model);
    }
}