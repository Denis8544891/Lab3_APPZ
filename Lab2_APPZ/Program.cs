using System;
using UniversityDisciplinesManager.Models;
using UniversityDisciplinesManager.Services;

namespace UniversityDisciplinesManager
{
    class Program
    {
        static UniversityManager manager = UniversityManager.Instance;

        static void Main(string[] args)
        {
            // Ініціалізація спостерігачів
            var emailService = new EmailNotificationService();
            var loggingService = new LoggingService();

            emailService.Initialize(manager);
            loggingService.Initialize(manager);

            while (true)
            {
                Console.WriteLine("\n=== Система управлiння навчальним процесом ===");
                Console.WriteLine("1. Додати викладача");
                Console.WriteLine("2. Додати студента");
                Console.WriteLine("3. Створити дисциплiну");
                Console.WriteLine("4. Додати активнiсть до дисциплiни");
                Console.WriteLine("5. Зарахувати студента на дисциплiну");
                Console.WriteLine("6. Призначити викладача на активнiсть");
                Console.WriteLine("7. Позначити виконання активностi студентом");
                Console.WriteLine("8. Перевiрити допуск до пiдсумкового контролю");
                Console.WriteLine("0. Вихiд");
                Console.Write("Оберiть опцiю: ");

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
                        Console.WriteLine("Невiрний вибiр. Спробуйте знову.");
                        break;
                }
            }
        }

        static void AddTeacher()
        {
            Console.Write("Введiть iм'я викладача: ");
            string name = Console.ReadLine();
            var teacher = new Teacher { Name = name };
            manager.Teachers.Add(teacher);
            Console.WriteLine($"Викладач {name} доданий до системи.");
        }

        static void AddStudent()
        {
            Console.Write("Введiть iм'я студента: ");
            string name = Console.ReadLine();
            Console.Write("Введiть курс студента: ");
            int course;
            while (!int.TryParse(Console.ReadLine(), out course) || course < 1 || course > 4)
            {
                Console.Write("Введiть коректний курс (1-4): ");
            }
            var student = new Student { Name = name, Course = course };
            manager.Students.Add(student);
            Console.WriteLine($"Студент {name} доданий до системи.");
        }

        static void CreateDiscipline()
        {
            Console.Write("Введiть назву дисциплiни: ");
            string name = Console.ReadLine();

            Console.Write("Введiть курс дисциплiни (1-4): ");
            int course;
            while (!int.TryParse(Console.ReadLine(), out course) || course < 1 || course > 4)
            {
                Console.Write("Введiть коректний курс (1-4): ");
            }

            var discipline = new Discipline { Name = name, Course = course };
            manager.Disciplines.Add(discipline);
            Console.WriteLine($"Дисциплiна {name} створена для {course} курсу.");
        }

        static void AddActivityToDiscipline()
        {
            if (manager.Disciplines.Count == 0)
            {
                Console.WriteLine("Спочатку створiть дисциплiну.");
                return;
            }

            Console.WriteLine("Оберiть дисциплiну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }

            int disciplineIndex;
            while (!int.TryParse(Console.ReadLine(), out disciplineIndex) ||
                   disciplineIndex < 1 || disciplineIndex > manager.Disciplines.Count)
            {
                Console.Write("Введiть коректний номер дисциплiни: ");
            }

            var discipline = manager.Disciplines[disciplineIndex - 1];

            Console.WriteLine("Оберiть тип активностi:");
            Console.WriteLine("1. Лекцiя");
            Console.WriteLine("2. Практичне заняття");
            Console.WriteLine("3. Лабораторна робота");
            Console.WriteLine("4. Курсова робота");
            Console.WriteLine("5. Модульна контрольна робота");
            Console.WriteLine("6. Екзамен");
            Console.WriteLine("7. Залiк");

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
                default:
                    Console.WriteLine("Невiрний вибiр.");
                    return;
            }

            Console.Write("Введiть назву активностi: ");
            activity.Name = Console.ReadLine();

            discipline.Activities.Add(activity);
            Console.WriteLine($"Активнiсть {activity.Name} додана до дисциплiни {discipline.Name}");
        }

        static void EnrollStudentInDiscipline()
        {
            if (manager.Students.Count == 0 || manager.Disciplines.Count == 0)
            {
                Console.WriteLine("Спочатку додайте студентiв та дисциплiни.");
                return;
            }

            Console.WriteLine("Оберiть студента:");
            for (int i = 0; i < manager.Students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Students[i].Name}");
            }

            int studentIndex;
            while (!int.TryParse(Console.ReadLine(), out studentIndex) ||
                   studentIndex < 1 || studentIndex > manager.Students.Count)
            {
                Console.Write("Введiть коректний номер студента: ");
            }
            var student = manager.Students[studentIndex - 1];

            Console.WriteLine("Оберiть дисциплiну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }

            int disciplineIndex;
            while (!int.TryParse(Console.ReadLine(), out disciplineIndex) ||
                   disciplineIndex < 1 || disciplineIndex > manager.Disciplines.Count)
            {
                Console.Write("Введiть коректний номер дисциплiни: ");
            }
            var discipline = manager.Disciplines[disciplineIndex - 1];

            manager.EnrollStudentInDiscipline(student, discipline);
        }

