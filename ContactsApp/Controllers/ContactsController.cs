using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactsApp.Services;
using ContactsApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsApp.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;
        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }
        // GET: api/values
        [HttpGet]
        [Route("get-all-contacts")]
        public async Task<List<Contact>> Get()
        {
            return await _contactsService.Get();
        }

        // GET api/values/5
        [HttpGet("get-contact-details/{id}")]
        public async Task<ActionResult<Contact>> Get(string id)
        {
            var contact = await _contactsService.Get(id);
            if (contact == null)
            {
                return NotFound($"Contact with id = {id} not found");
            }

            return contact;
        }

        // POST api/values
        [HttpPost]
        [Route("add-new-contact")]
        public async Task<ActionResult<Contact>> Post([FromBody] Contact contact)
        {
            await _contactsService.Create(contact);

            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
        }


        // PUT api/values/5
        [HttpPut("update-contact-details/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Contact contact)
        {
            var existingContact = await _contactsService.Get(id);

            if (existingContact == null)
            {
                return NotFound($"Contact with id = {id} not found");
            }

            await _contactsService.Update(id, contact);

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("delete-contact/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingContact = await _contactsService.Get(id);

            if (existingContact == null)
            {
                return NotFound($"Contact with id = {id} not found");
            }

            await _contactsService.Remove(id);

            return Ok($"Contact with id = {id} is deleted");
        }


        // GET search-in-contacts?searchText=someSearchText
        [HttpGet("search-in-contacts/{searchText}")]
        public async Task<ActionResult<List<Contact>>> Search([FromRoute] string? searchText)
        {
            var contact = await _contactsService.SearchInContacts(searchText);
            if (contact == null)
            {
                return NotFound($"Contact with keyword = {searchText} not found");
            }

            return contact;
        }
    }
}

