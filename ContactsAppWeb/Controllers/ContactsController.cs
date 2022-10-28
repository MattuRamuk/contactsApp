using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactsAppWeb.Models;
using ContactsAppWeb.HTTPClientHelper;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsAppWeb.Controllers
{
    public class ContactsController : Controller
    {
        ContactsAPI _api = new ContactsAPI();

        // GET: /<controller>/
        /*Gets all the contacts on the landing page*/
        public async Task<IActionResult> Index()
        {
            List<Contact>? contacts = new List<Contact>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("get-all-contacts");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    contacts = JsonConvert.DeserializeObject<List<Contact>>(result);
                }   
            }

            return View(contacts);
        }

        //navigate to add new contact page
        public ActionResult AddNewContact()
        {
            return View();
        }

        /*Adds a new contact*/
        public async Task<IActionResult> AddNew(Contact newContact)
        {
            HttpClient httpClient = _api.Initial();

            var newItem = new Contact
            {
                Name = newContact.Name,
                Nickname = newContact.Nickname,
                PhoneNumber = newContact.PhoneNumber,
                Email = newContact.Email
            };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("add-new-contact", newItem);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
           
            return View(newContact);
        }

        //navigate to edit contact details page
        public async Task<ActionResult> EditContact(string? id)
        {
            var contact = new Contact();
            HttpClient httpClient = _api.Initial();

            HttpResponseMessage response = await httpClient.GetAsync("get-contact-details/" + id);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    contact = JsonConvert.DeserializeObject<Contact>(result);
                }
            }
            return View(contact);
        }

        /*Edit an existing contact*/
        public async Task<IActionResult> Edit(Contact contact, string? id)
        {
            HttpClient httpClient = _api.Initial();

            var updatedContact = new Contact
            {
                Id = contact.Id,
                Name = contact.Name,
                Nickname = contact.Nickname,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email
            };

            HttpResponseMessage response = await httpClient.PutAsJsonAsync("update-contact-details/" + id, updatedContact);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updatedContact);
        }

        /*deletes an existing contact*/
        public async Task<IActionResult> DeleteContact(string? id)
        {
            HttpClient httpClient = _api.Initial();

            HttpResponseMessage response = await httpClient.DeleteAsync("delete-contact/" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        /*searches throughout the contact list*/
        public async Task<IActionResult> SearchInContactList(string? searchText)
        {
            HttpClient httpClient = _api.Initial();
            List<Contact>? contact = null;
            
            HttpResponseMessage response = await httpClient.GetAsync("search-in-contacts/" + searchText);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    contact = JsonConvert.DeserializeObject<List<Contact>>(result);
                }
            }
            return View("Index", contact);
        }

    }
}

