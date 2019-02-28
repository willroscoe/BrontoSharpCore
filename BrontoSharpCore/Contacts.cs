﻿using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    public class Contacts : BrontoApiClass
    {

        #region ctor
        public Contacts(LoginSession session)
            : base(session)
        {
            this.Timeout = TimeSpan.FromMinutes(15);
        }

        #endregion

        #region CRUD operations


        /// <summary>
        /// Add a new contact to Bronto
        /// </summary>
        /// <param name="contact">The contact to add</param>
        /// <returns>The result of the add operation <seealso cref="BrontoResult"/></returns>
        public async Task<BrontoResult> AddAsync(contactObject contact)
        {
            return await AddAsync(new contactObject[] { contact });
        }

        /// <summary>
        /// Adds a list of new contacts to Bronto
        /// </summary>
        /// <param name="contacts">the list of contacts to add</param>
        /// <returns>The result of the add operation <seealso cref="BrontoResult"/></returns>
        public async Task<BrontoResult> AddAsync(IEnumerable<contactObject> contacts)
        {
            /*using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                addContactsResponse response = await client.addContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }*/

            try
            {
                BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout);

                addContactsResponse response = await client.addContactsAsync(session.SessionHeader, contacts.ToArray());
                client = null;

                return BrontoResult.Create(response.@return);
            }
            catch (Exception ex)
            {
                return new BrontoResult() { HasErrors = true, Items = new List<BrontoResultItem>() { new BrontoResultItem() { IsError = true, ErrorString = ex.Message } } };
            }
        }

        
        /// <summary>
        /// Add or updates a contact in Bronto
        /// </summary>
        /// <param name="contact">The contact to add or update</param>
        /// <returns>The result of the add or update operation <seealso cref="BrontoResult"/></returns>
        public async Task<BrontoResult> AddOrUpdateAsync(contactObject contact)
        {
            return await AddOrUpdateAsync(new contactObject[] { contact });
        }

        /// <summary>
        /// Add or updates a list of contact in Bronto
        /// </summary>
        /// <param name="contact">The contacts to add or update</param>
        /// <returns>The result of the add or update operation <seealso cref="BrontoResult"/></returns>
        public async Task<BrontoResult> AddOrUpdateAsync(IEnumerable<contactObject> contacts)
        {
            /*using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                addOrUpdateContactsResponse response = await client.addOrUpdateContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }*/
            try
            {
                BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout);
                addOrUpdateContactsResponse response = await client.addOrUpdateContactsAsync(session.SessionHeader, contacts.ToArray());
                client = null;
                return BrontoResult.Create(response.@return);
            }
            catch (Exception ex)
            {
                return new BrontoResult() { HasErrors = true, Items = new List<BrontoResultItem>() { new BrontoResultItem() { IsError = true, ErrorString = ex.Message } } };
            }
        }



        /// <summary>
        /// Reads all bronto contacts with the minimal number of fields returned
        /// </summary>
        /// <returns>the list of contacts</returns>
        public async Task<List<contactObject>> ReadAsync()
        {
            return await ReadAsync(new contactFilter());
        }

        /// <summary>
        /// Reads all contacts in bronto with the specified return fields
        /// </summary>
        /// <param name="options">The fields and information to return. Use the extension methods on the readContacts class to specify options</param>
        /// <returns>the list of contacts</returns>
        public async Task<List<contactObject>> ReadAsync(readContacts options)
        {
            return await ReadAsync(new contactFilter(),options);
        }


        /// <summary>
        /// Reads contacts from bronto using a filter and the specified return fields and information
        /// </summary>
        /// <param name="filter">The filter to use when reading</param>
        /// <param name="options">The fields and information to return. Use the extension methods on the readContacts class to specify options</param>
        /// <returns>the list of contacts</returns>
        public async Task<List<contactObject>> ReadAsync(contactFilter filter, readContacts options = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            /*using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readContacts c = options ?? new readContacts();
                c.filter = filter;
                c.pageNumber = 1;
                List<contactObject> list = new List<contactObject>();
                readContactsResponse response = await client.readContactsAsync(session.SessionHeader, c);
                contactObject[] result = response.@return;
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    response = await client.readContactsAsync(session.SessionHeader, c);
                    result = response.@return;
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                }
                return list;
            }*/
            try
            {
                BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout);

                readContacts c = options ?? new readContacts();
                c.filter = filter;
                c.pageNumber = 1;
                List<contactObject> list = new List<contactObject>();
                readContactsResponse response = await client.readContactsAsync(session.SessionHeader, c);
                contactObject[] result = response.@return;
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    response = await client.readContactsAsync(session.SessionHeader, c);
                    result = response.@return;
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                }
                client = null;

                return list;
            }
            catch (Exception ex)
            {
                return new List<contactObject>();
            }

        }

        

        /// <summary>
        /// Deletes a list of contacts in bronto
        /// </summary>
        /// <param name="contacts">the contacts to delete</param>
        /// <returns>The result of the Delete operation</returns>
        public async Task<BrontoResult> DeleteAsync(IEnumerable<contactObject> contacts)
        {
            /*using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                deleteContactsResponse response = await client.deleteContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }*/
            try
            { 
                BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout);
                deleteContactsResponse response = await client.deleteContactsAsync(session.SessionHeader, contacts.ToArray());
                client = null;
                return BrontoResult.Create(response.@return);
            }
            catch (Exception ex)
            {
                return new BrontoResult() { HasErrors = true, Items = new List<BrontoResultItem>() { new BrontoResultItem() { IsError = true, ErrorString = ex.Message } } };
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the list of custom contact fields
        /// </summary>
        public List<fieldObject> Fields
        {
            get
            {
                BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout);
                
                readFields c = new readFields();
                c.filter = new fieldsFilter();
                c.pageNumber = 1;
                List<fieldObject> list = new List<fieldObject>();
                fieldObject[] result = client.readFieldsAsync(session.SessionHeader, c).Result.@return;
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    result = client.readFieldsAsync(session.SessionHeader, c).Result.@return;
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                }
                client = null;
                return list;
                
            }
        }

        #endregion

        #region static properties and methods

        /// <summary>
        /// Returns a new readContacts object to be used in the Read method <see cref="Read(contactFilter, readContacts)"/>
        /// </summary>
        public static readContacts ReadOptions
        {
            get
            {
                return new readContacts();
            }
        }

        #endregion

    }
}
