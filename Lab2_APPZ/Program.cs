using System;
using UniversityDisciplinesManager.Models;
using UniversityDisciplinesManager.Services;

namespace UniversityDisciplinesManager
{
    class Program
    {
        static UniversityManager manager = new UniversityManager();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Система управління навчальним процесом ===");
                Console.WriteLine("1. Додати викладача");
                Console.WriteLine("2. Додати студента");
                Console.WriteLine("3. Створити дисципліну");
                Console.WriteLine("4. Додати активність до дисципліни");
                Console.WriteLine("5. Зарахувати студента на дисципліну");
                Console.WriteLine("6. Призначити викладача на активність");
                Console.WriteLine("7. Позначити виконання активності студентом");
                Console.WriteLine("8. Перевірити допуск до підсумкового контролю");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть опцію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTeacher();
                        break;
                    case "2":
                        AddStudent();
                        break;
                    case "3":
                        CreateDiscipline();
                        break;
                    case "4":
                        AddActivityToDiscipline();
                        break;
                    case "5":
                        EnrollStudentInDiscipline();
                        break;
                    case "6":
                        AssignTeacherToActivity();
                        break;
                    case "7":
                        CompleteStudentActivity();
                        break;
                    case "8":
                        CheckFinalAssessment();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте знову.");
                        break;
                }
            }
        }

        static void AddTeacher()
        {
            Console.Write("Введіть ім'я викладача: ");
            string name = Console.ReadLine();
            var teacher = new Teacher { Name = name };
            manager.Teachers.Add(teacher);
            Console.WriteLine($"Викладач {name} доданий до системи.");
        }

        static void AddStudent()
        {
            Console.Write("Введіть ім'я студента: ");
            string name = Console.ReadLine();
            Console.Write("Введіть курс студента: ");
            int course;
            while (!int.TryParse(Console.ReadLine(), out course) || course < 1 || course > 4)
            {
                Console.Write("Введіть коректний курс (1-4): ");
            }
            var student = new Student { Name = name, Course = course };
            manager.Students.Add(student);
            Console.WriteLine($"Студент {name} доданий до системи.");
        }

        static void CreateDiscipline()
        {
            Console.Write("Введіть назву дисципліни: ");
            string name = Console.ReadLine();

            Console.Write("Введіть курс дисципліни (1-4): ");
            int course;
            while (!int.TryParse(Console.ReadLine(), out course) || course < 1 || course > 4)
            {
                Console.Write("Введіть коректний курс (1-4): ");
            }

            var discipline = new Discipline { Name = name, Course = course };
            manager.Disciplines.Add(discipline);
            Console.WriteLine($"Дисципліна {name} створена для {course} курсу.");
        }

        static void AddActivityToDiscipline()
        {
            Console.WriteLine("Оберіть дисципліну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }
            int disciplineIndex = int.Parse(Console.ReadLine()) - 1;
            var discipline = manager.Disciplines[disciplineIndex];

            Console.WriteLine("Оберіть тип активності:");
            Console.WriteLine("1. Лекція");
            Console.WriteLine("2. Практичне заняття");
            Console.WriteLine("3. Лабораторна робота");
            Console.WriteLine("4. Курсова робота");
            Console.WriteLine("5. Модульна контрольна робота");
            Console.WriteLine("6. Екзамен");
            Console.WriteLine("7. Залік");

            Activity activity = null;
            switch (Console.ReadLine())
            {
                case "1": activity = new Lecture(); break;
                case "2": activity = new PracticalClass(); break;
                case "3": activity = new LaboratoryWork(); break;
                case "4": activity = new CourseWork(); break;
                case "5": activity = new ModularControlWork(); break;
                case "6": activity = new Exam(); break;
                case "7": activity = new Credit(); break;
            }

            if (activity != null)
            {
                Console.Write("Введіть назву активності: ");
                activity.Name = Console.ReadLine();

                discipline.Activities.Add(activity);
                Console.WriteLine($"Активність {activity.Name} додана до дисципліни {discipline.Name}");
            }
        }

        static void EnrollStudentInDiscipline()
        {
            Console.WriteLine("Оберіть студента:");
            for (int i = 0; i < manager.Students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Students[i].Name}");
            }
            var student = manager.Students[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Оберіть дисципліну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }
            var discipline = manager.Disciplines[int.Parse(Console.ReadLine()) - 1];

            manager.EnrollStudentInDiscipline(student, discipline);
        }

        static void AssignTeacherToActivity()
        {
            Console.WriteLine("Оберіть викладача:");
            for (int i = 0; i < manager.Teachers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Teachers[i].Name}");
            }
            var teacher = manager.Teachers[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Оберіть дисципліну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }
            var discipline = manager.Disciplines[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Оберіть активність:");
            for (int i = 0; i < discipline.Activities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {discipline.Activities[i].Name} ({discipline.Activities[i].Type})");
            }
            var activity = discipline.Activities[int.Parse(Console.ReadLine()) - 1];

            manager.AssignTeacherToActivity(teacher, discipline, activity);
        }

        static void CompleteStudentActivity()
        {
            Console.WriteLine("Оберіть студента:");
            for (int i = 0; i < manager.Students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Students[i].Name}");
            }
            var student = manager.Students[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Оберіть дисципліну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }
            var discipline = manager.Disciplines[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Оберіть активність:");
            for (int i = 0; i < discipline.Activities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {discipline.Activities[i].Name} ({discipline.Activities[i].Type})");
            }
            var activity = discipline.Activities[int.Parse(Console.ReadLine()) - 1];

            manager.CompleteStudentActivity(student, discipline, activity);
        }

        static void CheckFinalAssessment()
        {
            Console.WriteLine("Оберіть студента:");
            for (int i = 0; i < manager.Students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Students[i].Name}");
            }
            var student = manager.Students[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Оберіть дисципліну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }
            var discipline = manager.Disciplines[int.Parse(Console.ReadLine()) - 1];

            bool canTakeFinalAssessment = manager.CanStudentTakeFinalAssessment(student, discipline);
            Console.WriteLine($"Студент {student.Name} " +
                (canTakeFinalAssessment ? "допущений" : "не допущений") +
                $" до підсумкового контролю з дисципліни {discipline.Name}");
        }
    }
}