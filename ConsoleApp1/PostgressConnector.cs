using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp
{
    internal class PostgressConnector
    {
        public static string ConnectionString { get; set; } = "Host=localhost;Port=5432;Username=postgres;Password=Gucci111;Database=Student_db";

    }
}
