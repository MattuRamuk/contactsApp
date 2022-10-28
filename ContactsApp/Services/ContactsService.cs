using System;
using ContactsApp.Models;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IMongoCollection<Contact> _contacts;

        public ContactsService(IContactsAppDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _contacts = database.GetCollection<Contact>(settings.ContactsCollectionName);
        }

        public async Task<Contact> Create(Contact contact)
        {
            await _contacts.InsertOneAsync(contact);
            return contact;
        }

        public async Task<List<Contact>> Get()
        {
            return await _contacts.Find(_ => true).ToListAsync();
        }
        
        public async Task<Contact?> Get(string id)
        {
            return await _contacts.Find(contact => contact.Id == id).FirstOrDefaultAsync();
        }

        public async Task Remove(string id)
        {
            await _contacts.DeleteOneAsync(contact => contact.Id == id);
        }

        public async Task Update(string id, Contact contact)
        {
            await _contacts.ReplaceOneAsync(contact => contact.Id == id, contact);
        }

        public async Task<List<Contact>> SearchInContacts(string? searchText)
        {
            return await _contacts.Find(contact => contact.Name.Contains(searchText)
            || contact.Nickname.Contains(searchText)
            || contact.Email.Contains(searchText)
            || contact.PhoneNumber.Contains(searchText)).ToListAsync();
        }
    }
}

