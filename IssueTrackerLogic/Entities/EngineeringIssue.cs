using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerLogic.Entities
{
    public class EngineeringIssue : IssueBase
    {
        protected internal override string ResolveIssue()
        {
            return $"Engineering Issue Solved: {this.IssueTitle}";
        }
    }
}
