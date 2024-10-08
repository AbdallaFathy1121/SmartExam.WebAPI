﻿using Namshi.Infrastructure.Context;
using SmartExam.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IChapterRepository ChapterRepository { get; private set; }
        public ISubjectRepository SubjectRepository { get; private set; }
        public IModelRepository ModelRepository { get; private set; }
        public IQuestionRepository QuestionRepository { get; private set; }
        public IExamRepository ExamRepository { get; private set; }
        public IExamQueryRepository ExamQueryRepository { get; private set; }
        public IStudentExamRepository StudentExamRepository { get; private set; }
        public IStudentExamQuestionRepository StudentExamQuestionRepository { get; private set; }
        public IStudentAnswerRepository StudentAnswerRepository { get; private set; }
        public IExamResultRepository ExamResultRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ChapterRepository = new ChapterRepository(context);
            SubjectRepository = new SubjectRepository(context);
            ModelRepository = new ModelRepository(context);
            QuestionRepository = new QuestionRepository(context);
            ExamRepository = new ExamRepository(context);
            ExamQueryRepository = new ExamQueryRepository(context);
            StudentExamRepository = new StudentExamRepository(context);
            StudentExamQuestionRepository = new StudentExamQuestionRepository(context);
            StudentAnswerRepository = new StudentAnswerRepository(context);
            ExamResultRepository = new ExamResultRepository(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
