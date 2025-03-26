using System.Collections.Generic;

namespace UniversityDisciplinesManager.Models
{
    public class Student
    {
        public string Name { get; set; }
        public int Course { get; set; }
        public List<Discipline> EnrolledDisciplines { get; set; } = new List<Discipline>();
        public Dictionary<Discipline, Dictionary<Activity, bool>> CompletedWorks { get; set; } = new Dictionary<Discipline, Dictionary<Activity, bool>>();

        public bool CanEnrollInDiscipline(Discipline discipline)
        {
            return Course == discipline.Course &&
                   !EnrolledDisciplines.Contains(discipline);
        }

        public void CompleteActivity(Discipline discipline, Activity activity)
        {
            if (!CompletedWorks.ContainsKey(discipline))
            {
                CompletedWorks[discipline] = new Dictionary<Activity, bool>();
            }
            CompletedWorks[discipline][activity] = true;
        }
    }
}