using System;

class Program
{
    static void Main()
    {
        StudentService service = new StudentService();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== STUDENT SYSTEM ===");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Results");
            Console.WriteLine("3. Delete A Student Result");
            Console.WriteLine("4. Update A Student Result");
            Console.WriteLine("5. Exit");
            Console.Write("Select option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    service.AddStudent();
                    break;

                case "2":
                    service.ViewResults();
                    break;

                case "3":
                    service.DeleteStudent();
                    break;

                case "4":
                    service.UpdateStudentResult();
                    break;

                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }
}