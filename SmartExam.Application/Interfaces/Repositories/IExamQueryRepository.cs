﻿using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.Interfaces.Repositories
{
    public interface IExamQueryRepository: IGenericRepository<ExamQuery>
    {
        Task<List<int>> GetRandomQuestionIdByQuery(IList<ExamQuery> examQueries);
    }
}
