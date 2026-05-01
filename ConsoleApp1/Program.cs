// Improved student result system
using System;

class Program
{

    static void Main()
    {
        StudentService service = new StudentService();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Welcome to Student Result System!");
            Console.WriteLine("\n=== STUDENT SYSTEM ===");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Results");
            Console.WriteLine("3. Delete Student");
            Console.WriteLine("4. Update Student");
            Console.WriteLine("5. Search Student");
            Console.WriteLine("6. Exit");

            Console.WriteLine("7. Top Student");
            Console.WriteLine("8. Count Students");
            Console.WriteLine("9. Show Passed Students");
            Console.WriteLine("10. Show Failed Students");
            Console.WriteLine("11. Show All Names");
            Console.WriteLine("12. Highest Average");
            Console.WriteLine("13. Count Passed Students");
            Console.WriteLine("14. Count Failed Students");
            Console.WriteLine("15. Sort Students By Name");
            Console.WriteLine("16. Sort By Score");
            Console.WriteLine("17. Lowest Student");
            Console.WriteLine("18. Check Student Exists");
            Console.WriteLine("19. Total Score");
            Console.WriteLine("20. Clear All Students");
            Console.WriteLine("21. Students Above Average");
            Console.WriteLine("22. Students Below Average");
            Console.WriteLine("23. Filter By Grade");
            Console.WriteLine("24. Count By Grade");
            Console.WriteLine("25. First Student Added");

            Console.Write("Select option: ");
            var choice =  Console.ReadLine();
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
                    service.SearchStudent();
                    break;

                case "6":
                    running = false;
                    break;

                case "7":
                    service.ShowTopStudent();
                    break;

                case "8":
                    service.CountStudents();
                    break;

                case "9":
                    service.ShowPassedStudents();
                    break;

                case "10":
                    service.ShowFailedStudents();
                    break;

                case "11":
                    service.ShowAllNames();
                    break;

                case "12":
                    service.HighestAverage();
                    break;

                case "13":
                    service.CountPassed();
                    break;

                case "14":
                    service.CountFailed();
                    break;

                case "15":
                    service.SortByName();
                    break;

                case "16":
                    service.SortByScore();
                    break;

                case "17":
                    service.ShowLowestStudent();
                    break;

                case "18":
                    service.CheckStudentExists();
                    break;

                case "19":
                    service.TotalScore();
                    break;

                case "20":
                    service.ClearAllStudents();
                    break;

                case "21":
                    service.StudentsAboveAverage();
                    break;

                case "22":
                    service.StudentsBelowAverage();
                    break;

                case "23":
                    service.FilterByGrade();
                    break;

                case "24":
                    service.CountByGrade();
                    break;

                case "25":
                    service.ShowFirstStudent();
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }
}