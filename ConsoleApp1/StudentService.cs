using Npgsql;
using StudentApp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Numerics;

public class StudentService
{
    private List<Student> students = new List<Student>();

    public void AddStudent()
    {
        try
        {
            var s = new Student();

            Console.Write("Enter name: ");
            s.Name = Console.ReadLine();

            Console.Write("Math: ");
            s.Math = Convert.ToInt32(Console.ReadLine());

            Console.Write("English: ");
            s.English = Convert.ToInt32(Console.ReadLine());

            Console.Write("Science: ");
            s.Science = Convert.ToInt32(Console.ReadLine());

            students.Add(s);
            
            // Save to database
            using (var connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=Gucci111;Database=Student_db"))
            {
                connection.Open();
                var command = new NpgsqlCommand("INSERT INTO students (name, math, english, science) VALUES (@name, @math, @english, @science)", connection);
                command.Parameters.AddWithValue("@name", s.Name);
                command.Parameters.AddWithValue("@math", s.Math);
                command.Parameters.AddWithValue("@english", s.English);
                command.Parameters.AddWithValue("@science", s.Science);
                command.ExecuteNonQuery();
            }

            Console.WriteLine("✅ Student added successfully!");
        }
        catch
        {
            Console.WriteLine("❌ Invalid input. Please enter numbers correctly.");
        }
    }

    public void ViewResults()
    {
        var students = new List<Student>();
       
        Console.WriteLine("\n--- STUDENT RESULTS ---");
        using (var connection = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            connection.Open();
            var sql = "select * from students";
            var command = new NpgsqlCommand(sql, connection);
            var read = command.ExecuteReader();
            while (read.Read()) 
            {
                students.Add(new Student
                {
                    Id = read.GetInt32(0),
                    Name = read.GetString(1),
                    Math = read.GetInt32(2),
                    English = read.GetInt16(3),
                    Science = read.GetInt16(4)

                });
            }
            foreach (var s in students)
            {

                Console.WriteLine($"\nName: {s.Name}");
                Console.WriteLine($"Total: {s.GetTotal()}");
                Console.WriteLine($"Average: {s.GetAverage():F2}");
                Console.WriteLine($"Grade: {s.GetGrade()}");
                Console.WriteLine($"Status: {s.GetStatus()}");
            }

        }


    }

    public void DeleteStudent()
    {
        Console.WriteLine("Enter the ID of the Student you want to Delete");
        int id = int.Parse(Console.ReadLine());
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();
            string query = "DELETE FROM students where id = @id";
            var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }

    internal void UpdateStudentResult()
    {
        var s = new Student();

        Console.Write("Enter Id of the Student you want to update: ");
        s.Id = int.Parse(Console.ReadLine());

        Console.Write("Enter name: ");
        s.Name = Console.ReadLine();

        Console.Write("Math: ");
        s.Math = Convert.ToInt32(Console.ReadLine());

        Console.Write("English: ");
        s.English = Convert.ToInt32(Console.ReadLine());

        Console.Write("Science: ");
        s.Science = Convert.ToInt32(Console.ReadLine());

        students.Add(s);

        // Save to database
        using (var connection = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            connection.Open();
            string query = @"UPDATE students SET name = @name, math =@math, english = @english, science =@science where id = @id";
            var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", s.Id);
            cmd.Parameters.AddWithValue("@name", s.Name);
            cmd.Parameters.AddWithValue("@math", s.Math);
            cmd.Parameters.AddWithValue("@english", s.English);
            cmd.Parameters.AddWithValue("@science", s.Science);
            cmd.ExecuteNonQuery();
        }
    }
    public void SearchStudent()
    {
        Console.Write("Enter name to search: ");
        string name = Console.ReadLine();

        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT * FROM students WHERE name = @name";

            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"\nName: {reader["name"]}");
                        Console.WriteLine($"Math: {reader["math"]}");
                        Console.WriteLine($"English: {reader["english"]}");
                        Console.WriteLine($"Science: {reader["science"]}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
            }
        }
    }
    public void ShowTopStudent()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT * FROM students ORDER BY (math + english + science) DESC LIMIT 1";

            using (var cmd = new NpgsqlCommand(query, conn))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine($"\nTop Student: {reader["name"]}");
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
            }
        }
    }
    public void CountStudents()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT COUNT(*) FROM students";
            var cmd = new NpgsqlCommand(query, conn);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            Console.WriteLine($"Total Students: {count}");
        }
    }
    public void ShowPassedStudents()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT * FROM students WHERE (math + english + science)/3 >= 50";

            var cmd = new NpgsqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Name: {reader["name"]}");
            }
        }
    }
    public void ShowFailedStudents()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT * FROM students WHERE (math + english + science)/3 < 50";

            var cmd = new NpgsqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Name: {reader["name"]}");
            }
        }
    }
    public void ShowAllNames()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT name FROM students";
            var cmd = new NpgsqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- STUDENT NAMES ---");

            while (reader.Read())
            {
                Console.WriteLine(reader["name"]);
            }
        }
    }
    public void HighestAverage()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT *, (math + english + science)/3 AS avg FROM students ORDER BY avg DESC LIMIT 1";
            var cmd = new NpgsqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine($"Top Student: {reader["name"]} - Avg: {reader["avg"]}");
            }
        }
    }
    public void CountPassed()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT COUNT(*) FROM students WHERE (math + english + science)/3 >= 50";
            var cmd = new NpgsqlCommand(query, conn);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            Console.WriteLine($"Passed Students: {count}");
        }
    }
    public void CountFailed()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT COUNT(*) FROM students WHERE (math + english + science)/3 < 50";
            var cmd = new NpgsqlCommand(query, conn);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            Console.WriteLine($"Failed Students: {count}");
        }
    }
    public void SortByName()
    {
        using (var conn = new NpgsqlConnection(PostgressConnector.ConnectionString))
        {
            conn.Open();

            string query = "SELECT * FROM students ORDER BY name ASC";
            var cmd = new NpgsqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- SORTED STUDENTS ---");

            while (reader.Read())
            {
                Console.WriteLine(reader["name"]);
            }
        }
    }
    public void SortByScore()
    {
        var sorted = students.OrderByDescending(s => s.Score);

        foreach (var s in sorted)
        {
            Console.WriteLine($"{s.Name} - {s.Score}");
        }
    }
    public void ShowLowestStudent()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students available.");
            return;
        }

        var lowest = students.OrderBy(s => s.Score).First();
        Console.WriteLine($"Lowest: {lowest.Name} - {lowest.Score}");
    }
    public void CheckStudentExists()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        bool exists = students.Any(s => s.Name.ToLower() == name.ToLower());

        Console.WriteLine(exists ? "Student exists." : "Student does not exist.");
    }
    public void TotalScore()
    {
        int total = students.Sum(s => s.Score);
        Console.WriteLine($"Total Score of all students: {total}");
    }
    public void ClearAllStudents()
    {
        students.Clear();
        Console.WriteLine("All students removed.");
    }
    public void StudentsAboveAverage()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students available.");
            return;
        }

        double avg = students.Average(s => s.GetAverage());

        var result = students.Where(s => s.GetAverage() > avg);

        foreach (var s in result)
        {
            Console.WriteLine($"{s.Name} - {s.GetAverage():F2}");
        }
    }
    public void StudentsBelowAverage()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students available.");
            return;
        }

        double avg = students.Average(s => s.GetAverage());

        var result = students.Where(s => s.GetAverage() < avg);

        foreach (var s in result)
        {
            Console.WriteLine($"{s.Name} - {s.GetAverage():F2}");
        }
    }
}
