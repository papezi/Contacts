using contactManager.src.Utils;
using System;

namespace contactManager.src.Contacts
{
    /// <summary>
    /// The class for validating contact.
    /// </summary>
    public static class ContactValidator
    {
        /// <summary>
        /// Property indicating if contact is valid.
        /// </summary>
        private static bool ContactValidity { get; set; }

        /// <summary>
        /// Property inticating line number of actual contact in the cource csv.
        /// </summary>
        private static int LineNumber { get; set; }

        /// <summary>
        /// Base method for validating each property of contact;
        /// </summary>
        /// <param name="contact">Contact data tranfer object.</param>
        /// <param name="lineNumber">Line number in the source csv.</param>
        /// <returns>True if contact is valid, else false.</returns>
        public static bool ValidateContact(ContactDTO contact, int lineNumber)
        {
            ContactValidity = true;
            LineNumber = lineNumber;
            //check if required Surname is present
            if (contact.Surname == "") 
            {
                MessageSender.SendReport("Line " + lineNumber + ": Surname is missing. Line failed to import." + Environment.NewLine);
                return false;
            }

            ValidateTextItem(contact.Name, 100, "Name");
            ValidateTextItem(contact.Surname, 100, "Surname");
            ValidateBirthNumber(contact.BirthNumber);
            ValidateTextItem(contact.Address, 255, "Address");

            //validate all phone numbers
            foreach(int? number in contact.PhoneNumbers)
            {
                ValidatePhoneNumber(number);
            }
            return ContactValidity;
        }

        /// <summary>
        /// Validating string property of contact.
        /// </summary>
        /// <param name="item">String to be validated.</param>
        /// <param name="maxLength">Max length of item.</param>
        /// <param name="nameOfItem">Name of validating property.</param>
        private static void ValidateTextItem(string item, int maxLength, string nameOfItem)
        {
            if (item.Length > maxLength)
            {
                MessageSender.SendReport("Line " + LineNumber + ": " + nameOfItem + " is too long. Line failed to import." + Environment.NewLine);
                ContactValidity = false;
            }
        }

        /// <summary>
        /// Validating birth number.
        /// </summary>
        /// <param name="birthNumber">Birth number.</param>
        private static void ValidateBirthNumber(long? birthNumber)
        {
            //item is not required
            if (birthNumber == null)
            {
                return;
            }
            
            // check if has 10 digits
            if (birthNumber > 1000000000 && birthNumber < 9999999999) 
            {
                // check if is divisible by 11
                if (birthNumber % 11 == 0) 
                {
                    return;
                }
            }
            MessageSender.SendReport("Line " + LineNumber + ": Birth number \"" + birthNumber + "\" is invalid. Line failed to import." + Environment.NewLine);
            ContactValidity = false;
        }

        /// <summary>
        /// Validating phone number.
        /// </summary>
        /// <param name="number">Phone number.</param>
        private static void ValidatePhoneNumber(int? number)
        {
            // item is not required
            if (number == null) 
            {
                return;
            }
            //check if has 9 digits
            if (number < 100000000 || number > 999999999)
            {
                MessageSender.SendReport("Line " + LineNumber + ": Phone number \"" + number + "\" is invalid. Line failed to import." + Environment.NewLine);
                ContactValidity = false;
            }
        }
    }
}
