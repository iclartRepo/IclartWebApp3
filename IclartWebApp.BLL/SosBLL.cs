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
    public class SosBLL : ISosBLL
    {
        private readonly IGenericRepository<SOSEntity> _repository;
        private readonly IGenericRepository<ProductEntity> _productRepository;
        private readonly IGenericRepository<ClientEntity> _clientRepository;
        private readonly IGenericRepository<SOSProductEntity> _orderRepository;
        private readonly IGenericRepository<SOSCustomEntity> _customOrderRepository;
        private ITrackerContext _context;

        public SosBLL(IGenericRepository<SOSEntity> repository,
            IGenericRepository<ProductEntity> productRepository,
            IGenericRepository<ClientEntity> clientRepository,
            IGenericRepository<SOSProductEntity> orderRepository,
            IGenericRepository<SOSCustomEntity> customOrderRepository,
            ITrackerContext context)
        {
            _context = context;
            _repository = repository;
            _productRepository = productRepository;
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
            _customOrderRepository = customOrderRepository;
        }

        public void AddSos(SOSFormModel model)
        {
            TinyMapper.Bind<SOSModel, SOSEntity>();
            var sosEntity = TinyMapper.Map<SOSEntity>(model.Sos);
            sosEntity.CreatedDate = DateTime.Now;
            sosEntity.Orders = new List<SOSProductEntity>();
            sosEntity.CustomOrders = new List<SOSCustomEntity>();

            double total_amount = 0;

            TinyMapper.Bind<SOSProductModel, SOSProductEntity>();
            if (model.StandardOrders != null)
            {
                for (int i = 0; i < model.StandardOrders.Count; i++)
                {
                    var order = model.StandardOrders[i];
                    var orderEntity = TinyMapper.Map<SOSProductEntity>(order);
                    order.QuantityDelivered = 0;
                    order.Discarded = false;

                    total_amount += (orderEntity.Price * orderEntity.Quantity);

                    var product = _productRepository.Get(y => y.Id == orderEntity.ProductId).FirstOrDefault();
                    if (product == null)
                    {
                        throw new Exception("Product not found.");
                    }
                    else
                    {
                        orderEntity.Product = product;
                        product.SOSOrders.Add(orderEntity);
                    }
                    orderEntity.SalesOrderSlip = sosEntity;
                    sosEntity.Orders.Add(orderEntity);
                    
                }
            }

            TinyMapper.Bind<SOSCustomModel, SOSCustomEntity>();
            if (model.CustomOrders != null)
            {
                for (int j = 0; j < model.CustomOrders.Count; j++)
                {
                    var order = model.CustomOrders[j];
                    var orderEntity = TinyMapper.Map<SOSCustomEntity>(order);
                    orderEntity.QuantityDelivered = 0;
                    orderEntity.Discarded = false;

                    total_amount += (orderEntity.Price * orderEntity.Quantity);

                    orderEntity.SalesOrderSlip = sosEntity;
                    sosEntity.CustomOrders.Add(orderEntity);
                }
            }
            sosEntity.TotalAmount = total_amount;
            sosEntity.Exported = false;

            var client = _clientRepository.Get(t => t.Id == sosEntity.ClientId).FirstOrDefault();
            if (client == null)
            {
                throw new Exception("Client not found.");
            }
            else
            {
                sosEntity.Status = false;
                sosEntity.ModifiedDate = DateTime.Now;
                sosEntity.ClientEntity = client;
                client.SalesOrderSlips.Add(sosEntity);
            }

            _repository.Insert(sosEntity);
            _context.SaveChanges();
        }
        public void DiscardOrders(int sosId, List<int> orderIds, List<int> customOrderIds)
        {
            if (orderIds != null)
            {
                for (int i=0; i<orderIds.Count; i++)
                {
                    var id = orderIds[i];
                    var order = _orderRepository.Get(x => x.Id == id).FirstOrDefault();

                    if (order != null)
                    {
                        order.Discarded = true;
                    }

                    _orderRepository.Update(order);
                }
            }
            if (customOrderIds != null)
            {
                for (int i = 0; i < customOrderIds.Count; i++)
                {
                    var id = orderIds[i];
                    var order = _customOrderRepository.Get(x => x.Id == id).FirstOrDefault();

                    if (order != null)
                    {
                        order.Discarded = true;
                    }

                    _customOrderRepository.Update(order);
                }
            }

            var checkSos = _repository.Get(i => i.Id == sosId).FirstOrDefault();

            if (checkSos != null)
            {
                var checkStandard = checkSos.Orders.Where(x => x.Discarded == false).ToList();
                var checkCustom = checkSos.CustomOrders.Where(x => x.Discarded == false).ToList();

                if (checkStandard.Count == 0 && checkCustom.Count == 0)
                {
                    checkSos.Status = true;
                    _repository.Update(checkSos);
                }
            }

            _context.SaveChanges();

        }
        public void UpdateSOS(SOSFormModel model)
        {
            var sosEntity = _repository.Get(i => i.Id == model.Sos.Id).FirstOrDefault();

            if (sosEntity == null)
            {
                throw new Exception("SOS not found.");
            }
            else
            {
                var standardOrders = sosEntity.Orders.ToList();
                var customOrders = sosEntity.CustomOrders.ToList();
                //Standard Orders
                for (int i = 0; i < standardOrders.Count; i++)
                {               
                    var order = standardOrders[i];
                    sosEntity.Orders.Remove(order);
                    _orderRepository.HardDelete(order);
                }

                //Custom Orders
                for (int j = 0; j < customOrders.Count; j++)
                {                   
                    var order = customOrders[j];                   
                    sosEntity.CustomOrders.Remove(order);
                    _customOrderRepository.HardDelete(order);
                }

                Mapper.Initialize(cfg => cfg.CreateMap<SOSModel, SOSEntity>().ForMember(x => x.CreatedDate, opt => opt.Ignore()).ForMember(x => x.Orders, opt => opt.Ignore()).ForMember(x => x.CustomOrders, opt => opt.Ignore()));
                Mapper.Map(model.Sos, sosEntity);
                sosEntity.ModifiedDate = DateTime.Now;

                double total_amount = 0;

                TinyMapper.Bind<SOSProductModel, SOSProductEntity>();
                if (model.StandardOrders != null)
                {
                    for (int i = 0; i < model.StandardOrders.Count; i++)
                    {
                        var order = model.StandardOrders[i];
                        var orderEntity = TinyMapper.Map<SOSProductEntity>(order);
                        order.QuantityDelivered = 0;
                        order.Discarded = false;

                        total_amount += (orderEntity.Price * orderEntity.Quantity);

                        var product = _productRepository.Get(y => y.Id == orderEntity.ProductId).FirstOrDefault();
                        if (product == null)
                        {
                            throw new Exception("Product not found.");
                        }
                        else
                        {
                            orderEntity.Product = product;
                        }
                        orderEntity.SalesOrderSlip = sosEntity;
                        sosEntity.Orders.Add(orderEntity);
                    }
                }

                TinyMapper.Bind<SOSCustomModel, SOSCustomEntity>();
                if (model.CustomOrders != null)
                {
                    for (int j = 0; j < model.CustomOrders.Count; j++)
                    {
                        var order = model.CustomOrders[j];
                        var orderEntity = TinyMapper.Map<SOSCustomEntity>(order);
                        orderEntity.QuantityDelivered = 0;
                        orderEntity.Discarded = false;

                        total_amount += (orderEntity.Price * orderEntity.Quantity);

                        orderEntity.SalesOrderSlip = sosEntity;
                        sosEntity.CustomOrders.Add(orderEntity);
                    }
                }
                sosEntity.TotalAmount = total_amount;

                _repository.Update(sosEntity);
                _context.SaveChanges();
            }
        }
    }
}
