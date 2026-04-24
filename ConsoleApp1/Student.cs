using System;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Math { get; set; }
    public int English { get; set; }
    public int Science { get; set; }

    public int GetTotal() => Math + English + Science;

    public double GetAverage() => GetTotal() / 3.0;

    public string GetGrade()
    {
        double avg = GetAverage();

        if (avg >= 70) return "A";
        else if (avg >= 60) return "B";
        else if (avg >= 50) return "C";
        else return "F";
    }

    public string GetStatus() => GetAverage() >= 50 ? "Pass" : "Fail";
}

public class Student
{
    private object service;

    public string Name { get; set; }
    public int Score { get; set; }

    public string GetGrade()
    {
        if (Score >= 70) return "A";
        else if (Score >= 60) return "B";
        else if (Score >= 50) return "C";
        else return "F";
        else if (choice == 3)
            service.SearchStudent();
    }
}