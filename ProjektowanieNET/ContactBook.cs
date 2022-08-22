using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using static ProjektowanieNET.Program;

namespace ProjektowanieNET
{
    internal class ContactBook
    {
        private IEnumerable<Contact> contacts;

        private static ContactBook _k = null!;

        public static ContactBook Book
        { 
            get
            {
                if (_k is null)
                    _k = new ContactBook();
                return _k;
            }
        }

        private ContactBook()
        {
            contacts = new List<Contact>();

            Load();
        }

        public IEnumerable<Contact> Contacts => contacts;

        public IEnumerable<Contact> Birthdays
        {
            get
            {
                return contacts.Where(k => {
                    var birthdayDay = new DateTime(DateTime.Now.Year, k.Birthday.Month, k.Birthday.Day);
                    var diff = (birthdayDay - DateTime.Now).TotalDays;

                    return diff < 7 && diff > -1;
                });
            }
        }

        public IEnumerable<Contact> Search(string s)
        {
            return contacts.Where(k => k.Matches(s));
        }

        public IEnumerable<Contact> SearchBirthday(string s)
        {
            return Birthdays.Where(u => u.Matches(s));
        }

        public void Remove(Contact k)
        {
            if (k.Id is null)
                throw new Exception("Nieoczekiwany błąd");

            contacts = contacts.Where(kk => kk.Id != k.Id);
            Save();
        }

        public void Edit(int id, Contact nowy)
        {
            nowy.Id = id;

            var klist = contacts.ToList();

            var i = klist.FindIndex(kk => kk.Id == nowy.Id);
            if (i == -1)
                throw new Exception("Nie znaleziono kontaktu");

            klist.RemoveAt(i);
            klist.Insert(i, nowy);

            contacts = klist;

            contacts = contacts.Where(kk => kk.Id != nowy.Id).Concat(new [] { nowy });
            Save();
        }


        public void Add(Contact k)
        {   
            k.Id = contacts.Count();

            while (contacts.Where(kk => kk.Id == k.Id).Any())
                k.Id++;

            contacts = contacts.Concat(new[] { k });
            Save();
        }

        private void Load()
        {
            if (!File.Exists(file))
            {
                var pusty = JsonSerializer.Serialize<IEnumerable<Contact>>(contacts);
                File.WriteAllText(file, pusty);
            }

            var dane = File.ReadAllText(file);
            var przeczytane = JsonSerializer.Deserialize<IEnumerable<Contact>>(dane);

            if (przeczytane is null)
            {
                throw new Exception("Cannot load the contacts");
            }

            contacts = przeczytane.ToList();
        }

        private void Save()
        {
            if (!File.Exists(file))
            {
                throw new Exception($"Storage file {file} does not exist");
            }

            var json = JsonSerializer.Serialize<IEnumerable<Contact>>(contacts);
            File.WriteAllText(file, json);

            Load();
        }
    }
}
