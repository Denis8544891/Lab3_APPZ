using System;
using UniversityDisciplinesManager.Models;

namespace UniversityDisciplinesManager.Events
{
    public class StudentEnrollmentEventArgs : EventArgs
    {
        public Student Student { get; }
        public Discipline Discipline { get; }

        public StudentEnrollmentEventArgs(Student student, Discipline discipline)
        {
            Student = student;
            Discipline = discipline;
        }
    }

    public class ActivityCompletionEventArgs : EventArgs
    {
        public Student Student { get; }
        public Discipline Discipline { get; }
        public Activity Activity { get; }

        public ActivityCompletionEventArgs(Student student, Discipline discipline, Activity activity)
        {
            Student = student;
            Discipline = discipline;
            Activity = activity;
        }
    }
}