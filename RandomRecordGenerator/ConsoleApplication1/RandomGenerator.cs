/// <Summary>
///  @author Jeremy Nelson @ The University of Alabama at Birmingham.
///  This is a Random Record Generator API to create Dummy test tuples into a database.  I wrote this during my DBMS class.
///  Professor: Dr. Samuel Thompson
///  My Project: Infant Mortality
///  Team Members: Neeti Thakral, Michael McGrath, and Rolando Davis.
///  Upon completing my sample Database for class, I decided to clean it up, extend it a litle bit more, and publish for other students to use
/// </Summary  
 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class RandomGenerator
    {
        //Use this static random object for all the methods
        static Random _random = new Random();

        /// <randomSKU>
        /// No Parameters
        /// Returns a random String of numbers and alphanumerical values.
        /// </randomSKU>
        /// <returns>
        /// Returns a String value and a length of 10 characters
        /// </returns>
        public static String randomSKU()
        {
            //Randomizes to get the SKU
            Random random = new Random();
            Double number = random.Next(0, 10);
            Double stringLength = 10;
            String allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            char[] token = { ',' };
            String[] characters = allowedChars.Split(token);
            String temp = "";
            String stringID = "";
            String sku = "";
            for (int i = 0; i < stringLength; i++)
            {
                temp = characters[(random.Next(0, characters.Length))];
                stringID += temp;
                sku = stringID;
            }
            return sku;
        }
        /// <randomSKU(int N)>
        /// Generates a random String of numbers and alphanumerical values.
        /// </randomSKU(int N>
        /// <param name="N">
        /// Length of the SKU
        /// </param>
        /// <returns>Returns a String value of N length</returns>
        public static String randomSKU(int N)
        {
            //Randomizes to get the SKU
            Random random = new Random();
            Double number = random.Next(0, 10);
            Double stringLength = N;
            String allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            char[] token = { ',' };
            String[] characters = allowedChars.Split(token);
            String temp = "";
            String stringID = "";
            String sku = "";
            for (int i = 0; i < stringLength; i++)
            {
                temp = characters[(random.Next(0, characters.Length))];
                stringID += temp;
                sku = stringID;
            }
            return sku;
        }
        /// <randomEmail>
        /// Generates a random E-mail with a maximum length of 10 characters before the host value.
        /// Characters used in the generation are are 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// Possible host values: @gmail.com, @hotmail.com, @yahoo.com
        /// </summary>
        /// <returns> Returns a String with random 10 random characters + hostname</returns>
        public static String randomEmail()
        {
            //Generates Random E-mail with the character limit being 10 before the host is added to the end of it.  
            Random random = new Random();
            Double number = random.Next(0, 10);
            String[] hosts = new String[] { "@gmail.com", "@hotmail.com", "@yahoo.com" };
            Double stringLength = 10;
            String allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            char[] token = { ',' };
            String[] characters = allowedChars.Split(token);
            String temp = "";
            String stringID = "";
            String email = "";
            for (int i = 0; i < stringLength; i++)
            {
                temp = characters[(random.Next(0, characters.Length))];
                stringID += temp;
                email = stringID;
            }

            return email + shuffleReturn(hosts);

        }
        public static String randomPhone()
        {
            //Random phone number.  Phone number has a prefix, suffix, and an extension (typically).  
            Random r = new Random();
            int prefix = r.Next(100, 999);
            int suffix = r.Next(100, 999);
            int extension = r.Next(1000, 9999);
            return prefix.ToString() + "-" + suffix.ToString() + "-" + extension.ToString();
        }

        public static String randomPassword()
        {
            Double passwordLength = 12;
            String allowedChars = "";
            String password = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowedChars += "!,@,#,$,%,^,*,(,),_,`,~,?";
            char[] token = { ',' };
            //Using the , delimiter, allowedChars is broken up into an array of all the characters listed above
            String[] characters = allowedChars.Split(token);

            String temp = "";
            String stringID = "";
            Random r = new Random();
            for (int i = 0; i < passwordLength; i++)
            {
                temp = characters[r.Next(0, characters.Length)];
                stringID += temp;
                password = stringID;
            }
            return password;
        }
        public static void Shuffle<T>(T[] array)
        {
            //Fisher-Yates Shuffle Algorithm to Shuffle an Array.
            //Shuffles contents in an array.  
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {

                int r = i + (int)(_random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }
        public static String shuffleReturn<T>(T[] array)
        {
            //Shuffles an array and returns the first element in the array as a String.  
            Shuffle(array);
            try
            {
                return array[0].ToString();
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Please ensure that the array length is greater than 0");
            }
        }
        public static int randomNumber(int min, int max)
        {
            Random r = new Random();
            int number = r.Next(min, max);
            return number;
        }
    }
}
