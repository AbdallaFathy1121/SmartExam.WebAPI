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

        Task<int> CompleteAsync();
    }
}
