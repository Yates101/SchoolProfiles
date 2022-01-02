using System;
using System.Collections.Generic;

namespace SchoolProfilesDataModel
{
    public class AdministratorEntity : ISchoolEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }

        public AdministratorEntity()
        {
            Id = Guid.NewGuid();
        }

        public List<NoteEntity> AccessNotes(ISchoolEntity agent, StudentEntity subject)
        {
            return subject.Notes;
        }
    }
}