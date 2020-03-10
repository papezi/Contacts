using contactManager.src.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using contactManager.database.contacts;
using contactManager.src.Utils;

namespace contactManagerTests
{
    [TestClass]
    public class ContactValidation
    {
        [TestMethod]
        public void CorrectComplete()
        {
            ContactDTO ValidContactDTO1 = new ContactDTO
            {
                Name = "Jan",
                Surname = "Novak",
                Address = "Doma na dvorku 35",
                BirthNumber = 7054251237,
                PhoneNumbers = new int?[] { 222222222, 222222222 }
            };
            Assert.IsTrue(ContactValidator.ValidateContact(ValidContactDTO1, 5));
        }

        [TestMethod]
        public void CorrectEmptyValues()
        {
            ContactDTO ValidContactDTO2 = new ContactDTO
            {
                Name = "",
                Surname = "Novak",
                Address = "Doma na dvorku 35",
                BirthNumber = 7054251237,
                PhoneNumbers = new int?[] { }
            };
            Assert.IsTrue(ContactValidator.ValidateContact(ValidContactDTO2, 5));
        }

        [TestMethod]
        public void CorrectAllButSurnameEmpty()
        {
            ContactDTO ValidContactDTO3 = new ContactDTO
            {
                Name = "",
                Surname = "Novak",
                Address = "",
                BirthNumber = null,
                PhoneNumbers = new int?[] { }
            };
            Assert.IsTrue(ContactValidator.ValidateContact(ValidContactDTO3, 5));
        }

        [TestMethod]
        public void NotCorrectSurnameMissing()
        {
            ContactDTO NotValidContactDTO1 = new ContactDTO
            {
                Name = "Jan",
                Surname = "",
                Address = "",
                BirthNumber = 7054251237,
                PhoneNumbers = new int?[] { 222222222 }
            };
            Assert.IsFalse(ContactValidator.ValidateContact(NotValidContactDTO1, 5));
        }

        [TestMethod]
        public void NotCorrectBirthNumberMod()
        {
            ContactDTO NotValidContactDTO2 = new ContactDTO
            {
                Name = "Jan",
                Surname = "Novak",
                Address = "",
                BirthNumber = 7054251236, //mod 11 = 10 (not 0)
                PhoneNumbers = new int?[] { 222222222 }
            };
            Assert.IsFalse(ContactValidator.ValidateContact(NotValidContactDTO2, 5));
        }

        [TestMethod]
        public void NotCorrectBirthNumberShort()
        {
            ContactDTO NotValidContactDTO3 = new ContactDTO
            {
                Name = "Jan",
                Surname = "Novak",
                Address = "",
                BirthNumber = 11,
                PhoneNumbers = new int?[] { 222222222 }
            };
            Assert.IsFalse(ContactValidator.ValidateContact(NotValidContactDTO3, 5));
        }

        [TestMethod]
        public void NotCorrectBirthNumberLong()
        {
            ContactDTO NotValidContactDTO3 = new ContactDTO
            {
                Name = "Jan",
                Surname = "Novak",
                Address = "",
                BirthNumber = 12345678906,
                PhoneNumbers = new int?[] { 222222222 }
            };
            Assert.IsFalse(ContactValidator.ValidateContact(NotValidContactDTO3, 5));
        }

        [TestMethod]
        public void NotCorrectPhoneShort()
        {
            ContactDTO NotValidContactDTO4 = new ContactDTO
            {
                Name = "Jan",
                Surname = "Novak",
                Address = "",
                BirthNumber = 7054251237,
                PhoneNumbers = new int?[] { 6 }
            };
            Assert.IsFalse(ContactValidator.ValidateContact(NotValidContactDTO4, 5));
        }
    }

    [TestClass]
    public class ContactParseItemsToDTO
    {
        [TestMethod]
        public void CorrectAllItems()
        {
            ContactDTO contact = new ContactDTO
            {
                Name = "Jan",
                Surname = "Novak",
                Address = "a b 5",
                BirthNumber = 7054251237,
                PhoneNumbers = new int?[] { 123456789 , null, null}
            };

            string[] items = new string[] { "Jan", "Novak", "7054251237", "a b 5", "123456789" };
            Assert.AreSame(contact.ToString(), ContactImporter.ParseLineToContactDTO(items, 4).ToString());
        }

        [TestMethod]
        public void CorrectEmptyItems()
        {
            ContactDTO contact = new ContactDTO
            {
                Name = "",
                Surname = "Novak",
                Address = "",
                BirthNumber = 7054251237,
                PhoneNumbers = new int?[] { 123456789, 987654321, 987456321 }
            };

            string[] items = new string[] { "", "Novak", "7054251237", "", "123456789", "987654321", "987456321"};
            Assert.AreSame(contact.ToString(), ContactImporter.ParseLineToContactDTO(items, 4).ToString());
        }

        [TestMethod]
        public void NotCorrectCharInBirthNumber()
        {
            string[] items = new string[] { "", "Novak", "7054f51237", "", "123456789", "987654321", "987456321" };
            Assert.IsNull(ContactImporter.ParseLineToContactDTO(items, 4));
        }

        [TestMethod]
        public void NotCorrectCharInPhone()
        {
            string[] items = new string[] { "", "Novak", "7054251237", "", "123456789", "987ghu21", "98x99v321" };
            Assert.IsNull(ContactImporter.ParseLineToContactDTO(items, 4));
        }
    }
}
