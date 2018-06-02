using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDataGenerator
{
    public class Muser
    {
        // filepath to creator files
        const string myPath = "..\\..\\..\\";

        // declare all variables needed
        public Guid ID { set; get; }
        public string EmailAddress { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string FirstName { set; get; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public string Skill { get; set; } // MISSING ON DB?
        public bool OnlineCollaboration { get; set; }


        // import list and make name/bios/place arrays
        public string[] FirstNames = File.ReadAllLines(@myPath + "names1.csv");
        public string[] LastNames = File.ReadAllLines(@myPath + "names2.csv");
        public string[] Adjectives = File.ReadAllLines(@myPath + "adjectives.csv");
        public string[] Bios = File.ReadAllLines(@myPath + "bios.csv");
        public string[] Skills = { "Beginner", "Amateur", "Expert" };
        public string[] Locations = { "Sydney", "Brisbane", "Adelaide", "Melbourne", "Perth", "Darwin", "Canberra", "Hobart" };


        // set random number for this muser loop
        int randNum;
        Random rnd = new Random();


        // create id
        public string getID(int muserLoop)
        {
            string myID = DateTime.Now.ToString("9mmss" + muserLoop);
            return myID;
        }


        // create first name
        public string getFirstName()
        {
            while ((randNum = (rnd.Next() % FirstNames.Length)) > FirstNames.Length - 1) ;
            return FirstNames[randNum];
        }

        // create second name
        public string getLastName()
        {
            while ((randNum = (rnd.Next() % LastNames.Length)) > LastNames.Length - 1) ;
            return LastNames[randNum];
        }

        // create username
        public string getUsername()
        {
            while ((randNum = (rnd.Next() % Adjectives.Length)) > Adjectives.Length - 1) ;
            return Adjectives[randNum] + FirstName + (randNum + 1);
        }

        // create email
        public string getEmailAddress(int muserLoop, string username)
        {
            return username + muserLoop + "@muser.mus";
        }


        // create location
        public string getLocation()
        {
            while ((randNum = (rnd.Next() % Locations.Length)) > Locations.Length - 1) ;
            return Locations[randNum];
        }

        // create bio
        public string getBio()
        {
            while ((randNum = (rnd.Next() % Bios.Length)) > Bios.Length - 1) ;
            string bioLine1 = Bios[randNum];
            while ((randNum = (rnd.Next() % Bios.Length)) > Bios.Length - 1) ;
            string bioLine2 = Bios[randNum];
            while ((randNum = (rnd.Next() % Bios.Length)) > Bios.Length - 1) ;
            string bioLine3 = Bios[randNum];
            return "\"" + bioLine1 + " " + bioLine2 + " " + bioLine3 + "\"";
        }

        // create password
        public string getPassword()
        {
            return "Pa$$word01!";
        }

        // create online collab status
        public bool getOnlineCollaboration()
        {
            randNum = (rnd.Next() % 2);
            if (randNum == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // create skill level
        public string getSkill()
        {
            randNum = (rnd.Next() % 3);
            if (randNum == 0)
            {
                return Skills[0];
            }
            else if (randNum == 1)
            {
                return Skills[1];
            }
            else
            {
                return Skills[2];
            }
        }

        public void ReturnGuidString()
        {
            string connectionString = "Data Source=muserdb-aus.database.windows.net;Initial Catalog=musertestbed_db;Integrated Security=False;User ID=hendrix;Password=asdf123!;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT NEWID()";
            comm.Connection = conn;
            conn.Open();
            SqlDataReader sqlDataReader = comm.ExecuteReader();

            while (sqlDataReader.Read())
            {
                ID = sqlDataReader.GetGuid(0); // shits itself here???
            }
        }

        // muser constructor
        public Muser(int muserLoop)
        {
            ReturnGuidString();
            FirstName = getFirstName();
            LastName = getLastName();
            Username = getUsername();
            EmailAddress = getEmailAddress(muserLoop, Username);
            EmailConfirmed = false;
            Username = EmailAddress;
            Location = getLocation();
            Bio = getBio();
            Password = getPassword();
            Skill = getSkill();
            OnlineCollaboration = getOnlineCollaboration();

            // ImageUrl = getImageUrl(muserLoop);
            // Instrument = getInstrument();
            // Genre = getGenre();
            // Seeking = getSeeking();

            // confirm variabales in console
            string muserOut = "\n" + ID + " = " + FirstName + " " + LastName + " (" + Username + ")";
            Console.WriteLine(muserOut);
        }
    }
}