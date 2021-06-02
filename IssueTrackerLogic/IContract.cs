using IssueTrackerLogic.Entities;
using System;
using System.Collections.Generic;

namespace IssueTrackerLogic
{
    public interface IContract
    {
        List<IssueBase> GetAllIssues();
        List<string> GetAllIssueTypes();
        string GetIssueType(IssueBase issue);
        List<Status> GetAllStatus();
        List<Priority> GetAllPriorities();
        int AddIssue(IssueBase issue);
        int UpdateIssue(IssueBase issue);
        void ResolveIssue(IssueBase issue);
    }
}
