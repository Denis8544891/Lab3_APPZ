using System;
using System.Collections.Generic;
using UniversityDisciplinesManager.Models;

namespace UniversityDisciplinesManager.Services
{
    public class UniversityManager
    {
        public List<Discipline> Disciplines { get; set; } = new List<Discipline>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();

        public void EnrollStudentInDiscipline(Student student, Discipline discipline)
        {
            if (student.CanEnrollInDiscipline(discipline))
            {
                student.EnrolledDisciplines.Add(discipline);
                discipline.Students.Add(student);
                Console.WriteLine($"{student.Name} зарахований на дисципліну {discipline.Name}");
            }
            else
            {
                Console.WriteLine($"Неможливо зарахувати {student.Name} на дисципліну {discipline.Name}");
            }
        }

        public void AssignTeacherToActivity(Teacher teacher, Discipline discipline, Activity activity)
        {
            if (teacher.CanTeachActivity(activity))
            {
                discipline.ActivityTeachers[activity] = teacher;
                teacher.AssignedActivities.Add(activity);
                Console.WriteLine($"{teacher.Name} призначений викладачем активності {activity.Name} дисципліни {discipline.Name}");
            }
            else
            {
                Console.WriteLine($"Неможливо призначити {teacher.Name} викладачем активності {activity.Name}");
            }
        }

        public void CompleteStudentActivity(Student student, Discipline discipline, Activity activity)
        {
            student.CompleteActivity(discipline, activity);
            Console.WriteLine($"{student.Name} успішно виконав активність {activity.Name} з {discipline.Name}");
        }

        public bool CanStudentTakeFinalAssessment(Student student, Discipline discipline)
        {
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