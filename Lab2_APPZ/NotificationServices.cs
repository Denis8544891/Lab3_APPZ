using System;
using UniversityDisciplinesManager.Services;
using UniversityDisciplinesManager.Events;

namespace UniversityDisciplinesManager.Services
{
    public class EmailNotificationService
    {
        public void Initialize(UniversityManager manager)
        {
            manager.StudentEnrolled += SendEnrollmentNotification;
            manager.ActivityCompleted += SendActivityCompletionNotification;
        }

        private void SendEnrollmentNotification(object sender, StudentEnrollmentEventArgs e)
        {
            Console.WriteLine($"Email sent: Student {e.Student.Name} enrolled in {e.Discipline.Name}");
        }

        private void SendActivityCompletionNotification(object sender, ActivityCompletionEventArgs e)
        {
            Console.WriteLine($"Email sent: Student {e.Student.Name} completed {e.Activity.Name} in {e.Discipline.Name}");
        }
    }

    public class LoggingService
    {
        public void Initialize(UniversityManager manager)
        {
            manager.StudentEnrolled += LogEnrollment;
            manager.ActivityCompleted += LogActivityCompletion;
        }

        private void LogEnrollment(object sender, StudentEnrollmentEventArgs e)
        {
            Console.WriteLine($"LOG: {e.Student.Name} enrolled in {e.Discipline.Name} at {DateTime.Now}");
        }

        private void LogActivityCompletion(object sender, ActivityCompletionEventArgs e)
        {
            Console.WriteLine($"LOG: {e.Student.Name} completed {e.Activity.Name} in {e.Discipline.Name} at {DateTime.Now}");
        }
    }
}