using System.Collections.Generic;
using System.Diagnostics;

namespace UniversityDisciplinesManager.Models
{
    public class Teacher
    {
        public string Name { get; set; }
        public List<Discipline> TeachingDisciplines { get; set; } = new List<Discipline>();
        public List<Activity> AssignedActivities { get; set; } = new List<Activity>();

        public bool CanTeachActivity(Activity activity)
        {
            return !AssignedActivities.Contains(activity);
        }
    }
}