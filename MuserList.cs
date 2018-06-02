using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Muser.Models;
using System.Data.SqlClient;
using Muser.Utilities;

namespace MyDataGenerator
{
    public class MuserList
    {
        public void createMusers(int listSize)
        {
            for (int muserLoop = 0; muserLoop < listSize; muserLoop++)
            {
                Muser muser = new Muser(muserLoop);
                Instrument have = new Instrument();
                Instrument seek = new Instrument();
                Genre genre = new Genre();

                Guid testUser = muser.ID;

                //confirm variabales in console
                string muserOut = "\n" + muser.ID + " = " + muser.FirstName + " " + muser.LastName + " (" + muser.Username + ")";
                Console.WriteLine(muserOut);

                // hash password
                Security dataGenSecurity = new Security();
                string hashedPassword = dataGenSecurity.StartPasswordHash(muser.Password);
                string salt = dataGenSecurity.ReturnSalt();
                Console.WriteLine(hashedPassword);
                Console.WriteLine(salt);
                

                //SQL Magic
                string datasource = "Data Source=muserdb-aus.database.windows.net;Initial Catalog=musertestbed_db;Integrated Security=False;User ID=hendrix;Password=asdf123!;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection sqlConnection = new SqlConnection(datasource);
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;

                sqlConnection.Open();

                command.CommandText = "INSERT INTO ProfileModels (User_ID, Username, First_Name, Last_Name, " +
                    "Location, Email, Bio, Online_Collaboration) " +
                    "VALUES (@id, @username, @fName, @lName, @location, @email, @bio, @collab)";
                command.Parameters.AddWithValue("@id", muser.ID);
                command.Parameters.AddWithValue("@username", muser.Username);
                command.Parameters.AddWithValue("@fName", muser.FirstName);
                command.Parameters.AddWithValue("@lName", muser.LastName);
                command.Parameters.AddWithValue("@location", muser.Location);
                command.Parameters.AddWithValue("@email", muser.EmailAddress);
                command.Parameters.AddWithValue("@bio", muser.Bio);
                command.Parameters.AddWithValue("@collab", muser.OnlineCollaboration);
                command.Connection = sqlConnection;
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO AspNetUsers (ID, UserName, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, " +
                    "TwoFactorEnabled, LockoutEnabled, AccessFailedCount, First_Name, Last_Name, Location, Email, " +
                    "Bio, Online_Collaboration, Instrument, Genre) " +
                    "VALUES (@aspId, @aspUsername, @aspConfirmed, @aspPasswordHash, @aspSecurityStamp, @aspPhoneConfirmed, @aspTwoFactor, @aspLockout, " +
                    "@aspAccessFailed, @aspFName, @aspLName, @aspLocation, @aspEmail, @aspBio, @aspCollab, " +
                    "@aspInstrument, @aspGenre)";

                command.Parameters.AddWithValue("@aspId", muser.ID);
                command.Parameters.AddWithValue("@aspUsername", muser.Username);
                command.Parameters.AddWithValue("@aspConfirmed", false);
                command.Parameters.AddWithValue("@aspPasswordHash", hashedPassword);
                command.Parameters.AddWithValue("@aspSecurityStamp", salt);
                command.Parameters.AddWithValue("@aspTwoFactor", false);
                command.Parameters.AddWithValue("@aspPhoneConfirmed", false);
                command.Parameters.AddWithValue("@aspLockout", false);
                command.Parameters.AddWithValue("@aspAccessFailed", 0);
                command.Parameters.AddWithValue("@aspFName", muser.FirstName);
                command.Parameters.AddWithValue("@aspLName", muser.LastName);
                command.Parameters.AddWithValue("@aspLocation", muser.Location);
                command.Parameters.AddWithValue("@aspEmail", muser.EmailAddress);
                command.Parameters.AddWithValue("@aspBio", muser.Bio);
                command.Parameters.AddWithValue("@aspCollab", muser.OnlineCollaboration);
                command.Parameters.AddWithValue("@aspInstrument", 0);
                command.Parameters.AddWithValue("@aspGenre", 0);
                command.Connection = sqlConnection;
                command.ExecuteNonQuery();

                bool[] InstrumentArr = { have.LeadGuitar, have.RhythmGuitar, have.Vocals, have.Bass, have.Keyboard, have.Drums };
                int[] MyInstrumentID = { 1, 2, 3, 4, 5, 6 };

                // write to Instruments table
                int count = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (InstrumentArr[i] == true)
                    {
                        command.Parameters.Clear();
                        command.CommandText = "INSERT INTO Instruments (User_Id, InstrumentID) VALUES (@id, @instrument)";
                        command.Parameters.AddWithValue("@id", muser.ID);
                        command.Parameters.AddWithValue("@instrument", MyInstrumentID[i]);
                        command.ExecuteNonQuery();
                        count++;
                    }
                }
                if (count == 0)
                {
                    command.Parameters.Clear();
                    command.CommandText = "INSERT INTO Instruments (User_Id, InstrumentID) VALUES (@id, @instrument)";
                    command.Parameters.AddWithValue("@id", muser.ID);
                    command.Parameters.AddWithValue("@instrument", MyInstrumentID[0]);
                    command.ExecuteNonQuery();
                    count++;
                }
                
                // write to Looking_For table
                count = 0;
                for (int i = 0, j = 5; i < 6; i++, j--)
                {
                    if (InstrumentArr[i] == true)
                    {
                        command.Parameters.Clear();
                        command.CommandText = "INSERT INTO Looking_For (User_Id, InstrumentID) VALUES (@id, @instrument)";
                        command.Parameters.AddWithValue("@id", muser.ID);
                        command.Parameters.AddWithValue("@instrument", MyInstrumentID[i]);
                        command.ExecuteNonQuery();
                        count++;
                    }
                }
                if (count == 0)
                {
                    command.Parameters.Clear();
                    command.CommandText = "INSERT INTO Looking_For (User_Id, InstrumentID) VALUES (@id, @instrument)";
                    command.Parameters.AddWithValue("@id", muser.ID);
                    command.Parameters.AddWithValue("@instrument", MyInstrumentID[0]);
                    command.ExecuteNonQuery();
                }

                 // write to Genres table
                bool[] GenreArr = { genre.Indie, genre.Metal, genre.Folk, genre.Rock, genre.Pop, genre.Jazz };
                int[] GenreID = { 1, 2, 3, 4, 5, 6 };
                for (int i = 0; i < 6; i++)
                {
                    if (GenreArr[i] == true)
                    {
                        command.Parameters.Clear();
                        command.CommandText = "INSERT INTO Genres (User_Id, GenreID) VALUES (@id, @genre)";
                        command.Parameters.AddWithValue("@id", muser.ID);
                        command.Parameters.AddWithValue("@genre", GenreID[i]);
                        command.ExecuteNonQuery();
                        count++;
                    }
                }
                if (count == 0)
                {
                    command.Parameters.Clear();
                    command.CommandText = "INSERT INTO Genres (User_Id, GenreID) VALUES (@id, @genre)";
                    command.Parameters.AddWithValue("@id", muser.ID);
                    command.Parameters.AddWithValue("@genre", GenreID[0]);
                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }
        }
    }
}
