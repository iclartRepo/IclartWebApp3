using AutoMapper;
using IclartWebApp.Common.Entities;
using IclartWebApp.Common.Models;
using IclartWebApp.DAL;
using IclartWebApp.DAL.Interfaces;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.BLL
{
    public class CompetitorBLL : ICompetitorBLL
    {
        private readonly ITrackerContext _context;
        private readonly IGenericRepository<CompetitorEntity> _repository;

        public CompetitorBLL(IGenericRepository<CompetitorEntity> repository, ITrackerContext context)
        {
            this._repository = repository;
            _context = context;
        }
        public void AddCompetitor(CompetitorModel competitor)
        {
            if (ValidateCompetitorExists(competitor))
            {
                TinyMapper.Bind<CompetitorModel, CompetitorEntity>();
                var competitorEntity = TinyMapper.Map<CompetitorEntity>(competitor);
                _repository.Insert(competitorEntity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Competitor already exists!");
            }
        }
        public void UpdateCompetitor(CompetitorModel competitor)
        {
            if (ValidateCompetitorExists(competitor))
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CompetitorModel, CompetitorEntity>().ForMember(x => x.CreatedDate, opt => opt.Ignore()));
                var competitorEntity = _repository.Get(i => i.Id == competitor.Id).First();
                Mapper.Map(competitor, competitorEntity);
                competitorEntity.ModifiedDate = DateTime.Now;
                _repository.Update(competitorEntity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Competitor already exists!");
            }
        }
        public void DeleteCompetitor(int id)
        {
            var competitor = _repository.Get(i => i.Id == id).First();
            _repository.HardDelete(competitor);
            _context.SaveChanges();
        }
        public bool ValidateCompetitorExists(CompetitorModel competitor)
        {
            var competitors = _repository.Get(i => i.Name == competitor.Name).ToList();
            if (competitors.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
