using System.Collections.Generic;

namespace BankOperations.ReportParsers
{
    public interface IBankParser
    {
        List<Operation> Load();
        ProgressReporter ProgressReporter { get; }
    }
}
