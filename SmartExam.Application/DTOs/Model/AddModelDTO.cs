﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.Model
{
    public record AddModelDTO (
        string Name,
        int ChapterId
    );
}
