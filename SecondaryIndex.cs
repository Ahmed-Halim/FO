using System;
using System.Collections.Generic;
using System.IO;

namespace Indexing
{
    class SecondaryIndex
    {
        public static List<SecondaryIndex> file = new List<SecondaryIndex>();

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
            file.Add(new SecondaryIndex(primaryKey, secondaryKey));
        }

        public void update(string primaryKey, String secondaryKey)
        {
            for (int i = 0; i < file.Count; i++)
            {
                if (file[i].primaryKey == primaryKey)
                {
                    file[i].secondaryKey = secondaryKey;
                    break;
                }
            }
        }

        public static List<String> getStudentsByName(String name)
        {
            List<String> result = new List<String>();

            for (int i = 0; i < file.Count; i++)
            {
                if (file[i].secondaryKey.Trim('\0') == name)
                {
                    result.Add(file[i].primaryKey);
                }

            }

            return result;
        }

        public static void saveToFile()
        {
            File.Delete("Secondary.txt");

            FileStream fs = new FileStream("Secondary.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < file.Count; i++)
            {

                String current = file[i].primaryKey + " " + file[i].secondaryKey;
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
                    file.Add(new SecondaryIndex(splitted[0], splitted[1]));

                }
            }
        }

    }
}
