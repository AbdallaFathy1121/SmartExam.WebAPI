using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.Interfaces.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IChapterRepository ChapterRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IModelRepository ModelRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IExamRepository ExamRepository { get; }
        IExamQueryRepository ExamQueryRepository { get; }
        IStudentExamRepository StudentExamRepository { get; }
        IStudentExamQuestionRepository StudentExamQuestionRepository { get; }

        Task<int> CompleteAsync();
    }
}
