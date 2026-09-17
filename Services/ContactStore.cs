using System.Text.Json;
using ContactApp.Models;


namespace ContactApp.Services
{
    public class ContactStore
    {
        //Variables to handle I/O
        private readonly string _dataDir;
        private readonly string _dataFile;
        private readonly object _lock = new();
        private readonly JsonSerializerOptions _json = new() { WriteIndented = true };

        //List of Contacts to hold form submissions in RAM
        private List<Contact> _cache = new();


        //Constructor to handle working in the web environment
        public ContactStore(IWebHostEnvironment env)
        {
            //set the pathing
            _dataDir = Path.Combine(env.ContentRootPath, "data");
            _dataFile = Path.Combine(_dataDir, "contacts.json");


            //create the directory if it doesn't exist
            Directory.CreateDirectory(_dataDir);

            //Load all form responses
            LoadFromDisk();
        }


        //method to get all form submissions
        public IReadOnlyList<Contact> GetAll()
        {
            lock (_lock) return _cache.ToList();
        }

        //void to add a contact
        public void Add(Contact c) //prevent readers/writers problem
        {
            lock ( _lock)
            {
                _cache.Add(c); //Adds the contact to the list of contacts
                SaveToDisk(); //Immedietly saves the Json File
            }
        }

        //void to load from the disk 
        private void LoadFromDisk()
        {
            //Handle if the Json does not exist
            if (!File.Exists(_dataFile)) { _cache = new(); return; }

            try
            {
                var text = File.ReadAllText(_dataFile);
                _cache = JsonSerializer.Deserialize<List<Contact>>(text) ?? new();
            }
            catch
            {
                _cache = new(); //if corrupted, start fresh
            }
        }
        

        //void to save to disk
        private void SaveToDisk()
        {
            var text = JsonSerializer.Serialize(_cache, _json);
            File.WriteAllText(_dataFile, text);
        }
    }
}
