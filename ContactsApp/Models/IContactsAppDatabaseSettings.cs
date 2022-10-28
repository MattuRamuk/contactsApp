using System;
namespace ContactsApp.Models
{

    public interface IContactsAppDatabaseSettings
    {
        public string ContactsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        

    }
}
