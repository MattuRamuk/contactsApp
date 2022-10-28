using System;
namespace ContactsApp.Models
{
    public class ContactsAppDatabaseSettings : IContactsAppDatabaseSettings
    {
        public string ContactsCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