        static void AssignTeacherToActivity()
        {
            if (manager.Teachers.Count == 0 || manager.Disciplines.Count == 0)
            {
                Console.WriteLine("Спочатку додайте викладачiв та дисциплiни.");
                return;
            }

            Console.WriteLine("Оберiть викладача:");
            for (int i = 0; i < manager.Teachers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Teachers[i].Name}");
            }

            int teacherIndex;
            while (!int.TryParse(Console.ReadLine(), out teacherIndex) ||
                   teacherIndex < 1 || teacherIndex > manager.Teachers.Count)
            {
                Console.Write("Введiть коректний номер викладача: ");
            }
            var teacher = manager.Teachers[teacherIndex - 1];

            Console.WriteLine("Оберiть дисциплiну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }

            int disciplineIndex;
            while (!int.TryParse(Console.ReadLine(), out disciplineIndex) ||
                   disciplineIndex < 1 || disciplineIndex > manager.Disciplines.Count)
            {
                Console.Write("Введiть коректний номер дисциплiни: ");
            }
            var discipline = manager.Disciplines[disciplineIndex - 1];

            Console.WriteLine("Оберiть активнiсть:");
            for (int i = 0; i < discipline.Activities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {discipline.Activities[i].Name} ({discipline.Activities[i].Type})");
            }

            int activityIndex;
            while (!int.TryParse(Console.ReadLine(), out activityIndex) ||
                   activityIndex < 1 || activityIndex > discipline.Activities.Count)
            {
                Console.Write("Введiть коректний номер активностi: ");
            }
            var activity = discipline.Activities[activityIndex - 1];

            manager.AssignTeacherToActivity(teacher, discipline, activity);
        }

        static void CompleteStudentActivity()
        {
            if (manager.Students.Count == 0 || manager.Disciplines.Count == 0)
            {
                Console.WriteLine("Спочатку додайте студентiв та дисциплiни.");
                return;
            }

            Console.WriteLine("Оберiть студента:");
            for (int i = 0; i < manager.Students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Students[i].Name}");
            }

            int studentIndex;
            while (!int.TryParse(Console.ReadLine(), out studentIndex) ||
                   studentIndex < 1 || studentIndex > manager.Students.Count)
            {
                Console.Write("Введiть коректний номер студента: ");
            }
            var student = manager.Students[studentIndex - 1];

            Console.WriteLine("Оберiть дисциплiну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }

            int disciplineIndex;
            while (!int.TryParse(Console.ReadLine(), out disciplineIndex) ||
                   disciplineIndex < 1 || disciplineIndex > manager.Disciplines.Count)
            {
                Console.Write("Введiть коректний номер дисциплiни: ");
            }
            var discipline = manager.Disciplines[disciplineIndex - 1];

            Console.WriteLine("Оберiть активнiсть:");
            for (int i = 0; i < discipline.Activities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {discipline.Activities[i].Name} ({discipline.Activities[i].Type})");
            }

            int activityIndex;
            while (!int.TryParse(Console.ReadLine(), out activityIndex) ||
                   activityIndex < 1 || activityIndex > discipline.Activities.Count)
            {
                Console.Write("Введiть коректний номер активностi: ");
            }
            var activity = discipline.Activities[activityIndex - 1];

            manager.CompleteStudentActivity(student, discipline, activity);
        }

        static void CheckFinalAssessment()
        {
            if (manager.Students.Count == 0 || manager.Disciplines.Count == 0)
            {
                Console.WriteLine("Спочатку додайте студентiв та дисциплiни.");
                return;
            }

            Console.WriteLine("Оберiть студента:");
            for (int i = 0; i < manager.Students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Students[i].Name}");
            }

            int studentIndex;
            while (!int.TryParse(Console.ReadLine(), out studentIndex) ||
                   studentIndex < 1 || studentIndex > manager.Students.Count)
            {
                Console.Write("Введiть коректний номер студента: ");
            }
            var student = manager.Students[studentIndex - 1];

            Console.WriteLine("Оберiть дисциплiну:");
            for (int i = 0; i < manager.Disciplines.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manager.Disciplines[i].Name}");
            }

            int disciplineIndex;
            while (!int.TryParse(Console.ReadLine(), out disciplineIndex) ||
                   disciplineIndex < 1 || disciplineIndex > manager.Disciplines.Count)
            {
                Console.Write("Введiть коректний номер дисциплiни: ");
            }
            var discipline = manager.Disciplines[disciplineIndex - 1];

            bool canTakeFinalAssessment = manager.CanStudentTakeFinalAssessment(student, discipline);
            Console.WriteLine($"Студент {student.Name} " +
                (canTakeFinalAssessment ? "допущений" : "не допущений") +
                $" до пiдсумкового контролю з дисциплiни {discipline.Name}");
        }
    }
}