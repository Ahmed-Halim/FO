using System;
using System.Collections.Generic;
using System.IO;

namespace Indexing
{
    class PrimaryIndex {

        public static List<PrimaryIndex> Pfile = new List<PrimaryIndex>();
        
        private String primaryKey;
        private int recordNumber;

        public PrimaryIndex() { }

        public PrimaryIndex(String primaryKey, int recordNumber)
        {
            this.primaryKey = primaryKey;
            this.recordNumber = recordNumber;
        }

        public static void add(String primaryKey)
        {
            int recordNumber = Pfile.Count;
            Pfile.Add(new PrimaryIndex(primaryKey, recordNumber));
        }

        public static void update(int recordNumber, String newKey)
        {
            for (int i = 0; i < Pfile.Count; i++)
            {
                if (Pfile[i].recordNumber == recordNumber)
                {
                    Pfile[i].primaryKey = newKey;
                    break;
                }
            }
        }

        public static int getStudentByID(String key)
        {
            for (int i = 0; i < Pfile.Count; i++)
            {
                if (key == Pfile[i].primaryKey)
                {
                    return Pfile[i].recordNumber;
                }
            }
            return -1;
        }

        public static void saveToFile()
        {
            
            File.Delete("Primary.txt");

            FileStream fs = new FileStream("Primary.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < Pfile.Count; i++)
            {
                
                String current = Pfile[i].primaryKey + " " + Pfile[i].recordNumber.ToString();
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
                    Pfile.Add(new PrimaryIndex(splitted[0], int.Parse(splitted[1])));

                }

                sr.Close();
                fs.Close();
            }
        }
    }
}
