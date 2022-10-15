using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZHPCMS.Common
{
    public enum RowStatus
    {
        Active = 2,
        InActive = 4,
        Delete = 5
    };

    public enum FileTypes
    {
        Picture = 1,
        Video = 2,
        Word = 3,
        Pdf = 4,
        Audio = 5,
        Excel = 6
    };

    public enum Archive
    {
        Form = 1,
        RulesAndRegulations = 2,
        Magzine = 3
    };

    public enum Days
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7

    };

    public enum Rating
    { 
        VerySatisfied = 1,
        Satisfied = 2 ,
        UnSatisfied = 3
    }

}
