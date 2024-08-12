using Hangfire;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Application.Interfaces.Services;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Services
{
    public class ChangeExamStatus : IChangeExamStatus
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChangeExamStatus(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void ChangeExamStatu(Exam exam, bool status)
        {
            exam.Status = status;
            _unitOfWork.ExamRepository.UpdateAsync(exam.Id, exam);
            _unitOfWork.CompleteAsync();
        }
    }
}
