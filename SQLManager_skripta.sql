CREATE TABLE Student
(
	IDStudent int PRIMARY KEY IDENTITY,
	FirstName nvarchar(255),
	LastName nvarchar(255),
	Email nvarchar(255),
	YearOfStudy int,
	Picture varbinary(max)
)

GO

CREATE TABLE Teacher
(
	IDTeacher int PRIMARY KEY IDENTITY,
	FirstName nvarchar(255),
	LastName nvarchar(255),
	Email nvarchar(255),
	Picture varbinary(max)
)

GO

CREATE TABLE Subject
(
	IDSubject int PRIMARY KEY IDENTITY,
	Name nvarchar(255)
)

GO

CREATE TABLE StudentSubject
(
	IDStudentSubject int PRIMARY KEY IDENTITY,
	StudentID int FOREIGN KEY REFERENCES Student(IDStudent),
	SubjectID int FOREIGN KEY REFERENCES Subject(IDSubject)
)

GO

CREATE TABLE TeacherSubject
(
	IDTeacherSubject int PRIMARY KEY IDENTITY,
	TeacherID int FOREIGN KEY REFERENCES Teacher(IDTeacher),
	SubjectID int FOREIGN KEY REFERENCES Subject(IDSubject)
)

GO
CREATE TABLE TeacherStudent
(
	IDTeacherStudent int PRIMARY KEY IDENTITY,
	TeacherID int FOREIGN KEY REFERENCES Teacher(IDTeacher),
	StudentID int FOREIGN KEY REFERENCES Student(IDStudent)
)

GO
----------------------------------

CREATE PROCEDURE AddStudent
	@FirstName nvarchar(255),
	@LastName nvarchar(255),
	@Email nvarchar(255),
	@YearOfStudy int,
	@Picture varbinary(max),
	@IDStudent int output
AS
BEGIN
INSERT INTO Student (FirstName, LastName, Email, YearOfStudy, Picture) VALUES (@FirstName, @LastName, @Email, @YearOfStudy, @Picture)
SET @IDStudent = @@IDENTITY
END

GO

CREATE PROCEDURE DeleteStudent
	@IDStudent INT
AS
BEGIN
	IF EXISTS (SELECT * FROM StudentSubject WHERE StudentID = @IDStudent)
	BEGIN
		DELETE FROM StudentSubject
		WHERE StudentID = @IDStudent
	END
	IF EXISTS (SELECT * FROM TeacherStudent WHERE StudentID = @IDStudent)
	BEGIN
		DELETE FROM TeacherStudent
		WHERE StudentID = @IDStudent
	END
	DELETE FROM Student 
	WHERE IDStudent = @IDStudent
END
GO

CREATE PROCEDURE GetStudents
AS
SELECT * FROM Student

GO

CREATE PROCEDURE GetStudent
	@IDStudent INT
AS
SELECT * FROM Student 
WHERE IDStudent = @IDStudent

GO

CREATE PROCEDURE UpdateStudent
	@IDStudent INT,
	@FirstName nvarchar(255),
	@LastName nvarchar(255),
	@Email nvarchar(255),
	@YearOfStudy int,
	@Picture varbinary(max)
AS
UPDATE Student SET FirstName = @FirstName, LastName = @LastName, Email = @Email, YearOfStudy = @YearOfStudy, Picture = @Picture WHERE IDStudent = @IDStudent


GO
-----------------------


CREATE PROCEDURE AddTeacher
	@FirstName nvarchar(255),
	@LastName nvarchar(255),
	@Email nvarchar(255),
	@Picture varbinary(max),
	@IDTeacher int output
AS
	BEGIN
	INSERT INTO Teacher (FirstName, LastName, Email, Picture) VALUES (@FirstName, @LastName, @Email, @Picture)
	SET @IDTeacher = @@IDENTITY
	END

GO

CREATE PROCEDURE DeleteTeacher
	@IDTeacher INT
AS
BEGIN
	IF EXISTS (SELECT * FROM TeacherSubject WHERE TeacherID = @IDTeacher)
	BEGIN
		DELETE FROM TeacherSubject
		WHERE TeacherID = @IDTeacher
	END
	IF EXISTS (SELECT * FROM TeacherStudent WHERE TeacherID = @IDTeacher)
	BEGIN
		DELETE FROM TeacherStudent
		WHERE TeacherID = @IDTeacher
	END
	DELETE FROM Teacher 
	WHERE IDTeacher = @IDTeacher
END

GO

CREATE PROCEDURE GetTeachers
AS
SELECT * FROM Teacher

GO

CREATE PROCEDURE GetTeacher
	@IDTeacher INT
AS
SELECT * FROM Teacher 
WHERE IDTeacher = @IDTeacher

Go

CREATE PROCEDURE UpdateTeacher
	@IDTeacher INT,
	@FirstName nvarchar(255),
	@LastName nvarchar(255),
	@Email nvarchar(255),
	@Picture varbinary(max)
AS
UPDATE Teacher SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Picture = @Picture WHERE IDTeacher = @IDTeacher

GO

--------------------------------------------

CREATE PROCEDURE AddSubject
	@Name nvarchar(255),
	@IDSubject int output
AS
	BEGIN
	INSERT INTO Subject (Name) VALUES (@Name)
	SET @IDSubject = @@IDENTITY
	END

GO

CREATE PROCEDURE DeleteSubject
	@IDSubject INT
