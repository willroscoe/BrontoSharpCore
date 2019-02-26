using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bronto.API
{
    public class Messages : BrontoApiClass
    {
        #region ctor
        public Messages(LoginSession session)
            : base(session)
        {
            this.Timeout = TimeSpan.FromMinutes(15);
        }

        #endregion

        /// <summary>
        /// Reads all bronto contacts with the minimal number of fields returned
        /// </summary>
        /// <returns>the list of contacts</returns>
        public List<messageObject> Read()
        {
            return Read(new messageFilter());
        }

        /// <summary>
        /// Reads all contacts in bronto with the specified return fields
        /// </summary>
        /// <param name="options">The fields and information to return. Use the extension methods on the readMessages class to specify options</param>
        /// <returns>the list of contacts</returns>
        public List<messageObject> Read(readMessages options)
        {
            return Read(new messageFilter(), options);
        }



        /// <summary>
        /// Reads contacts from bronto using a filter and the specified return fields and information
        /// </summary>
        /// <param name="filter">The filter to use when reading</param>
        /// <param name="options">The fields and information to return. Use the extension methods on the readMessages class to specify options</param>
        /// <returns>the list of contacts</returns>
        public List<messageObject> Read(messageFilter filter, readMessages options = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readMessages c = options ?? new readMessages();
                c.filter = filter;
                c.pageNumber = 1;
                List<messageObject> list = new List<messageObject>();
                messageObject[] result = client.readMessagesAsync(session.SessionHeader, c);
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    result = client.readMessagesAsync(session.SessionHeader, c);
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
        public async Task<List<messageObject>> ReadAsync()
        {
            return await ReadAsync(new messageFilter());
        }

        /// <summary>
        /// Reads all contacts in bronto with the specified return fields
        /// </summary>
        /// <param name="options">The fields and information to return. Use the extension methods on the readMessages class to specify options</param>
        /// <returns>the list of contacts</returns>
        public async Task<List<messageObject>> ReadAsync(readMessages options)
        {
            return await ReadAsync(new messageFilter(), options);
        }


        /// <summary>
        /// Reads contacts from bronto using a filter and the specified return fields and information
        /// </summary>
        /// <param name="filter">The filter to use when reading</param>
        /// <param name="options">The fields and information to return. Use the extension methods on the readMessages class to specify options</param>
        /// <returns>the list of contacts</returns>
        public async Task<List<messageObject>> ReadAsync(messageFilter filter, readMessages options = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readMessages c = options ?? new readMessages();
                c.filter = filter;
                c.pageNumber = 1;
                List<messageObject> list = new List<messageObject>();
                readMessagesResponse response = await client.readMessagesAsync(session.SessionHeader, c);
                messageObject[] result = response.@return;
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    response = await client.readMessagesAsync(session.SessionHeader, c);
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
        /// Returns a new readMessages object to be used in the Read method <see cref="Read(messageFilter, readMessages)"/>
        /// </summary>
        public static readMessages ReadOptions
        {
            get
            {
                return new readMessages();
            }
        }

        #endregion
    }
}
