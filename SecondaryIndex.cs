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
                String PK = file[i].primaryKey;
                String SK = file[i].secondaryKey;

                char[] PKchar;
                int primaryKeyLen = 6;

                char[] SKchar;
                int secondaryKeyLen = 20;

                char[] record;
                int recordLen = 26;

                PKchar = new char[primaryKeyLen];
                SKchar = new char[secondaryKeyLen];
                record = new char[recordLen];

                PK.CopyTo(0, PKchar, 0, PK.Length);
                SK.CopyTo(0, SKchar, 0, SK.Length);


                SKchar.CopyTo(record, 0);
                PKchar.CopyTo(record, primaryKeyLen);


                sw.Write(record);

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

                    int primaryKeyLen = 6;
                    int secondaryKeyLen = 20;
                    int recordLen = 26;

                    char[] record = new char[recordLen];

                    sr.Read(record, 0, recordLen);

                    String primaryKey = new String(record).Substring(0, primaryKeyLen);
                    String secondaryKey = new String(record).Substring(primaryKeyLen, secondaryKeyLen);

                    file.Add(new SecondaryIndex(primaryKey, secondaryKey));
                }
            }
        }

    }
}
