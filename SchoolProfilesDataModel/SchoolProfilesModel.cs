using System;
using System.Collections.Generic;

namespace SchoolProfilesDataModel
{
    // This is the main class that acts as a user interface to the data model.
    // The real user interface will use this class.
    public class SchoolProfiles
    {
        private SchoolDatabaseContext db;

        public SchoolProfiles(SchoolDatabaseContext db) {
            this.db = db;
        }

        public List<NoteEntity> ViewNotes(ISchoolEntity agent, StudentEntity subject)
        {
            return agent.AccessNotes(agent, subject);
        }

        public void AddNotes(ISchoolEntity agent, StudentEntity subject, NoteEntity note)
        {
            if (agent is TeacherEntity)
            {
                db.Add(note);
                db.SaveChanges();
            }
            else
            {
                throw new SchoolAccessDeniedException(
                "Access Denied for " + agent.Id +
                " to add notes for student " + subject.Id);
            }
        }

        public void AddStudent(ISchoolEntity agent, StudentEntity student)
        {
            if (agent is AdministratorEntity)
            {
                db.Add(student);
                db.SaveChanges();
            }
            else
            {
                throw new SchoolAccessDeniedException(
                "Access Denied for " + agent.Id +
                " to add new student records, you must have Administrator privelages");
            }
        }
    }
}
