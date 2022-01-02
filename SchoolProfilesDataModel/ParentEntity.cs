using System;
using System.Collections.Generic;

namespace SchoolProfilesDataModel
{
    public class ParentEntity : ISchoolEntity
    {
        public string Name { get; set; }
        public Guid Id { get; private set; }
        public List<StudentEntity> AssociatedStudents { get; set; }

        public ParentEntity() : this(new List<StudentEntity>()) { }
        public ParentEntity(List<StudentEntity> associatedStudents) 
        {
            AssociatedStudents = associatedStudents;
            Id = Guid.NewGuid();
        }

        public List<NoteEntity> AccessNotes(ISchoolEntity agent, INoteOwnerEntity subject)
        {
            if (AssociatedAccounts.Exists(s => s.Id == subject.Id))
            {
                foreach (StudentEntity student in this.AssociatedStudents)
                {
                    if (student.Id == subject.Id)
                    {
                        return subject.Notes;
                    }
                }
            }
            throw new SchoolAccessDeniedException(
                "Access Denied for Parent " + agent.Id +
                " to view notes for student " + subject.Id);
        }
    }
}