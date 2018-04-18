using System;

namespace Indexing
{
    class Program
    {
        static void Main(string[] args)
        {
            PrimaryIndex.loadFromFile();
            SecondaryIndex.loadFromFile();
            Student_Fixed.loadFromAvailable();

            if (Student_Fixed.Available.Count == 0) 
                Student_Fixed.Available.AddFirst(-1);
            
            String Q = "y";
            do
            {
                Student_Fixed s = new Student_Fixed();
                Console.Clear();

                Console.WriteLine("1. Add");
                Console.WriteLine("2. Display");
                Console.WriteLine("3. Search by ID");
                Console.WriteLine("4. Search by Name");
                Console.WriteLine("5. Update");
                Console.WriteLine("6. Delete");

                String choice = Console.ReadLine();

                if (choice == "1")
                {
                    s.addStudent();
                }
                else if (choice == "2")
                {
                    s.displayAll();
                }
                else if (choice == "3")
                {
                    s.searchByID();
                }
                else if (choice == "4")
                {
                    s.getStudentsByName();
                }
                else if (choice == "5")
                {
                    s.updateStudent();
                }
                else if (choice == "6")
                {
                    s.deleteStudent();
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }

                Console.WriteLine("Do another operation ? (Y / N)");
                Q = Console.ReadLine();

            } while (Q == "y" || Q == "Y");

            PrimaryIndex.saveToFile();
            SecondaryIndex.saveToFile();
            Student_Fixed.saveToAvailable();
        }
    }
}
