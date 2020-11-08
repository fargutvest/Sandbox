using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MTBReportParser
{
    public enum Status
    {
        [Display(Name = "[SUCCESS]")]
        Success,
        [Display(Name = "[DECLINE]")]
        Decline,
        [Display(Name = "[IN_PROGRESS]")]
        InProgress,
        [Display(Name = "[UNKNOWN]")]
        Unknown
    }
}
