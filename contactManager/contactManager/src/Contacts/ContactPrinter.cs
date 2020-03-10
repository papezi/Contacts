using contactManager.database.contacts;
using contactManager.src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactManager.src.Contacts
{
    /// <summary>
    /// The class for printing database content.
    /// </summary>
    public static class ContactPrinter
    {
        /// <summary>
        /// Collect data from database to string.
        /// </summary>
        public static void PrintContactDb()
        {
            DbContactsEntities database = new DbContactsEntities();
            string report = ">> Printing contact database: " + Environment.NewLine;
            //collect data
            foreach (Person person in database.Persons)
            {
                report += person.PersonID + "> "
                    + person.Name + " "
                    + person.Surname + " "
                    + person.BirthNumber + " "
                    + person.Address + " ";
                foreach (PhoneNumber number in person.PhoneNumbers)
                {
                    report += number.PNumber + " ";
                }
                report += Environment.NewLine;
            }
            //send it to log
            MessageSender.SendReport(report);
        }
    }
}
