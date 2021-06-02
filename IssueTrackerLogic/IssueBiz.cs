using IssueTrackerInfastructure;
using IssueTrackerLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerLogic
{
    public class IssueBiz : IContract
    {
        List<IssueBase> allIssues = new List<IssueBase>()
        {
            new OperationalIssue{IssueID = 101, IssueTitle = "Browser Issuse For Web Portal", IssueDesc = "User is unable to load site on IE.",
                IssuePriority = Priority.Medium , IssueStatus = Status.Open},
            new ServicesIssue{IssueStatus = Status.Open, IssuePriority = Priority.High, IssueDesc = "User needs to email IT support as the wait call is quite long.",
                IssueID = 102, IssueTitle = "Need Customer Service Email"},
            new EngineeringIssue{IssueTitle = "Shipping Service Not Available On Weekends", IssueID = 103, IssueDesc = "Need to have arrangements for shipping during weekends for 24/7 business.",
                IssuePriority = Priority.High, IssueStatus = Status.InProgress}
        };

        private LogHelper _log;

        public void ExtraMethod() { }
        
        public IssueBiz(LogHelper logHelper)
        {
            _log = logHelper;
        }

        public int AddIssue(IssueBase issue)
        {
            allIssues.Add(issue);

            if (issue.IssueTitle.Length > 15)
                _log.AddLogInfo(issue.IssueDesc.Substring(0, 15) + "...Added.");
            else
                _log.AddLogInfo(issue.IssueDesc + "...Added.");

            return issue.IssueID;
        }

        public void ResolveIssue(IssueBase issue)
        {
            string message = issue.ResolveIssue();
            _log.AddLogInfo(message);
        }

        public List<IssueBase> GetAllIssues()
        {
            return allIssues;
        }

        public List<string> GetAllIssueTypes()
        {
            List<string> allIssueTypes = new List<string>()
            {
                "Operational",
                "Service",
                "Engineering"
            };

            return allIssueTypes;
        }

        public List<Priority> GetAllPriorities()
        {
            List<Priority> priorities = new List<Priority>() { Priority.Low, Priority.Medium, Priority.High };
            return priorities;
        }

        public List<Status> GetAllStatus()
        {
            List<Status> statuses = new List<Status>() { Status.Open, Status.InProgress, Status.Closed };
            return statuses;
        }
            
        public int UpdateIssue(IssueBase issue)
        {
            foreach (var i in allIssues)
            {
                if(i.IssueID == issue.IssueID)
                {
                    allIssues.Remove(i);
                    break;
                }
            }

            allIssues.Add(issue);

            if (issue.IssueDesc.Length > 15)
                _log.AddLogInfo($"{issue.IssueDesc.Substring(0, 15)} ...Upadated.");
            else
                _log.AddLogInfo($"{issue.IssueDesc} ...Updated.");

            return issue.IssueID;
        }

        public string GetIssueType(IssueBase issue)
        {
            string type = issue.GetType().Name;
            string result = "";
            switch (type)
            {
                case "EngineeringIssue":
                    result = "Engineering";
                    break;
                case "ServicesIssue":
                    result = "Services";
                    break;
                case "OperationalIssue":
                    result = "Operations";
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
