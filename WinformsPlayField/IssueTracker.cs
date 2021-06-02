using IssueTrackerInfastructure;
using IssueTrackerLogic;
using IssueTrackerLogic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsPlayField
{
    public partial class IssueTracker : Form
    {
        private IContract _issueBiz;
        private LogHelper _logger;
        private IssueBase issueToSave;

        public IssueTracker()
        {
            InitializeComponent();
            //Initializing Dependencies
            _logger = new LogHelper();
            _issueBiz = new IssueBiz(_logger);
            _logger.LogUpdated += _logger_LogUpdated;

            //Works if _issueBiz is of type IssueBiz, but not IContract beccause the interface doesn't blueprint the extra method in child despite it being legal in child
            //_issueBiz.ExtraMethod();
        }

        private void _logger_LogUpdated(object sender, List<LogDetailArgs> e)
        {
            ibLog.DataSource = null;
            ibLog.DataSource = e;
        }



        private void btnNew_Click(object sender, EventArgs e)
        {
            issueToSave = null;
            txtID.Text = "";
            txtTitle.Text = "";
            txtDesc.Text = "";
            cmbPriority.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            dgvAllIssues.ClearSelection();
            cmbType.Enabled = true;
            btnResolve.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool newIssue = false;
            string type = cmbType.SelectedItem.ToString();

            if(issueToSave == null)
            {
                newIssue = true;
                switch (type)
                {
                    case "Operational":
                        issueToSave = new OperationalIssue();
                        break;

                    case "Service":
                        issueToSave = new ServicesIssue();
                        break;

                    case "Engineering":
                        issueToSave = new EngineeringIssue();
                        break;
                    default:
                        break;
                }
            }

            int issueID;
            int.TryParse(txtID.Text, out issueID);
            issueToSave.IssueID = issueID;
            issueToSave.IssueDesc = txtDesc.Text;
            issueToSave.IssueTitle = txtTitle.Text;
            Priority priority;
            Enum.TryParse<Priority>(cmbPriority.SelectedItem.ToString(), out priority);
            issueToSave.IssuePriority = priority;
            Status status;
            Enum.TryParse<Status>(cmbStatus.SelectedItem.ToString(), out status);
            issueToSave.IssueStatus = status;

            if (newIssue)
                _issueBiz.AddIssue(issueToSave);
            else
                _issueBiz.UpdateIssue(issueToSave);

            LoadIssues();
        }

        private void LoadIssues()
        {
            BindingSource source = new BindingSource();
            source.DataSource = _issueBiz.GetAllIssues();
            dgvAllIssues.DataSource = source;
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            _issueBiz.ResolveIssue(issueToSave);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadIssues();
        }

        private void IssueTracker_Load(object sender, EventArgs e)
        {
            cmbPriority.DataSource = _issueBiz.GetAllPriorities();
            cmbStatus.DataSource = _issueBiz.GetAllStatus();
            cmbType.DataSource = _issueBiz.GetAllIssueTypes();
            btnResolve.Enabled = false;
        }

        private void dgvAllIssues_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvAllIssues.SelectedRows.Count > 0)
                {
                    int selectedIssue;
                    int.TryParse(dgvAllIssues.SelectedRows[0].Cells[0].Value.ToString(), out selectedIssue);

                    foreach (var issue in _issueBiz.GetAllIssues())
                    {
                        if (issue.IssueID == selectedIssue)
                        {
                            issueToSave = issue;
                            break;
                        }
                    }

                    txtID.Text = issueToSave.IssueID.ToString();
                    txtDesc.Text = issueToSave.IssueDesc;
                    txtTitle.Text = issueToSave.IssueTitle;
                    cmbPriority.SelectedItem = issueToSave.IssuePriority;
                    cmbStatus.SelectedItem = issueToSave.IssueStatus;
                    cmbType.SelectedItem = _issueBiz.GetIssueType(issueToSave);
                    cmbType.Enabled = false;
                    btnResolve.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                _logger.AddLogInfo($"Error: {ex.Message}");
            }
        }
    }
}
