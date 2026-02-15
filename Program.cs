using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradeManagementSystem
{
    // ENUM for grade classification
    enum GradeLevel
    {
        Fail,
        Pass,
        Average,
        Good,
        Excellent
    }

    // STRUCT to represent a student
    struct Student
    {
        public string Name;
        public int Grade;
        public GradeLevel Level;

        public Student(string name, int grade)
        {
            Name = name;
            Grade = grade;
            Level = CalculateGradeLevel(grade);
        }

        // Determines grade category
        private static GradeLevel CalculateGradeLevel(int grade)
        {
            if (grade >= 85) return GradeLevel.Excellent;
            if (grade >= 75) return GradeLevel.Good;
            if (grade >= 65) return GradeLevel.Average;
            if (grade >= 50) return GradeLevel.Pass;
            return GradeLevel.Fail;
        }

        public override string ToString()
        {
            return $"{Name} - {Grade} ({Level})";
        }
    }

    class Program
    {
        // Using a Dictionary to store students for efficient lookup
        static Dictionary<string, Student> 
        students = new Dictionary<string, Student>();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                // Display menu and get user choice
                DisplayMenu();
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        DisplayAllStudents();
                        break;
                    case "3":
                        SearchStudent();
                        break;
                    case "4":
                        UpdateStudentGrade();
                        break;
                    case "5":
                        DeleteStudent();
                        break;
                    case "6":
                        CalculateAverage();
                        break;
                    case "7":
                        FindHighestAndLowest();
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("Exiting program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n===== Student Grade Management System =====");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Display All Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Update Student Grade");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Calculate Average Grade");
            Console.WriteLine("7. Find Highest and Lowest Grades");
            Console.WriteLine("8. Exit");
            Console.Write("Select an option: ");
        }

        static void AddStudent()
        {
            // Get student name and grade from user input
            Console.Write("Enter student name: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            if (students.ContainsKey(name))
            {
                Console.WriteLine("Student already exists.");
                return;
            }

            int grade = GetValidGrade();
            Student newStudent = new Student(name, grade);

            students.Add(name, newStudent);
            Console.WriteLine("Student added successfully.");
        }

        static void DisplayAllStudents()
        {
            // Check if there are any students to display
            if (students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }
            // Display all students in the system
            Console.WriteLine("\n--- Student Records ---");
            foreach (var student in students.Values)
            {
                Console.WriteLine(student);
            }
        }

        static void SearchStudent()
        {
            // Get student name to search from user input
            Console.Write("Enter student name to search: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (students.TryGetValue(name, out Student student))
            {
                Console.WriteLine($"Student Found -> {student}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        static void UpdateStudentGrade()
        {
            // Get student name to update from user input
            Console.Write("Enter student name to update: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (!students.ContainsKey(name))
            {
                Console.WriteLine("Student does not exist.");
                return;
            }

            int newGrade = GetValidGrade();
            students[name] = new Student(name, newGrade);

            Console.WriteLine("Grade updated successfully.");
        }

        static void DeleteStudent()
        {
            // Get student name to delete from user input
            Console.Write("Enter student name to delete: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (students.Remove(name))
            {
                Console.WriteLine("Student deleted successfully.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        static void CalculateAverage()
        {
            // Check if there are any students to calculate average grade
            if (students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }
            // Calculate and display the average grade of all students
            double average = students.Values.Average(s => s.Grade);
            Console.WriteLine($"Average Grade: {average:F2}");
        }

        static void FindHighestAndLowest()
        {
            // Check if there are any students to find highest and lowest grades
            if (students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }
            // Find and display the highest and lowest grades among all students
            int highest = students.Values.Max(s => s.Grade);
            int lowest = students.Values.Min(s => s.Grade);

            Console.WriteLine($"Highest Grade: {highest}");
            Console.WriteLine($"Lowest Grade: {lowest}");
        }

        static int GetValidGrade()
        {
            // Loop until a valid grade is entered by the user
            while (true)
            {
                Console.Write("Enter grade (0 - 100): ");
                if (int.TryParse(Console.ReadLine(), out int grade) && grade >= 0 && grade <= 100)
                {
                    return grade;
                }
                Console.WriteLine("Invalid grade. Please enter a number between 0 and 100.");
            }
        }
    }
}
