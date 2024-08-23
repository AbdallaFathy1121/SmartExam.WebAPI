
# Smart Exam
<ul>
<li><b>API-Controlled Exam Creation:</b> Allows teachers to register, add subjects, and create models.</li>
<li><b>Exam Structure:</b> Each model contains sections, and each section includes MCQ questions with correct answers, forming a question bank.</li>
<li><b>Randomized Question Selection:</b> Teachers can specify the number of random questions from a particular model for exams.</li>
<li><b>Automated Exam Process:</b> When a student starts an exam, the system randomly selects questions based on the teacher's criteria.</li>
<li><b>Instant Grading:</b> After completing the exam, the student's score is immediately displayed.</li>
</ul>


## Project Map

1. **Authors Management**
   - Add Author
   - Update Author

2. **Categories Management**
   - Add Category
   - Update Category

3. **Books Management**
   - Add Book
   - Update Book
   - Allow/Disallow Rentals
   - Manage Copies
     - Add Copy
     - Update Copy
     - Allow/Disallow Rentals for Copies
     - View Copy Rentals History

4. **Subscribers Management**
   - Add Subscriber
   - Send Welcome Email
   - Update Subscriber
   - Block Subscriber
   - View Subscriber Profile
   - View Subscriber Rentals History

5. **Rentals Management**
   - Add Rentals
   - Update Rentals
   - Send Rental Email
   - Cancel Rental
   - Handle Rental Returns
   - Send Reminder Email
   - Apply Delay Penalties

6. **User Management**
   - Add User
   - Update User
   - Delete User
   - Manage User Roles

7. **Reports**
   
8. **Search**

## Dependencies

### Prerequisites
- .NET 7.0
- Vanilla JavaScript
- jQuery
- HTML & CSS
- Metronic

### Tools
- View to HTML
- UoN ExpressAnnotation
- Serilog
- OpenHtmlToPdf
- Hashids
- Hangfire
- ClosedXML

### Libraries
- Handlebars
- Typeahead.js
- ApexCharts

