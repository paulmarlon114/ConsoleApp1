using Npgsql;
using StudentApp;
using System;
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
}
