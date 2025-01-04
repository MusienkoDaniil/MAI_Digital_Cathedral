using System;

public class Student : IEquatable<Student>
{
    private string firstName;
    private string lastName;
    private string patronymic;
    private string group;
    private string gradebookNumber;
    private int course;
    
    
    public Student(string firstName, string lastName, string patronymic, string group, string gradebookNumber, int course)
    {
        this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName), "Имя не может быть null.");
        this.lastName = lastName ?? throw new ArgumentNullException(nameof(lastName), "Фамилия не может быть null.");
        this.patronymic = patronymic ?? throw new ArgumentNullException(nameof(patronymic), "Отчество не может быть null.");
        this.group = group ?? throw new ArgumentNullException(nameof(group), "Группа не может быть null.");
        this.gradebookNumber = gradebookNumber ?? throw new ArgumentNullException(nameof(gradebookNumber), "Номер зачётной книжки не может быть null.");

        if (course < 1 || course > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(course), "Курс должен быть числом от 1 до 4.");
        }
        this.course = course;
    }

    
    public string FirstName => firstName;
    public string LastName => lastName;
    public string Patronymic => patronymic;
    public string Group => group;
    public string GradebookNumber => gradebookNumber;
    public int Course => course;

    
    public override string ToString()
    {
        return $"ФИО: {lastName} {firstName} {patronymic}, Группа: {group}, Номер зачётной книжки: {gradebookNumber}, Курс: {course}";
    }

    
    public override bool Equals(object obj)
    {
        return Equals(obj as Student);
    }

    public bool Equals(Student other)
    {
        if (other == null) return false;
        return firstName == other.firstName &&
               lastName == other.lastName &&
               patronymic == other.patronymic &&
               group == other.group &&
               gradebookNumber == other.gradebookNumber &&
               course == other.course;
    }

    
    public override int GetHashCode()
    {
        return HashCode.Combine(firstName, lastName, patronymic, group, gradebookNumber, course);
    }
}


public class Program
{
    public static void Main()
    {
        try
        {
            var student1 = new Student("Иван", "Иванов", "Иванович", "Группа-М14О-201БВ-23", "12345", 2);
            var student2 = new Student("Петр", "Петров", "Петрович", "Группа-М14О-102БВ-24", "67890", 1);
            var student3 = new Student("Иван", "Иванов", "Иванович", "Группа-М14О-201БВ-23", "12345", 2);

            Console.WriteLine("Информация о студентах:");
            Console.WriteLine(student1);
            Console.WriteLine(student2);

            Console.WriteLine("\nПроверка равенства:");
            Console.WriteLine($"student1 == student2: {student1.Equals(student2)}");
            Console.WriteLine($"student1 == student3: {student1.Equals(student3)}");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
