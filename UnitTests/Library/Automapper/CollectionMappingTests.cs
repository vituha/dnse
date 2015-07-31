using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;

namespace VS.Library.UT.Automapper
{
    [TestFixture]
    internal sealed class CollectionMappingTests
    {
        [Test]
        public void Automapper_ShouldNotMapCollectionPropertiesByDefault()
        {
            IConfiguration configuration = Mapper.Configuration;
            //MapperRegistry.Mappers.Add(new EntityCollectionMapper());

            configuration.CreateMap<OrderDto, Order>().ForMember(o => o.OrderDetails, opt => opt.UseDestinationValue());
            configuration.CreateMap<OrderDetailsDto, OrderDetail>();

            IMappingEngine mapper = Mapper.Engine;

            OrderDto dto = CreateTemplateOrder();

            Order entity = new Order
            {
                OrderId = 1,
                OrderDetails = new ObservableCollection<OrderDetail>()
                {
                    new OrderDetail
                    {
                        OrderId = -1,
                        DetailId = -11,
                        DetailName = "Very Old Detail Name"
                    },
                    new OrderDetail
                    {
                        OrderId = 1,
                        DetailId = 11,
                        DetailName = "Old First Detail Name"
                    }
                }
            };

            entity.OrderDetails.CollectionChanged += OrderDetails_CollectionChanged;

            mapper.Map(dto, entity);

            entity.OrderDetails[1].DetailId.Should().Be(12);
        }

        void OrderDetails_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine(e.Action);
        }

        private static OrderDto CreateTemplateOrder()
        {
            return new OrderDto
            {
                OrderId = 1,
                OrderName = "First Order",
                OrderDetails = new List<OrderDetailsDto>
                {
                    new OrderDetailsDto
                    {
                        DetailId = 11,
                        DetailName = "First Detail Name"
                    },
                    new OrderDetailsDto
                    {
                        DetailId = 12,
                        DetailName = "Second Detail Name"
                    }
                }
            };
        }
    }
}
