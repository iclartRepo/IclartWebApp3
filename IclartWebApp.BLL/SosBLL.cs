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
    public class SosBLL
    {
        private GenericRepository<SOSEntity> _repository;
        private GenericRepository<ProductEntity> _productRepository;
        private GenericRepository<ClientEntity> _clientRepository;
        private GenericRepository<SOSProductEntity> _orderRepository;
        private GenericRepository<SOSCustomEntity> _customOrderRepository;
        private DBContext context;

        public SosBLL()
        {
            context = new DBContext();
            _repository = new GenericRepository<SOSEntity>(context);
            _productRepository = new GenericRepository<ProductEntity>(context);
            _clientRepository = new GenericRepository<ClientEntity>(context);
            _orderRepository = new GenericRepository<SOSProductEntity>(context);
            _customOrderRepository = new GenericRepository<SOSCustomEntity>(context);
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
                //Standard Orders
                for (int i = 0; i < sosEntity.Orders.Count; i++)
                {
                    var order = sosEntity.Orders.ToList()[i];
                    sosEntity.Orders.Remove(order);
                    _orderRepository.HardDelete(order);
                }

                //Custom Orders
                for (int j = 0; j < sosEntity.CustomOrders.Count; j++)
                {
                    var order = sosEntity.CustomOrders.ToList()[j];
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
            }
        }
    }
}