AS
BEGIN
	IF EXISTS (SELECT * FROM TeacherSubject WHERE SubjectID = @IDSubject)
	BEGIN
		DELETE FROM TeacherSubject
		WHERE SubjectID = @IDSubject
	END
	IF EXISTS (SELECT * FROM StudentSubject WHERE SubjectID = @IDSubject)
	BEGIN
		DELETE FROM StudentSubject
		WHERE SubjectID = @IDSubject
	END
	DELETE FROM Subject 
	WHERE IDSubject = @IDSubject
END

GO


CREATE PROCEDURE GetSubjects
AS
SELECT * FROM Subject

GO

CREATE PROCEDURE GetSubject
	@IDSubject INT
AS
SELECT * FROM Subject 
WHERE IDSubject = @IDSubject

Go

CREATE PROCEDURE UpdateSubject
	@IDSubject INT,
	@Name nvarchar(255)
AS
UPDATE Subject SET Name = @Name WHERE IDSubject = @IDSubject


GO
-----------------------------------------------

CREATE PROCEDURE AddTeacherStudent
	@TeacherID int,
	@StudentID int,
	@IDTeacherStudent int output
AS
	BEGIN
	INSERT INTO TeacherStudent (TeacherID, StudentID) VALUES (@TeacherID, @StudentID)
	SET @IDTeacherStudent = @@IDENTITY
	END

GO

CREATE PROCEDURE DeleteTeacherStudent
	@IDTeacherStudent INT
AS
DELETE FROM TeacherStudent 
WHERE IDTeacherStudent = @IDTeacherStudent

GO


CREATE PROCEDURE GetTeachersStudents
AS
SELECT ts.IDTeacherStudent, ts.TeacherID, ts.StudentID, t.FirstName +' '+ t.LastName as Teacher, s.FirstName +' '+ s.LastName as Student
FROM TeacherStudent as ts
INNER JOIN Teacher as t on ts.TeacherID = t.IDTeacher
INNER JOIN Student as s on ts.StudentID = s.IDStudent

GO

CREATE PROCEDURE GetTeacherStudent
	@IDTeacherStudent INT
AS
SELECT * FROM TeacherStudent 
WHERE IDTeacherStudent = @IDTeacherStudent

Go

CREATE PROCEDURE UpdateTeacherStudent
	@IDTeacherStudent INT,
	@TeacherID int,
	@StudentID int
AS
UPDATE TeacherStudent SET TeacherID = @TeacherID, StudentID = @StudentID WHERE IDTeacherStudent = @IDTeacherStudent


Go

-------------------------------------------------

CREATE PROCEDURE AddStudentSubject
	@StudentID int,
	@SubjectID int,
	@IDStudentSubject int output
AS
	BEGIN
	INSERT INTO StudentSubject (StudentID, SubjectID) VALUES (@StudentID, @SubjectID)
	SET @IDStudentSubject = @@IDENTITY
	END

GO

CREATE PROCEDURE DeleteStudentSubject
	@IDStudentSubject INT
AS
DELETE FROM StudentSubject
WHERE IDStudentSubject = @IDStudentSubject

GO


CREATE PROCEDURE GetStudentsSubjects
AS
SELECT ss.IDStudentSubject, ss.StudentID, ss.SubjectID, st.FirstName +' '+ st.LastName as Student, su.Name as Subject
FROM StudentSubject as ss
INNER JOIN Student as st on ss.StudentID = st.IDStudent
INNER JOIN Subject as su on ss.SubjectID = su.IDSubject

GO

CREATE PROCEDURE GetStudentSubject
	@IDStudentSubject INT
AS
SELECT * FROM StudentSubject
WHERE IDStudentSubject = @IDStudentSubject

Go

CREATE PROCEDURE UpdateStudentSubject
	@IDStudentSubject int,
	@StudentID int,
	@SubjectID int
AS
UPDATE StudentSubject SET StudentID = @StudentID, SubjectID = @SubjectID WHERE IDStudentSubject = @IDStudentSubject


Go

--------------------------------------

CREATE PROCEDURE AddTeacherSubject
	@TeacherID int,
	@SubjectID int,
	@IDTeacherSubject int output
AS
	BEGIN
	INSERT INTO TeacherSubject (TeacherID, SubjectID) VALUES (@TeacherID, @SubjectID)
	SET @IDTeacherSubject = @@IDENTITY
	END

GO

CREATE PROCEDURE DeleteTeacherSubject
	@IDTeacherSubject INT
AS
DELETE FROM TeacherSubject
WHERE IDTeacherSubject = @IDTeacherSubject

GO


CREATE PROCEDURE GetTeachersSubjects
AS
SELECT ts.IDTeacherSubject, ts.TeacherID, ts.SubjectID, t.FirstName +' '+ t.LastName as Teacher, s.Name as Subject
FROM TeacherSubject as ts
INNER JOIN Teacher as t on ts.TeacherID = t.IDTeacher
INNER JOIN Subject as s on ts.SubjectID = s.IDSubject

GO

CREATE PROCEDURE GetTeacherSubject
	@IDTeacherSubject INT
AS
SELECT * FROM TeacherSubject
WHERE IDTeacherSubject = @IDTeacherSubject

Go

ALTER PROCEDURE UpdateTeacherSubject
	@IDTeacherSubject int,
	@TeacherID int,
	@SubjectID int
AS
UPDATE TeacherSubject SET TeacherID = @TeacherID, SubjectID = @SubjectID WHERE IDTeacherSubject = @IDTeacherSubject

