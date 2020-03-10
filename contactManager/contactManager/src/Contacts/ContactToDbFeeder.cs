using contactManager.database.contacts;
using contactManager.src.Utils;
using System;
using System.Linq;

namespace contactManager.src.Contacts
{
    /// <summary>
    /// The class for inserting contact into databsae.
    /// </summary>
    public static class ContactToDbFeeder
    {
        /// <summary>
        /// Decide if contact will be added as new or update exisiting contact.
        /// </summary>
        /// <param name="newContact">Contact data tranfer object.</param>
        /// <returns>True if contact was added as new, else false.</returns>
        public static bool LineToDbFeed(ContactDTO newContact)
        {
            //check if birth number is not null and already exists in databse
            DbContactsEntities database = new DbContactsEntities();
            if (newContact.BirthNumber != null && database.Persons.Any(person => person.BirthNumber == newContact.BirthNumber))
            {
                //update contact
                DbUpdateContact(newContact, database);
                return false;
            }
            else
            {
                //add new contact
                DbAddContact(newContact, database);
                return true;
            }
        }

        /// <summary>
        /// Update existing contact in database.
        /// </summary>
        /// <param name="newContact">Contact data transfer object.</param>
        /// <param name="database">Database of contact entities.</param>
        private static void DbUpdateContact(ContactDTO newContact, DbContactsEntities database)
        {
            try
            {
                //find existing contact
                Person existingPerson = database.Persons.First(p => p.BirthNumber == newContact.BirthNumber);
                //update informations
                existingPerson.Name = newContact.Name;
                existingPerson.Surname = newContact.Surname;
                existingPerson.Address = newContact.Address;
                //update phone numbers
                for (int i = 0; i < newContact.PhoneNumbers.Length; i++)
                {
                    existingPerson.PhoneNumbers.ElementAt(i).PNumber = newContact.PhoneNumbers[i];
                }
                database.SaveChanges();
            } catch (Exception ex)
            {
                MessageSender.SendReport(ex.ToString());
            }
            
        }

        /// <summary>
        /// Add new contact to database.
        /// </summary>
        /// <param name="newContact">Contact data transfer object.</param>
        /// <param name="database">Database of contact entities.</param>
        private static void DbAddContact(ContactDTO newContact, DbContactsEntities database)
        {
            try
            {
                //create new contact
                Person newPerson = new Person
                {
                    Name = newContact.Name,
                    Surname = newContact.Surname,
                    BirthNumber = newContact.BirthNumber,
                    Address = newContact.Address
                };
                database.Persons.Add(newPerson);

                //create new phone numbers
                for (int i = 0; i < newContact.PhoneNumbers.Length; i++)
                {
                    PhoneNumber newNumber = new PhoneNumber
                    {
                        PNumber = newContact.PhoneNumbers[i]
                    };
                    newPerson.PhoneNumbers.Add(newNumber);
                }
                database.SaveChanges();
            } catch (Exception ex)
            {
                MessageSender.SendReport(ex.ToString());
            }
        }
    }
}
