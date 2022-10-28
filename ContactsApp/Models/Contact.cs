using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContactsApp.Models;

    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = String.Empty;

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("nickname")]
        public string? Nickname { get; set; }

        [BsonElement("phonenumber")]
        public string? PhoneNumber { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

    }


