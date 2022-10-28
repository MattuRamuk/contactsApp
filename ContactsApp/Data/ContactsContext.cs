using System;
using Microsoft.EntityFrameworkCore;
using ContactsApp.Models;

namespace ContactsApp.Data
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
    }
}

