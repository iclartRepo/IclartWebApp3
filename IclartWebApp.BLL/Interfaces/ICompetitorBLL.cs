using IclartWebApp.Common.Models;

namespace IclartWebApp.BLL
{
    public interface ICompetitorBLL
    {
        void AddCompetitor(CompetitorModel competitor);
        void DeleteCompetitor(int id);
        void UpdateCompetitor(CompetitorModel competitor);
        bool ValidateCompetitorExists(CompetitorModel competitor);
    }
}