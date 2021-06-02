using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerLogic.Entities
{
    public abstract class IssueBase
    {
        public int IssueID { get; set; }
        public string IssueDesc { get; set; }
        public string IssueTitle { get; set; }
        public Status IssueStatus { get; set; }
        public Priority IssuePriority { get; set; }

        protected internal abstract string ResolveIssue();
    }
}
