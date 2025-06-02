using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixCharge.API.Models.InputModels
{
    public class CreateClientInputModel
    {
        public CreateClientInputModel(string name, string email, string phone, string document)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }

        public Client ToEntity(Guid UserId)
        {
            return new Client(UserId, Name, Email, Phone, Document);
        }
    }
}