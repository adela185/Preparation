using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerLogic.Entities
{
    public class OperationalIssue : IssueBase
    {
        protected internal override string ResolveIssue()
        {
            return $"Operational Issue Solved: {this.IssueTitle}";
        }
    }
}
