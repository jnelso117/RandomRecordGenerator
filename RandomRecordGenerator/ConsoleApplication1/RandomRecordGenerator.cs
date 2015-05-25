/*******************************
 * Program: RandomRecordGenerator
 * Author: Jeremy Nelson, UAB
 * E-mail: jcn1990s@gmail.com
 * 
 * Info: This program was made for the sole purpose of writing random "dummy" data into a database.  Our limit was 30 records per Table for our Sponsor.
 * I decided to publish this code because it could come in handy for the next batch of students taking IS301 or wanting to dabble with using OleDBConnection to
 * write inside a database.  
 * This project consists of .txt files with 30 records each:
 * - Text file consisting of 30 Female names.
 * - Text file consisting of 30 Male names.
 * - Text file consisting of 30 Last names.
 * - Text File consisting of 30 names of Popular Operating Systems excluding Windows Phone past 8.1
 * - Text file consisting of popular Vendor names, with many repeating names.
 * 
 * If you would like to add more names, feel free to do so.  If you use my code, please give me shoutout of some sort. If you see anything that I could fix, please
 * let me know.  
 * 
 * Hopefully, this can get some beginning programmers a base to start with in for learning how to use Connection Strings and get the imagination juices flowing.
 * 
 * Ideas moving forward: Making a UI for the whole thing or using Command Line arguments for input. Finding a way to randomize the data in a way such that you won't
 * receive an Exception for going out of bounds of your Input. TL;DR, write a way to randomize the names that it guarantees no duplicates and no OutOfBounds Exceptions.
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
namespace ConsoleApplication1
{
   public class RandomRecordGenerator
    {
        static void Main(string[] args)
        {
            //Initialize some arrays that will be used
            String[] wFirstNames = System.IO.File.ReadAllLines("WFirstNames.txt");
            String[] mFirstNames = System.IO.File.ReadAllLines("MFirstNames.txt");
            String[] LastNames = System.IO.File.ReadAllLines("LastNames.txt");
            String[] vendors = System.IO.File.ReadAllLines("Vendors.txt");
            String[] compatibility = System.IO.File.ReadAllLines("Compatibility.txt");
            String[] status = { "See Doctor Immediately", "Normal", "Schedule Appointment" };
            String[] popularOS = new String[]{"Android", "Apple iOS", "Microsoft"};
            String[] specialties = { "Surgeon", "Physician", "Gynecologist" };


           /* using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Nagato\Documents\InfantMortality.accdb;
Persist Security Info=False")) {
                //You need to change the OleDbConnection String to fit your File Source for your database.  This also refers to a Database not being password protected.
                //You may add your Connection String via Project > Add New Data Source and follow the instructions.
            * Next, open the connection
            connection.Open();   
            //My Assignment was to generate 30 random records into our Tables. 
            * Consider making this number take whatever param you need via Console.ReadLine
            /* WARNINGS BEFORE DOING YOUR QUERIES:
             * IF YOUR QUERY(S) IS/ARE WRITING PK'S, AND YOU RUN THE QUERY AGAIN AFTER THE PK VALUES ARE GENERATED, YOU WILL GET A RUNTIME ERROR FOR DUPLICATES IN THE COLUMN.
             * JUST BE CAREFUL WHEN TESTING WITH PK'S.
             */
            for(int i = 0; i < 30; i++)
            {
                
                
                //Shuffle the information.
                RandomGenerator.Shuffle(wFirstNames);
                RandomGenerator.Shuffle(mFirstNames);
                RandomGenerator.Shuffle(LastNames);
                RandomGenerator.Shuffle(vendors);
                RandomGenerator.Shuffle(compatibility);

                //Query to PATIENT table
                OleDbCommand patientQueryCommand = new OleDbCommand();
                patientQueryCommand.Connection = connection;
                patientQueryCommand.CommandText = "INSERT INTO PATIENT (PATIENT_EMAIL, PATIENT_FNAME, PATIENT_LNAME, PATIENT_WEIGHT, PATIENT_HEIGHT, PATIENT_TRIMESTER, PATIENT_PASSWRD, CONDITION_ID) VALUES (?,?,?,?,?,?,?,?)";
                patientQueryCommand.Parameters.Add("PATIENT_EMAIL", OleDbType.LongVarChar);
                patientQueryCommand.Parameters.Add("PATIENT_FNAME", OleDbType.LongVarChar);
                patientQueryCommand.Parameters.Add("PATIENT_LNAME", OleDbType.LongVarChar);
                patientQueryCommand.Parameters.Add("PATIENT_WEIGHT", OleDbType.Numeric);
                patientQueryCommand.Parameters.Add("PATIENT_HEIGHT", OleDbType.Numeric);
                patientQueryCommand.Parameters.Add("PATIENT_TRIMESTER", OleDbType.Numeric);
                patientQueryCommand.Parameters.Add("PATIENT_PASSWRD", OleDbType.LongVarChar);
                patientQueryCommand.Parameters.Add("CONDITION_ID", OleDbType.LongVarChar);
                patientQueryCommand.Parameters[0].Value = RandomGenerator.randomEmail();
                patientQueryCommand.Parameters[1].Value = wFirstNames[i];
                patientQueryCommand.Parameters[2].Value = LastNames[i];
                patientQueryCommand.Parameters[3].Value = RandomGenerator.randomNumber(130, 140);
                patientQueryCommand.Parameters[4].Value = RandomGenerator.randomNumber(66, 70);
                patientQueryCommand.Parameters[5].Value = RandomGenerator.randomNumber(1, 3);
                patientQueryCommand.Parameters[6].Value = RandomGenerator.randomPassword();
                patientQueryCommand.Parameters[7].Value = RandomGenerator.shuffleReturn(status);

                //Query to PERIPHERAL Table
                OleDbCommand peripheralQueryCommand = new OleDbCommand();
                peripheralQueryCommand.Connection = connection;
                peripheralQueryCommand.CommandText = "INSERT INTO PERIPHERAL (P_FDA_NUM, P_SYS_COMPAT) VALUES (?,?)";
                peripheralQueryCommand.Parameters.Add("P_FDA_NUM", OleDbType.Numeric);
                peripheralQueryCommand.Parameters.Add("P_SYS_COMPAT", OleDbType.LongVarChar);
                peripheralQueryCommand.Parameters[0].Value = RandomGenerator.randomNumber(1000000, 1500000);
                peripheralQueryCommand.Parameters[1].Value = RandomGenerator.shuffleReturn(compatibility);
               
                //Query to VENDOR Table
                OleDbCommand venderQueryCommand = new OleDbCommand();
                venderQueryCommand.Connection = connection;
                venderQueryCommand.CommandText = "INSERT INTO VENDOR (P_SYS_COMPAT, P_FDA_NUM) VALUES (?,?)";
                venderQueryCommand.Parameters.Add("P_SYS_COMPAT", OleDbType.LongVarChar);
                venderQueryCommand.Parameters.Add("P_FDA_NUM", OleDbType.Numeric);
                venderQueryCommand.Parameters[0].Value = RandomGenerator.shuffleReturn(compatibility);
                venderQueryCommand.Parameters[1].Value = RandomGenerator.randomNumber(1000000, 1500000);

                //Query to COMPATIBILITY Table
                OleDbCommand compatibilityQueryCommand = new OleDbCommand();
                compatibilityQueryCommand.Connection = connection;
                compatibilityQueryCommand.CommandText = "INSERT INTO COMPATIBILITY (DEVICE_OS, DEVICE_COMPATIBILITY) VALUES (?,?)";
                compatibilityQueryCommand.Parameters.Add("DEVICE_OS", OleDbType.LongVarChar);
                compatibilityQueryCommand.Parameters.Add("DEVICE_COMPATIBILITY", OleDbType.LongVarChar);
                compatibilityQueryCommand.Parameters[0].Value = compatibility[i];
                if (compatibility[i].Contains("Android"))
                {
                    compatibilityQueryCommand.Parameters[1].Value = "Android";
                }
                else if (compatibility[i].Contains("Apple"))
                {
                    compatibilityQueryCommand.Parameters[1].Value = "Apple";
                }
                else if (compatibility[i].Contains("Windows"))
                {
                    compatibilityQueryCommand.Parameters[1].Value = "Microsoft";
                }
                else compatibilityQueryCommand.Parameters[1].Value = "Unknown";

                // Query for DEVICE Table

                OleDbCommand deviceQueryCommand = new OleDbCommand();
                deviceQueryCommand.Connection = connection;
                deviceQueryCommand.CommandText = "INSERT INTO DEVICE (DEVICE_SKU, DEVICE_VENDOR) VALUES (?,?)";
                deviceQueryCommand.Parameters.Add("DEVICE_SKU", OleDbType.LongVarChar);
                deviceQueryCommand.Parameters.Add("Device_Vendor", OleDbType.LongVarChar);
                deviceQueryCommand.Parameters[0].Value = RandomGenerator.randomSKU();
                deviceQueryCommand.Parameters[1].Value = vendors[i];

                //Query for DOCTOR Table
                OleDbCommand doctorQueryCommand = new OleDbCommand();
                doctorQueryCommand.Connection = connection;
                doctorQueryCommand.CommandText = "INSERT INTO DOCTOR (SPECIALTY_1, SPECIALTY_2, SPECIALTY_3, DOC_CONTACT) VALUES (?,?,?,?)";
                doctorQueryCommand.Parameters.Add("SPECIALTY_1", OleDbType.LongVarChar);
                doctorQueryCommand.Parameters.Add("SPECIALTY_2", OleDbType.LongVarChar);
                doctorQueryCommand.Parameters.Add("SPECIALTY_3", OleDbType.LongVarChar);
                doctorQueryCommand.Parameters.Add("DOC_CONTACT", OleDbType.LongVarChar);
                doctorQueryCommand.Parameters[0].Value = RandomGenerator.shuffleReturn(specialties);
                doctorQueryCommand.Parameters[1].Value = RandomGenerator.shuffleReturn(specialties);
                doctorQueryCommand.Parameters[2].Value = RandomGenerator.shuffleReturn(specialties);
                doctorQueryCommand.Parameters[3].Value = RandomGenerator.randomPhone();

                //Queries are not pushed until the ExecuteNonQuery method is provoked.
                 //* I recommend writing try-catch statements here, but I didn't do it because I didn't know what error I should catch.
                patientQueryCommand.ExecuteNonQuery();
                Console.WriteLine("Patient records generated.");
                peripheralQueryCommand.ExecuteNonQuery();
                Console.WriteLine("Peripheral records generated");
                venderQueryCommand.ExecuteNonQuery();
                Console.WriteLine("Vendor records generated");
                //May get an error if it tries to commit the same values for PK.
                compatibilityQueryCommand.ExecuteNonQuery();
                Console.WriteLine("Compatibility records generated");
                deviceQueryCommand.ExecuteNonQuery();
                Console.WriteLine("Device records generated");
                doctorQueryCommand.ExecuteNonQuery();
                Console.WriteLine("Doctor records generated");

                




            }
            //Always close string connection
            //connection.Close();
        }
            //Console.WriteLine("Contents pushed successfully");
            //Console.ReadLine();
            
        }
    }
//}
