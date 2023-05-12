# # Online Examination System

E-Exam is a System for students to get their exams online. The admin is responsible for adding and editing subjects and professors and make an approval for the professors. Professors prepare for their subjects' exams with adding and editing questions and identifying the correct answer and the level of difficulty for each question. Students take the exam with different random questions and the results are stored to be shown for the student and professor.


# Types of Users

1. Admin: is basically a professor with Administrating Authentications. 
2. Professor: is responsible for The Structure of the Exam and its content.
 3. Student: takes the Exam and then directly notified with his own result.

## Admin

1. Adding and Editing the Levels and Departments of the Faculty.
 2. Adding and Editing the Subjects of each level and department.
 3. View The List of The Professors, and Approve the Sign up Requests.
 4. Specifies the subjects for each professor. 
 5. All Privileges of the Professor

## Professor

1. Adding and Editing Chapters for each Subject that he teaches and detect the number of questions of each Chapter 
2. Organizing the structure of the exam.
 3. Determine the allowed time for finishing the exam.
 4. Adding and Editing Questions and Identify The Correct answer, and those Questions should be published for the students as a Questions Bank 
 5. Show The Results of the Students for his Subjects.

## Student

1. Sign Up and Select his Level and Department.
 2. Sign In and Select the Subject to start his Exam
 3. Finish The Exam, Submit it and show the Result and the Previous Exams' Results.

# The Structure of the Exam

1. The types of Question are MCQ and True & False. 
2. The Professor specifies how many questions that the Exam must have. 
3. The Professor specifies how many questions included from each chapter that the Exam must have.
 4. The Questions are partitioned into Three Categories, A, B & C depending on the Difficulty Level 
 5. The Professor Specifies number of Questions of each Category from each Chapter of each type
  Ex:  3 Questions MCQ of Category A from Chapter 1, 
         4 Questions MCQ of Category B from Chapter 1, 
         4 Questions MCQ of Category C from Chapter 1,
         5 Questions T&F of Category A from Chapter 1, 
         3 Questions T&F of Category B from Chapter 1,
         2 Questions T&F of Category C from Chapter 1. And so on for each Chapter.
 6. Each student gets random Questions According to the previous constraints that Professor set. 
 7. The MCQ Choices should be arranged randomly, so that, the order of answer will differ from student to another.
       Ex: Consider a Question with choices (20, 30, 40 and 50). The Choices should appear for the different students like this: 
       Student 1: A) 20 B) 30 C) 40 D) 50 
       Student 2: A) 50 B) 40 C) 30 D) 20 
       Student 3: A) 30 B) 20 C) 50 D) 40 
       Student 4: A) 50 B) 20 C) 40 D) 30


# Considerations


1. Sign Up and Sign in Pages. Sign up for Professors, who need Approval from The Admin, and for Students.
 2. The Authentication of each page, ex… student cannot add questions, users cannot get into Log in or Sign up pages. 
 3. Data Validation for every input in the System, prevent SQL Injection and Consider Basic Security Concepts. 
 4. If allowed Time of the Exam is finished, then the Exam is automatically submitted. 
 5. The Professor can add multiple Questions in one Request. 
 6. The Professor can add or edit questions for only his Subjects. 
 7. The Professor can see The Results of the Students for only his Subjects
 8. The Student can see only his Result. 
 9. The Student cannot submit the Exam more than one Time. 
 10. The Student is notified of his Rank among his Level. 
 11. The Professor Determine the Exam start time and end time, so that the student cannot access the Exam before or after its time. 
 12. Student can choose some Chapters to get a Training Exam for them. 
 13.  Question can be text, image, audio, video of compilation of them. 
 14. MCQ Questions can have more than one correct answer and the student must select all the correct choices. 
 15. Professor can add Open Ended Questions, which the student answers by writing the answer, and the professor Evaluate the answer manually
