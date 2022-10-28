using System;
using ContactsApp.Models;

namespace ContactsApp.Services
{
    public interface IContactsService
    {
        Task<List<Contact>> Get();
        Task<Contact?> Get(string id);
        Task<Contact> Create(Contact contact);
        Task Update(string id, Contact contact);
        Task Remove(string id);
        Task<List<Contact>> SearchInContacts(string? searchText);

    }
}

