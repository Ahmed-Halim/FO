using System;
using System.Collections.Generic;
using System.IO;

namespace Indexing
{
    class PrimaryIndex {

        public static List<PrimaryIndex> file = new List<PrimaryIndex>();
        
        private String primaryKey;
        private int recordNumber;

        public PrimaryIndex() { }

        public PrimaryIndex(String primaryKey, int recordNumber)
        {
            this.primaryKey = primaryKey;
            this.recordNumber = recordNumber;
        }

        public void update(int recordNumber, String newKey)
        {
            for (int i = 0; i < file.Count; i++)
            {
                if (file[i].recordNumber == recordNumber)
                {
                    file[i].primaryKey = newKey;
                    break;
                }
            }
        }

        public void add(String key)
        {
            this.primaryKey = key;
            this.recordNumber = file.Count;
            file.Add(this);
        }

        public static int getStudentByID(String key)
        {
            for (int i = 0; i < file.Count; i++)
            {
                if (key == file[i].primaryKey.Trim('\0'))
                {
                    return file[i].recordNumber;
                }
            }
            return -1;
        }

        public static void saveToFile()
        {
            if (File.Exists("Primary.txt"))
            {
                File.Delete("Primary.txt");
            }

            FileStream fs = new FileStream("Primary.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < file.Count; i++)
            {
                
                String current = file[i].primaryKey + " " + file[i].recordNumber.ToString();
                sw.WriteLine(current);

            }

            sw.Close();
            fs.Close();
        }

        public static void loadFromFile()
        {
            if (File.Exists("Primary.txt"))
            {
                FileStream fs = new FileStream("Primary.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                while (sr.Peek() != -1)
                {
                    
                    string[] splitted = sr.ReadLine().Split(' ');
                    file.Add(new PrimaryIndex(splitted[0], int.Parse(splitted[1])));

                }

                sr.Close();
                fs.Close();
            }
        }
    }
}
