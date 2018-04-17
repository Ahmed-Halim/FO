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
                if (key == file[i].primaryKey)
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
                
                String KEY = file[i].primaryKey;
                int RN = file[i].recordNumber;

                char[] primaryKey;
                int primaryKeyLen = 6;

                char[] recordNumber;
                int valueLen = 4;

                char[] record;
                int recordLen = 10;

                primaryKey = new char[primaryKeyLen];
                recordNumber = new char[valueLen];
                record = new char[recordLen];

                KEY.CopyTo(0, primaryKey, 0, KEY.Length);
                RN.ToString().CopyTo(0, recordNumber, 0, RN.ToString().Length);

                primaryKey.CopyTo(record, 0);
                recordNumber.CopyTo(record, primaryKeyLen);

                sw.Write(record);

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
                    int primaryKeyLen = 6;
                    int recordNumberLen = 4;
                    int recordLen = 10;

                    char[] record = new char[recordLen];

                    sr.Read(record, 0, recordLen);

                    String key = new String(record).Substring(0, primaryKeyLen);
                    int rn = int.Parse(new String(record).Substring(primaryKeyLen, recordNumberLen));

                    file.Add(new PrimaryIndex(key, rn));
                }

                sr.Close();
                fs.Close();
            }
        }
    }
}
