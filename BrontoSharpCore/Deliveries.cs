using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bronto.API
{
    public class Deliveries : BrontoApiClass
    {

        #region ctor
        public Deliveries(LoginSession session)
            : base(session)
        {
            this.Timeout = TimeSpan.FromMinutes(15);
        }

        #endregion

        /// <summary>
        /// Reads all bronto contacts with the minimal number of fields returned
        /// </summary>
        /// <returns>the list of contacts</returns>
        public List<deliveryObject> Read()
        {
            return Read(new deliveryFilter());
        }

        /// <summary>
        /// Reads all contacts in bronto with the specified return fields
        /// </summary>
        /// <param name="options">The fields and information to return. Use the extension methods on the readDeliveries class to specify options</param>
        /// <returns>the list of contacts</returns>
        public List<deliveryObject> Read(readDeliveries options)
        {
            return Read(new deliveryFilter(), options);
        }



        /// <summary>
        /// Reads contacts from bronto using a filter and the specified return fields and information
        /// </summary>
        /// <param name="filter">The filter to use when reading</param>
        /// <param name="options">The fields and information to return. Use the extension methods on the readDeliveries class to specify options</param>
        /// <returns>the list of contacts</returns>
        public List<deliveryObject> Read(deliveryFilter filter, readDeliveries options = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readDeliveries c = options ?? new readDeliveries();
                c.filter = filter;
                c.pageNumber = 1;
                List<deliveryObject> list = new List<deliveryObject>();
                deliveryObject[] result = client.readDeliveries(session.SessionHeader, c);
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    result = client.readDeliveries(session.SessionHeader, c);
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// Reads all bronto contacts with the minimal number of fields returned
        /// </summary>
        /// <returns>the list of contacts</returns>
        public async Task<List<deliveryObject>> ReadAsync()
        {
            return await ReadAsync(new deliveryFilter());
        }

        /// <summary>
        /// Reads all contacts in bronto with the specified return fields
        /// </summary>
        /// <param name="options">The fields and information to return. Use the extension methods on the readDeliveries class to specify options</param>
        /// <returns>the list of contacts</returns>
        public async Task<List<deliveryObject>> ReadAsync(readDeliveries options)
        {
            return await ReadAsync(new deliveryFilter(), options);
        }


        /// <summary>
        /// Reads contacts from bronto using a filter and the specified return fields and information
        /// </summary>
        /// <param name="filter">The filter to use when reading</param>
        /// <param name="options">The fields and information to return. Use the extension methods on the readDeliveries class to specify options</param>
        /// <returns>the list of contacts</returns>
        public async Task<List<deliveryObject>> ReadAsync(deliveryFilter filter, readDeliveries options = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readDeliveries c = options ?? new readDeliveries();
                c.filter = filter;
                c.pageNumber = 1;
                List<deliveryObject> list = new List<deliveryObject>();
                readDeliveriesResponse response = await client.readDeliveriesAsync(session.SessionHeader, c);
                deliveryObject[] result = response.@return;
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    response = await client.readDeliveriesAsync(session.SessionHeader, c);
                    result = response.@return;
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                }
                return list;
            }
        }

        #region static properties and methods

        /// <summary>
        /// Returns a new readDeliveries object to be used in the Read method <see cref="Read(deliveryFilter, readDeliveries)"/>
        /// </summary>
        public static readDeliveries ReadOptions
        {
            get
            {
                return new readDeliveries();
            }
        }

        #endregion

    }
}
