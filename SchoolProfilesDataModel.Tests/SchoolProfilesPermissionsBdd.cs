using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolProfilesDataModel.Tests
{
    // These are BDD-style tests for test-driving the data model.
    // You can test-drive by adding a test for a new scenario and then implementing it.
    [TestClass]
    public class SchoolProfilesPermissionsBdd
    {
        [TestMethod]
        public void StudentsCanViewOwnNotes()
        {
            using (var db = SchoolDatabaseContext.TestContext())
            {
                // Arrange - Set up the database
                var agent = new StudentEntity { Name = "Jesse" };
                var note = new NoteEntity { Text = "This is a note", Student = agent };
                db.Add(agent);
                db.Add(note);
                db.SaveChanges();

                // Act - Run the request using the model
                var model = new SchoolProfiles(db);
                List<NoteEntity> notes = model.ViewNotes(agent: agent, subject: agent);

                // Assert - Check we get the right thing back
                Assert.AreEqual(1, notes.Count);
                Assert.AreEqual(note.Text, notes[0].Text);
            }
        }

        [TestMethod]
        public void StudentsCannotViewOtherStudentsNotes()
        {
            using (var db = SchoolDatabaseContext.TestContext())
            {
                var agent = new StudentEntity { Name = "Jesse" };
                var student = new StudentEntity { Name = "Jane" };
                var note = new NoteEntity { Text = "This is a note", Student = student };
                db.Add(agent);
                db.Add(student);
                db.Add(note);
                db.SaveChanges();

                var model = new SchoolProfiles(db);

                Assert.ThrowsException<SchoolAccessDeniedException>(
                    () => model.ViewNotes(agent: agent, subject: student));
            }
        }

        [TestMethod]
        public void ParentsCanViewNotesOfAssociatedStudents()
        {
            using (var db = SchoolDatabaseContext.TestContext())
            {
                var student = new StudentEntity { Name = "Jane" };
                var studentList = new List<StudentEntity>();
                studentList.Add(student);
                var agent = new ParentEntity { Name = "Deb", AssociatedStudents = studentList };
                var note = new NoteEntity { Text = "This is a note", Student = student };
                db.Add(agent);
                db.Add(student);
                db.Add(note);
                db.SaveChanges();

                var model = new SchoolProfiles(db);
                List<NoteEntity> notes = model.ViewNotes(agent: agent, subject: student);

                Assert.AreEqual(1, notes.Count);
                Assert.AreEqual(note.Text, notes[0].Text);
            }
        }

        [TestMethod]
        public void ParentsCannotViewNotesOfUnassociatedStudents()
        {
            using (var db = SchoolDatabaseContext.TestContext())
            {
                var student = new StudentEntity { Name = "Jane" };
                var student2 = new StudentEntity { Name = "John" };
                var studentList = new List<StudentEntity>();
                studentList.Add(student2);
                var agent = new ParentEntity { Name = "Deb", AssociatedStudents = studentList};
                var note = new NoteEntity { Text = "This is a note", Student = student };
                db.Add(agent);
                db.Add(student);
                db.Add(note);
                db.SaveChanges();

                var model = new SchoolProfiles(db);

                Assert.ThrowsException<SchoolAccessDeniedException>(
                    () => model.ViewNotes(agent: agent, subject: student));
            }
        }

        [TestMethod]
        public void TeachersCanViewNotesOnStudentRecords()
        {
            using (var db = SchoolDatabaseContext.TestContext())
            {
                var agent = new TeacherEntity { Name = "Mr Smith" };
                var student = new StudentEntity { Name = "Jane" };
                var note = new NoteEntity { Text = "This is a note", Student = student };
                db.Add(agent);
                db.Add(student);
                db.Add(note);
                db.SaveChanges();

                var model = new SchoolProfiles(db);
                List<NoteEntity> notes = model.ViewNotes(agent: agent, subject: student);

                Assert.AreEqual(1, notes.Count);
                Assert.AreEqual(note.Text, notes[0].Text);
            }
        }

        [TestMethod]
        public void TeachersCanAddNotesToStudentRecords()
        {
            using (var db = SchoolDatabaseContext.TestContext())
            {
                var agent = new TeacherEntity { Name = "Mr Smith" };
                var student = new StudentEntity { Name = "Jane" };
                var note = new NoteEntity { Text = "This is a note", Student = student };
                db.Add(agent);
                db.Add(student);
                db.SaveChanges();

                var model = new SchoolProfiles(db);
                model.AddNotes(agent: agent, subject: student, note: note);
                List<NoteEntity> notes = model.ViewNotes(agent: agent, subject: student);

                Assert.AreEqual(1, notes.Count);
                Assert.AreEqual(note.Text, notes[0].Text);
            }
        }

        [TestMethod]
        public void AdministratorsCanCreateStudents()
        {
            using (var db = SchoolDatabaseContext.TestContext())
            {
                var admin = new AdministratorEntity();
                var student = new StudentEntity { Name = "Jane" };
                db.Add(admin);
                db.SaveChanges();


                var model = new SchoolProfiles(db);
                model.AddStudent(agent: admin, student: student);
                var studentRecord = db.Students.Find(student.Id);

                Assert.AreEqual(student, studentRecord);
            }
        }

        [TestMethod]
        public void AdministratorsCanUpdateStudents()
        {

        }
    }
}
