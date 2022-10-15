﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    public interface IRowStatus
    {
        long RowStatusId { get; set; }
        string TitleEnglish { get; set; }
        string TitleArabic { get; set; }
        DateTime? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
