using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerLogic.Entities
{
    public class ServicesIssue : IssueBase
    {
        protected internal override string ResolveIssue()
        {
            return $"Services Issue Solved: {this.IssueTitle}";
        }
    }
}
