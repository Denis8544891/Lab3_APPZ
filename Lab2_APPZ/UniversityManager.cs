using System;
using System.Collections.Generic;
using UniversityDisciplinesManager.Models;
using UniversityDisciplinesManager.Interfaces;
using UniversityDisciplinesManager.Events;

namespace UniversityDisciplinesManager.Services
{
    public class UniversityManager : IUniversityEventGenerator
    {
        private static UniversityManager _instance;
        public static UniversityManager Instance => _instance ??= new UniversityManager();

        public event EventHandler<StudentEnrollmentEventArgs> StudentEnrolled;
        public event EventHandler<ActivityCompletionEventArgs> ActivityCompleted;

        public List<Discipline> Disciplines { get; } = new();
        public List<Student> Students { get; } = new();
        public List<Teacher> Teachers { get; } = new();

        private UniversityManager() { }

        protected virtual void OnStudentEnrolled(Student student, Discipline discipline)
        {
            StudentEnrolled?.Invoke(this, new StudentEnrollmentEventArgs(student, discipline));
        }

        protected virtual void OnActivityCompleted(Student student, Discipline discipline, Activity activity)
        {
            ActivityCompleted?.Invoke(this, new ActivityCompletionEventArgs(student, discipline, activity));
        }

        public void EnrollStudentInDiscipline(Student student, Discipline discipline)
        {
            if (student == null || discipline == null)
            {
                Console.WriteLine("Помилка: студент або дисципліна не можуть бути null.");
                return;
            }

            if (student.CanEnrollInDiscipline(discipline))
            {
                student.EnrolledDisciplines.Add(discipline);
                discipline.Students.Add(student);
                Console.WriteLine($"{student.Name} зарахований на дисциплiну {discipline.Name}");
                OnStudentEnrolled(student, discipline);
            }
            else
            {
                Console.WriteLine($"Неможливо зарахувати {student.Name} на дисциплiну {discipline.Name}");
            }
        }

        public void AssignTeacherToActivity(Teacher teacher, Discipline discipline, Activity activity)
        {
            if (teacher == null || discipline == null || activity == null)
            {
                Console.WriteLine("Помилка: викладач, дисципліна або активність не можуть бути null.");
                return;
            }

            if (teacher.CanTeachActivity(activity))
            {
                if (!discipline.ActivityTeachers.ContainsKey(activity))
                {
                    discipline.ActivityTeachers[activity] = teacher;
                    teacher.AssignedActivities.Add(activity);
                    Console.WriteLine($"{teacher.Name} призначений викладачем активностi {activity.Name} дисциплiни {discipline.Name}");
                }
                else
                {
                    Console.WriteLine($"Помилка: активність {activity.Name} вже має викладача.");
                }
            }
            else
            {
                Console.WriteLine($"Неможливо призначити {teacher.Name} викладачем активностi {activity.Name}");
            }
        }

        public void CompleteStudentActivity(Student student, Discipline discipline, Activity activity)
        {
            if (student == null || discipline == null || activity == null)
            {
                Console.WriteLine("Помилка: студент, дисципліна або активність не можуть бути null.");
                return;
            }

            if (!student.EnrolledDisciplines.Contains(discipline))
            {
                Console.WriteLine($"{student.Name} не зарахований на дисциплiну {discipline.Name}");
                return;
            }

            student.CompleteActivity(discipline, activity);
            Console.WriteLine($"{student.Name} успiшно виконав активнiсть {activity.Name} з {discipline.Name}");
            OnActivityCompleted(student, discipline, activity);
        }

        public bool CanStudentTakeFinalAssessment(Student student, Discipline discipline)
        {
            if (student == null || discipline == null)
            {
                Console.WriteLine("Помилка: студент або дисципліна не можуть бути null.");
                return false;
            }

            if (!student.CompletedWorks.ContainsKey(discipline))
                return false;

            foreach (var activity in discipline.Activities)
            {
                if (!student.CompletedWorks[discipline].ContainsKey(activity) ||
                    !student.CompletedWorks[discipline][activity])
                {
                    return false;
                }
            }
            return true;
        }
    }
}