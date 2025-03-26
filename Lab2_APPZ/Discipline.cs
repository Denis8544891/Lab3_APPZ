using System.Collections.Generic;

namespace UniversityDisciplinesManager.Models
{
    public class Discipline
    {
        public string Name { get; set; }
        public int Course { get; set; } 
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<Student> Students { get; set; } = new List<Student>();
        public Dictionary<Activity, Teacher> ActivityTeachers { get; set; } = new Dictionary<Activity, Teacher>(); 

        public bool CanAddTeacher(Teacher teacher)
        {
            return Teachers.Count < (Students.Count / 10 + 1);
        }
    }
}