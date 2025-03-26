using System;

namespace UniversityDisciplinesManager.Models
{
    public abstract class Activity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Teacher Instructor { get; set; }
    }

    public class Lecture : Activity
    {
        public Lecture() { Type = "Лекція"; }
        public int Duration { get; set; }
    }

    public class PracticalClass : Activity
    {
        public PracticalClass() { Type = "Практичне заняття"; }
        public int ParticipantsCount { get; set; }
    }

    public class LaboratoryWork : Activity
    {
        public LaboratoryWork() { Type = "Лабораторна робота"; }
        public int SubgroupSize { get; set; }
    }

    public class CourseWork : Activity
    {
        public CourseWork() { Type = "Курсова робота"; }
        public int ComplexityLevel { get; set; }
    }

    public class ModularControlWork : Activity
    {
        public ModularControlWork() { Type = "Модульна контрольна робота"; }
        public int MaxScore { get; set; }
    }

    public class Exam : Activity
    {
        public Exam() { Type = "Екзамен"; }
        public DateTime ExamDate { get; set; }
    }

    public class Credit : Activity
    {
        public Credit() { Type = "Залік"; }
        public bool IsAutomaticallyAssigned { get; set; }
    }
}