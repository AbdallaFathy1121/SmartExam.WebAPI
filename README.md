
# Smart Exam
<ul>
<li><b>API-Controlled Exam Creation:</b> Allows teachers to register, add subjects, and create Chapters.</li>
<li><b>Exam Structure:</b> Each chapter contains models, and each model includes MCQ questions with correct answers, forming a question bank.</li>
<li><b>Randomized Question Selection:</b> Teachers can specify the number of random questions from a particular model for exams.</li>
<li><b>Automated Exam Process:</b> When a student starts an exam, the system randomly selects questions based on the teacher's criteria.</li>
<li><b>Instant Grading:</b> After completing the exam, the student's score is immediately displayed.</li>
</ul>

## Project Map

1. **Users Management**
   - Get All
   - Get By Id
   - Register
   - Login
   - Delete

2. **Subjects Management**
   - Get All
   - Get By Id
   - Add
   - Update
   - Delete

3. **Chapters Management**
   - Get All
   - Get By Id
   - Get By Subject Id
   - Add
   - Update
   - Delete

4. **Models Management**
   - Get All
   - Get By Id
   - Get By Chapter Id
   - Add
   - Update
   - Delete

5. **Exams Management**
   - Get All
   - Get By User Id
   - Get By Id
   - Add
   - Update
   - Delete

6. **Exam Queries Management**
   - Get All
   - Get By Id
   - Get By Exam Id
   - Add
   - Update
   - Delete

7. **Student Exams Management**
   - Get All
   - Get By Id
   - Add
   
8. **Student Exam Questions Management**
   - Add List Of Questions By (Student Exam Id)
   
9. **Student Answers Management**
    - Add List Of Answers

10. **Exam Results Management**
    - Add By Student Exam Id
   

## Dependencies

### Technologies
- .NET 8.0 Web API
- EF
- Identity
- LINQ
- Clean Architecture
- Repository Pattern
- Unit of Work
- MVVM Pattern
- Global Error Handling

### Tools
- Serilog
- Hangfire

