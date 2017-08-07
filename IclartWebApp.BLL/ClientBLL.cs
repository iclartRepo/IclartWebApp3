using AutoMapper;
using IclartWebApp.Common.Entities;
using IclartWebApp.Common.Models;
using IclartWebApp.DAL;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.BLL
{
    public class ClientBLL
    {
        private GenericRepository<ClientEntity> _repository;
        private GenericRepository<CompetitorEntity> _competitorRepository;
        private GenericRepository<CompetitorDiscountSchemesEntity> _dsSchemesRepository;
        private DBContext context;

        public ClientBLL()
        {
            context = new DBContext();
            _repository = new GenericRepository<ClientEntity>(context);
            _competitorRepository = new GenericRepository<CompetitorEntity>(context);
            _dsSchemesRepository = new GenericRepository<CompetitorDiscountSchemesEntity>(context);
        }
        /// <summary>
        /// Adding new Client into Database
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(ClientFormModel client)
        {
            var exists = ValidateIfExists(client.Client);
            var complete = ValidateCompleteFields(client.Client);
            if (exists && complete)
            {
                TinyMapper.Bind<ClientModel, ClientEntity>();
                var clientEntity = TinyMapper.Map<ClientEntity>(client.Client);
                clientEntity.Created_Date = DateTime.Now;
                clientEntity.CompetitorDiscountSchemes = new List<CompetitorDiscountSchemesEntity>();

                TinyMapper.Bind<CompetitorDiscountSchemesModel, CompetitorDiscountSchemesEntity>();
                if (client.CompetitorDiscountSchemes != null)
                {
                    for (int i = 0; i < client.CompetitorDiscountSchemes.Count; i++)
                    {
                        var competitorDS = client.CompetitorDiscountSchemes[i];
                        var competitorDSEntity = TinyMapper.Map<CompetitorDiscountSchemesEntity>(competitorDS);
                        var competitor = _competitorRepository.Get(y => y.Id == competitorDS.CompetitorId).First();
                        competitorDSEntity.CompetitorEntity = competitor;
                        competitor.CompetitorDiscountSchemes.Add(competitorDSEntity);
                        clientEntity.CompetitorDiscountSchemes.Add(competitorDSEntity);
                    }
                }

                _repository.Insert(clientEntity);
            }
            else if (complete == false)
            {
                throw new Exception("Please fill out required fields.");
            }
            else
            {
                throw new Exception("Client already exists.");
            }
        }
        /// <summary>
        /// Updating Client Info
        /// </summary>
        /// <param name="client"></param>
        public void UpdateClient(ClientFormModel client)
        {
            var exists = ValidateIfExists(client.Client);
            var complete = ValidateCompleteFields(client.Client);
            if (exists && complete)
            {

                var clientEntity = _repository.Get(i => i.Id == client.Client.Id).First();
                var discountSchemes = clientEntity.CompetitorDiscountSchemes.ToList();
                for (int i = 0; i < discountSchemes.Count; i++)
                {
                    var competitorDS = discountSchemes[i];
                    clientEntity.CompetitorDiscountSchemes.Remove(competitorDS);
                    _dsSchemesRepository.HardDelete(competitorDS);
                }
                Mapper.Initialize(cfg => cfg.CreateMap<ClientModel, ClientEntity>().ForMember(x => x.Created_Date, opt => opt.Ignore()).ForMember(x => x.CompetitorDiscountSchemes, opt => opt.Ignore()));
                Mapper.Map(client.Client, clientEntity);
                clientEntity.Modified_Date = DateTime.Now;


                TinyMapper.Bind<CompetitorDiscountSchemesModel, CompetitorDiscountSchemesEntity>();
                if (client.CompetitorDiscountSchemes != null)
                {
                    for (int i = 0; i < client.CompetitorDiscountSchemes.Count; i++)
                    {
                        var competitorDS = client.CompetitorDiscountSchemes[i];
                        var competitorDSEntity = TinyMapper.Map<CompetitorDiscountSchemesEntity>(competitorDS);
                        var competitor = _competitorRepository.Get(y => y.Id == competitorDS.CompetitorId).First();
                        competitorDSEntity.CompetitorEntity = competitor;
                        clientEntity.CompetitorDiscountSchemes.Add(competitorDSEntity);
                    }
                }

                _repository.Update(clientEntity);
            }
            else if (complete == false)
            {
                throw new Exception("Please fill out required fields.");
            }
            else
            {
                throw new Exception("Client already exists.");
            }
        }
        /// <summary>
        /// Soft Delete a Client
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClient(int id)
        {
            var clientEntity = _repository.Get(i => i.Id == id).First();
            clientEntity.IsDeleted = !clientEntity.IsDeleted;
            clientEntity.Modified_Date = DateTime.Now;
            _repository.SoftDelete(clientEntity);
        }
        /// <summary>
        /// Add Validation Function for incomplete fields
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool ValidateCompleteFields(ClientModel client)
        {
            var complete = true;
            if (client.Name == "" || client.Name == null)
            {
                complete = false;
            }
            if (client.Telephone_Number == "" || client.Telephone_Number == null)
            {
                complete = false;
            }
            if (client.Contacts_Order == "" || client.Contacts_Order == null)
            {
                complete = false;
            }
            return complete;
        }
        /// <summary>
        /// Add Validation function for checking if client exists already
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool ValidateIfExists(ClientModel client)
        {
            var clients = _repository.Get();
            var clientCheck = (from c in clients
                               where c.Name.Trim(' ').ToLower() == client.Name.Trim(' ').ToLower() && c.IsDeleted == false
                               select c).ToList();
            if (clientCheck.Count == 0 || client.Id == clientCheck[0].Id)
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
