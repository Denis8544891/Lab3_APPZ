using System;
using UniversityDisciplinesManager.Events;
using UniversityDisciplinesManager.Models;

namespace UniversityDisciplinesManager.Interfaces
{
    public interface IUniversityEventGenerator
    {
        event EventHandler<StudentEnrollmentEventArgs> StudentEnrolled;
        event EventHandler<ActivityCompletionEventArgs> ActivityCompleted;
    }
}