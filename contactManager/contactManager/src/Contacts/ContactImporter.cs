using contactManager.src.Contacts;
using contactManager.src.Utils;
using System;
using System.IO;

namespace contactManager.src.Contacts
{
    /// <summary>
    /// The class for importing contacts from csv file.
    /// </summary>
    public static class ContactImporter
    {
        /// <summary>
        /// Read from csv file line by line.
        /// </summary>
        /// <param name="path">Path to csv file.</param>
        /// <returns>Number of failed lines.</returns>
        public static int ImportFile(string path)
        {
            MessageSender.SendReport(">> Importing file: " + path + Environment.NewLine);
            int countNewContacts = 0;
            int countUpdatedContacts = 0;
            int lineNumber = 0;
            //read from file
            using (StreamReader file = new StreamReader(path))
            {
                // skip header line
                string line = file.ReadLine();
                while ((line = file.ReadLine()) != null)
                {
                    lineNumber++;
                    string[] items = line.Split('\t');
                    //check if line contains right amount of items
                    if (items.Length < 5 || items.Length > 7)
                    {
                        MessageSender.SendReport("Line " + lineNumber + ": Invalid number of items." + Environment.NewLine);
                        continue;
                    }
                    //parse line to contact data transfer object
                    ContactDTO contact = ParseLineToContactDTO(items, lineNumber);
                    if (contact == null)
                    {
                        //parsing failed
                        continue;
                    }
                    if (ContactValidator.ValidateContact(contact, lineNumber))
                    {
                        if (ContactToDbFeeder.LineToDbFeed(contact) == true)
                        {
                            countNewContacts++;
                        }
                        else
                        {
                            countUpdatedContacts++;
                        }
                    }
                }
            }
            MessageSender.SendReport("Succesfully added " + countNewContacts + " contacts." + Environment.NewLine);
            MessageSender.SendReport("Succesfully updated " + countUpdatedContacts + " contacts." + Environment.NewLine);
            return lineNumber - (countNewContacts + countUpdatedContacts);
        }

        /// <summary>
        /// Parse items to contact data transfer object.
        /// </summary>
        /// <param name="items">Array of contact string informations.</param>
        /// <param name="lineNumber">Line number of contact in the cource csv.</param>
        /// <returns>New contact data tranfer object or null if failed.</returns>
        public static ContactDTO ParseLineToContactDTO(string[] items, int lineNumber)
        {
            ContactDTO contact = new ContactDTO
            {
                Name = items[0],
                Surname = items[1],
                Address = items[3]
            };
            //parse birth number
            if (!long.TryParse(items[2], out long birthNumber))
            {
                MessageSender.SendReport("Line " + lineNumber + ": Birth number is not valid number." + Environment.NewLine);
                return null;
            }
            contact.BirthNumber = birthNumber;
            //parse phone numbers
            for (int i = 4; i < items.Length; i++)
            {
                if (!int.TryParse(items[i], out int number))
                {
                    MessageSender.SendReport("Line " + lineNumber + ": Phone number is not valid number." + Environment.NewLine);
                    return null;
                }
                contact.PhoneNumbers.SetValue(number, i - 4);
            }
            return contact;
        }
    }
}
