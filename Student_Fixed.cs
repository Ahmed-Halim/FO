using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Indexing
{
    class Student_Fixed
    {
        public static LinkedList<int> Available = new LinkedList<int>();

        private char[] isDeleted;
        public const int isDeletedLen = 1;

        private char[] studentID;
        public const int studentIDLen = 6;

        private char[] studentName;
        public const int studentNameLen = 20;

        private char[] studentFaculty;
        public const int studentFacultyLen = 20;

        public const int recordLen = 47;

        public Student_Fixed()
        {
            isDeleted = new char[isDeletedLen];
            studentID = new char[studentIDLen];
            studentName = new char[studentNameLen];
            studentFaculty = new char[studentFacultyLen];
        }

        public bool addStudent()
        {
            String input;

            isDeleted[0] = '0';

            Console.Write("Student ID: ");
            input = Console.ReadLine();

            if (input.Length > studentIDLen)
            {
                Console.WriteLine("Error - Length Mismatch");
                return false;
            }

            input.CopyTo(0, studentID, 0, input.Length);

            Console.Write("Student Name: ");
            input = Console.ReadLine();
            if (input.Length > studentNameLen)
            {
                Console.WriteLine("Error - Length Mismatch");
                return false;
            }
            input.CopyTo(0, studentName, 0, input.Length);

            Console.Write("Student Faculty: ");
            input = Console.ReadLine();
            if (input.Length > studentFacultyLen)
            {
                Console.WriteLine("Error - Length Mismatch");
                return false;
            }
            input.CopyTo(0, studentFaculty, 0, input.Length);

            char[] record = new char[recordLen];
            isDeleted.CopyTo(record, 0);
            studentID.CopyTo(record, isDeletedLen);
            studentName.CopyTo(record, isDeletedLen + studentIDLen);
            studentFaculty.CopyTo(record, isDeletedLen + studentIDLen + studentNameLen);

            FileStream fs;
            StreamWriter sw;

            int AvailableRecord = Available.First();

            if (AvailableRecord != -1)
            {
                fs = new FileStream("Fixed.txt", FileMode.Open);
                sw = new StreamWriter(fs);
                sw.BaseStream.Seek(AvailableRecord * recordLen, SeekOrigin.Begin);

                PrimaryIndex pi = new PrimaryIndex();
                pi.update(AvailableRecord, new String(studentID));
                SecondaryIndex.add(new String(studentID), new String(studentName));

                Available.RemoveFirst();
            }
            else
            {
                fs = new FileStream("Fixed.txt", FileMode.Append);
                sw = new StreamWriter(fs);

                PrimaryIndex PI = new PrimaryIndex();
                PI.add(new String(studentID));

                SecondaryIndex.add(new String(studentID), new String(studentName));
            }

            sw.Write(record);

            sw.Close();
            fs.Close();

            return true;
        }

        public void getStudentsByName()
        {
            Console.Write("Student Name: ");
            String input = Console.ReadLine();

            List<String> primaryKeys = SecondaryIndex.getStudentsByName(input);

            if (primaryKeys.Count > 0)
            {
                Console.WriteLine(primaryKeys.Count + " Student(s) Found!");

                for (int i = 0; i < primaryKeys.Count; i++)
                {
                    int recordNo = PrimaryIndex.getStudentByID(primaryKeys[i]);

                    if (recordNo != -1)
                    {

                        FileStream fs = new FileStream("Fixed.txt", FileMode.Open);
                        StreamReader sr = new StreamReader(fs);

                        sr.BaseStream.Seek(recordLen * recordNo, SeekOrigin.Begin);

                        char[] record = new char[recordLen];

                        sr.Read(record, 0, recordLen);

                        if (record[0] == '0')
                        {
                            String recordString = new String(record);
                            String id = recordString.Substring(isDeletedLen, studentIDLen);
                            String name = recordString.Substring(isDeletedLen + studentIDLen, studentNameLen);
                            String faculty = recordString.Substring(isDeletedLen + studentIDLen + studentNameLen, studentFacultyLen);

                            Console.Write("ID: " + id);
                            Console.Write("\tName: " + name);
                            Console.Write("\tFaculty: " + faculty + "\n");
                        }
                        sr.Close();
                        fs.Close();
                    }
                }
                
            }
            else
            {
                Console.WriteLine("No Students were found");
            }
        }

        public void searchByID()
        {
            String input;
            Console.Write("Search by ID: ");
            input = Console.ReadLine();
            int recordNo = PrimaryIndex.getStudentByID(input);

            if (recordNo != -1)
            {
                FileStream fs = new FileStream("Fixed.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(recordLen * recordNo, SeekOrigin.Begin);
                char[] record = new char[recordLen];
                sr.Read(record, 0, recordLen);

                if (record[0] == '0')
                {
                    String recordString = new String(record);
                    String id = recordString.Substring(isDeletedLen, studentIDLen);
                    String name = recordString.Substring(isDeletedLen + studentIDLen, studentNameLen);
                    String faculty = recordString.Substring(isDeletedLen + studentIDLen + studentNameLen, studentFacultyLen);

                    Console.WriteLine("Student Found!");
                    Console.Write("ID: " + id);
                    Console.Write("\tName: " + name);
                    Console.Write("\tFaculty: " + faculty + "\n");
                    sr.Close();
                    fs.Close();
                    return;
                }
                Console.WriteLine("ID: " + input + " was not found");
                sr.Close();
                fs.Close();
            }
            else
            {
                Console.WriteLine("Student Not Found");
            }
        }

        public void displayAll()
        {
            FileStream fs = new FileStream("Fixed.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                char[] record = new char[recordLen];

                sr.Read(record, 0, recordLen);

                if (record[0] == '0')
                {
                    String recordString = new String(record);

                    String id = recordString.Substring(isDeletedLen, studentIDLen);
                    String name = recordString.Substring(isDeletedLen + studentIDLen, studentNameLen);
                    String faculty = recordString.Substring(isDeletedLen + studentIDLen + studentNameLen, studentFacultyLen);

                    Console.Write("ID: " + id);
                    Console.Write("\tName: " + name);
                    Console.Write("\tFaculty: " + faculty + "\n");
                }
            }
            sr.Close();
            fs.Close();
        }

        public void deleteStudent()
        {
            String input;
            Console.Write("Delete Student ID: ");
            input = Console.ReadLine();
            int recordNumber = getStudentRecordNumber(input);

            if (recordNumber != -1)
            {
                FileStream fs = new FileStream("Fixed.txt", FileMode.Open);
                StreamWriter sw = new StreamWriter(fs);

                sw.BaseStream.Seek(recordLen * recordNumber, SeekOrigin.Begin);

                sw.Write('1');

                sw.Close();
                fs.Close();

                Console.WriteLine("Student Successfully deleted");
                Available.AddFirst(recordNumber);
            }
            else
            {
                Console.WriteLine("ID " + input + " was not found.");
            }
        }

        public int getStudentRecordNumber(String input)
        {
            FileStream fs = new FileStream("Fixed.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            int recordNumber = 0;

            while (sr.Peek() != -1)
            {
                char[] record = new char[recordLen];
                sr.Read(record, 0, recordLen);
                if (record[0] == '0')
                {
                    String recordString = new String(record);
                    String id = recordString.Substring(isDeletedLen, studentIDLen);
                    if (input == id.Trim('\0'))
                    {
                        sr.Close();
                        fs.Close();
                        return recordNumber;
                    }
                }
                recordNumber++;
            }

            sr.Close();
            fs.Close();

            return -1;
        }

        public void updateStudent()
        {
            String input;
            Console.Write("Update Student ID: ");
            input = Console.ReadLine();
            int recordNumber = getStudentRecordNumber(input);

            if (recordNumber != -1)
            {
                FileStream fs = new FileStream("Fixed.txt", FileMode.Open);
                StreamWriter sw = new StreamWriter(fs);

                sw.BaseStream.Seek(recordLen * recordNumber, SeekOrigin.Begin);

                isDeleted[0] = '0';
                input.CopyTo(0, studentID, 0, input.Length);

                Console.Write("Updated Name: ");
                input = Console.ReadLine();
                if (input.Length > studentNameLen)
                {
                    Console.WriteLine("Error - Length Mismatch");
                    return;
                }
                input.CopyTo(0, studentName, 0, input.Length);

                Console.Write("Updated Faculty: ");
                input = Console.ReadLine();
                if (input.Length > studentFacultyLen)
                {
                    Console.WriteLine("Error - Length Mismatch");
                    return;
                }
                input.CopyTo(0, studentFaculty, 0, input.Length);

                char[] record = new char[recordLen];
                isDeleted.CopyTo(record, 0);
                studentID.CopyTo(record, isDeletedLen);
                studentName.CopyTo(record, isDeletedLen + studentIDLen);
                studentFaculty.CopyTo(record, isDeletedLen + studentIDLen + studentNameLen);

                sw.Write(record);

                sw.Close();
                fs.Close();

                Console.WriteLine("Student updated");
            }
            else
            {
                Console.WriteLine("ID " + input + " is not found.");
            }
        }

        public static void saveToAvailable()
        {
            if (File.Exists("Available.txt"))
            {
                File.Delete("Available.txt");
            }

            FileStream fs = new FileStream("Available.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            foreach (int i in Available) {
                sw.WriteLine(i.ToString());
            }

            sw.Close();
            fs.Close();
        }

        public static void loadFromAvailable()
        {
            if (File.Exists("Available.txt"))
            {
                FileStream fs = new FileStream("Available.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                while (sr.Peek() != -1)
                {
                    Available.AddLast(int.Parse(sr.ReadLine()));
                }

                sr.Close();
                fs.Close();
            }
        }

    }
}
