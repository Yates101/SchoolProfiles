using System;
using System.Collections.Generic;

namespace SchoolProfilesDataModel
{
    public interface ISchoolEntity
    {
        Guid Id { get; }
        string Name { get; set; }

        List<NoteEntity> AccessNotes(ISchoolEntity agent, StudentEntity subject);
    }
}