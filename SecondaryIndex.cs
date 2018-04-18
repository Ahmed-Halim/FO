using System;
using System.Collections.Generic;
using System.IO;

namespace Indexing
{
    class SecondaryIndex
    {
        public static List<SecondaryIndex> Sfile = new List<SecondaryIndex>();

        String primaryKey;
        String secondaryKey;

        public SecondaryIndex() { }

        public SecondaryIndex(String primaryKey, String secondaryKey)
        {
            this.primaryKey = primaryKey;
            this.secondaryKey = secondaryKey;
        }


        public static void add(String primaryKey, String secondaryKey)
        {
            Sfile.Add(new SecondaryIndex(primaryKey, secondaryKey));
        }

        public static void update(String primaryKey, String secondaryKey)
        {
            for (int i = 0; i < Sfile.Count; i++)
            {
                if (Sfile[i].primaryKey == primaryKey)
                {
                    Sfile[i].secondaryKey = secondaryKey;
                    break;
                }
            }
        }

        public static List<String> getStudentsByName(String name)
        {
            List<String> result = new List<String>();

            for (int i = 0; i < Sfile.Count; i++)
            {
                if (Sfile[i].secondaryKey.Trim('\0') == name)
                {
                    result.Add(Sfile[i].primaryKey);
                }

            }

            return result;
        }

        public static void saveToFile()
        {
            File.Delete("Secondary.txt");

            FileStream fs = new FileStream("Secondary.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < Sfile.Count; i++)
            {

                String current = Sfile[i].primaryKey + " " + Sfile[i].secondaryKey;
                sw.WriteLine(current);

            }

            sw.Close();
            fs.Close();
        }

        public static void loadFromFile()
        {
            if (File.Exists("Secondary.txt"))
            {
                FileStream fs = new FileStream("Secondary.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                while (sr.Peek() != -1)
                {
                    
                    string[] splitted = sr.ReadLine().Split(' ');
                    Sfile.Add(new SecondaryIndex(splitted[0], splitted[1]));

                }
            }
        }

    }
}
