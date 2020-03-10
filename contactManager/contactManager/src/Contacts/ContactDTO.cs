using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactManager.src.Contacts
{
    /// <summary>
    /// Contact data tranfer object.
    /// </summary>
    public class ContactDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long? BirthNumber { get; set; }
        public string Address { get; set; }
        public int?[] PhoneNumbers { get; set; } = new int?[3];
    }
}
