using Bronto.API.BrontoService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API.Tests
{
    [TestClass]
    public class DeliveryTests : BrontoBaseTestWithLogin
    {
        
        /*[TestMethod()]
        public void ReadTest()
        {
            Deliveries deliveries = new Deliveries(Login);
            List<deliveryObject> lists = deliveries.Read();
        }

        [TestMethod()]
        public async Task ReadAsyncTest()
        {
            Deliveries deliveries = new Deliveries(Login);
            List<deliveryObject> lists = await deliveries.ReadAsync();
        }*/

        [TestMethod]
        public void ReadAllDeliveries()
        {
            Deliveries deliveries = new Deliveries(Login);
            StartTimer("Reading all Deliveries");
            deliveryFilter filter = GetDeliveryDateFilter();
            List<deliveryObject> list = ReadDeliveriesInternal(deliveries, filter, null);
            Console.WriteLine(EndTimer().ToString());
            Console.WriteLine("{0} deliveries read", list.Count);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    WriteDeliveryToConsole(item);
                }
            }
        }

        [TestMethod]
        public void ReadAllDeliveriesWithMessageDetail()
        { 
            Deliveries deliveries = new Deliveries(Login);
            StartTimer("Reading all Deliveries");
            deliveryFilter filter = GetDeliveryDateFilter();
            readDeliveries doptions = new readDeliveries() { includeContent = false };
            List<deliveryObject> list = ReadDeliveriesInternal(deliveries, filter, doptions);
            Console.WriteLine(EndTimer().ToString());
            Console.WriteLine("{0} deliveries read", list.Count);
            if (list.Count > 0)
            {
                Messages messages = new Messages(Login);

                foreach (var item in list) // get message details for each delivery
                {
                    messageFilter msgFilter = new messageFilter() { id = new string[] { item.messageId } };
                    readMessages moptions = new readMessages() { includeContent = true };
                    List<messageObject> message = messages.Read(msgFilter, moptions);
                    WriteDeliveryAndMessageToConsole(item, message.FirstOrDefault());
                }
            }
        }


        private void WriteDeliveryToConsole(deliveryObject delivery)
        {
            Console.WriteLine("Delivery returned: Date: {0} with ID {1} {2}, Status {2}, Sends {3}, Opens {4}, Clicks {5}, Bounces {6}", delivery.start, delivery.id, delivery.messageId, delivery.status, delivery.numSends, delivery.numOpens, delivery.numClicks, delivery.numBounces);
        }


        private void WriteDeliveryAndMessageToConsole(deliveryObject delivery, messageObject message)
        {
            Console.WriteLine("Delivery returned: Date: {0} with ID {1} {2}, Status {2}, Sends {3}, Opens {4}, Clicks {5}, Bounces {6}", delivery.start, delivery.id, delivery.messageId, delivery.status, delivery.numSends, delivery.numOpens, delivery.numClicks, delivery.numBounces);
        }


        private deliveryFilter GetDeliveryDateFilter()
        {
            deliveryFilter filter = new deliveryFilter();
            filter.start = new dateValue[]
            {
                new dateValue()
                {
                    @operator = filterOperator.SameDay,
                    operatorSpecified = true,
                    value = DateTime.Today,
                    valueSpecified = true
                }
            };
            return filter;
        }

        private List<deliveryObject> ReadDeliveriesInternal(Deliveries deliveries, deliveryFilter filter, readDeliveries options)
        {
            return deliveries.Read(filter, options);
        }


    }
}
