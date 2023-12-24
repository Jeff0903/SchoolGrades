﻿using SchoolGrades;
using SchoolGrades.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolGrades_WPF
{
    /// <summary>
    /// Logica di interazione per frmTopicChooseByPeriod.xaml
    /// </summary>
    public partial class frmTopicChooseByPeriod : Window
    {
        private Class currentClass;
        private SchoolSubject currentSubject;

        internal Topic TopicChosen { get; private set; }

        List<Topic> topicsDone;

        public enum TopicChooseFormType
        {
            ChooseTopicOnExit,
            OpenTopicOnExit,
        }
        TopicChooseFormType formType;
        private SchoolPeriod currentSchoolPeriod;
        public frmTopicChooseByPeriod(TopicChooseFormType FormType,
            Class Class, SchoolSubject Subject)
        {
            InitializeComponent();
            currentClass = Class;
            currentSubject = Subject;
            formType = FormType;
            TopicChosen = new Topic();
            TopicChosen.Id = 0;
        }
        private void frmTopicChooseByPeriod_Load(object sender, EventArgs e)
        {
            if (currentSubject != null)
            {
                dtpEndPeriod.Value = DateTime.Now;
                dtpStartPeriod.Value = dtpEndPeriod.Value.AddDays(-7);
                txtSchoolSubject.Text = currentSubject.IdSchoolSubject;
                txtClass.Text = currentClass.Abbreviation;
                txtSchoolYear.Text = currentClass.SchoolYear;
            }
            else
            {

            }
            List<SchoolPeriod> listPeriods = Commons.bl.GetSchoolPeriods(currentClass.SchoolYear);
            cmbSchoolPeriod.ItemsSource = listPeriods;
            // select the combo item of the partial period of the DateTime.Now
            foreach (SchoolPeriod sp in listPeriods)
            {
                if (sp.DateFinish > DateTime.Now && sp.DateStart < DateTime.Now
                    && sp.IdSchoolPeriodType == "P")
                {
                    cmbSchoolPeriod.SelectedItem = sp;
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime dateFrom;
            if (cmbSchoolPeriod.Text == "")
                dateFrom = Commons.DateNull;
            else
                dateFrom = dtpStartPeriod.Value;
            topicsDone = Commons.bl.GetTopicsDoneInPeriod(currentClass, currentSubject,
                dateFrom, dtpEndPeriod.Value);

            dgwTopics.ItemsSource = topicsDone;
            //if (chkVisualizePath.IsChecked)
            //    dgwTopics.Columns[0].Visible = true;
            //else
            //    dgwTopics.Columns[0].Visible = false;

            dgwTopics.Columns[0].Visible = true;
            dgwTopics.Columns[1].Visible = false;
            dgwTopics.Columns[2].Visible = false;
            dgwTopics.Columns[3].Visible = false;
            dgwTopics.Columns[4].Visible = false;
            dgwTopics.Columns[5].Visible = false;
            dgwTopics.Columns[6].Visible = true;
            dgwTopics.Columns[7].Visible = true;
            dgwTopics.Columns[8].Visible = false;
            dgwTopics.Columns[9].Visible = false;
            dgwTopics.Columns[10].Visible = false;
            dgwTopics.Columns[11].Visible = false;
            dgwTopics.Columns[12].Visible = false;
        }
        private void dgwTopics_CellContentClick(object sender, RoutedEvent e)
        {

        }
        private void dgwTopics_CellContentDoubleClick(object sender, RoutedEvent e)
        {
            DataGrid grid = (DataGrid)sender;
            int RowIndex = grid.SelectedIndex;
            if (RowIndex > -1)
            {
                DataGridRow value = dgwTopics.Rows[RowIndex];
                switch (formType)
                {
                    case (TopicChooseFormType.ChooseTopicOnExit):
                        {
                            if (value != null)
                            {
                                TopicChosen = topicsDone[RowIndex];
                                this.Close();
                            }
                            break;
                        }
                    case (TopicChooseFormType.OpenTopicOnExit):
                        {
                            List<Topic> oneItemList = new List<Topic>();
                            oneItemList.Add(topicsDone[RowIndex]);
                            frmTopics t = new frmTopics(frmTopics.TopicsFormType.HighlightTopics,
                                currentClass, currentSubject, null, oneItemList);
                            t.ShowDialog();
                            //t.Dispose();
                            break;
                        }
                }
            }
        }
        private void btnRandomTopic_Click(object sender, EventArgs e)
        {
            if (topicsDone == null)
            {
                topicsDone = Commons.bl.GetTopicsDoneInPeriod(currentClass, currentSubject,
                    dtpStartPeriod.Value, dtpEndPeriod.Value);
            }
            if (topicsDone.Count > 0)
            {
                Random r = new Random();
                int index = r.Next(topicsDone.Count);
                TopicChosen = topicsDone[index];
                this.Close();
            }
            Console.Beep();
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (dgwTopics.SelectedItems.Count == 0)
            {
                MessageBox.Show("Scegliere un argomento nella griglia");
                return;
            }
            int rowIndex = dgwTopics.SelectedItems[0].Index;
            //DataRow row = ((DataTable)(dgwTopics.ItemsSource)).Rows[rowIndex];
            DataGridRow value = dgwTopics.Rows[rowIndex];
            switch (formType)
            {
                case (TopicChooseFormType.ChooseTopicOnExit):
                    {
                        if (value != null)
                        {
                            TopicChosen = topicsDone[rowIndex];
                            this.Close();
                        }
                        break;
                    }
                case (TopicChooseFormType.OpenTopicOnExit):
                    {
                        List<Topic> oneItemList = new List<Topic>();
                        oneItemList.Add(topicsDone[rowIndex]);
                        frmTopics t = new frmTopics(frmTopics.TopicsFormType.HighlightTopics,
                            currentClass, currentSubject, null, oneItemList);
                        t.ShowDialog();
                        t.Dispose();
                        break;
                    }
            }
        }
        private void cmbStandardPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSchoolPeriod = (SchoolPeriod)(cmbSchoolPeriod.SelectedValue);
            if (currentSchoolPeriod.IdSchoolPeriodType != "N")
            {
                dtpStartPeriod.Value = (DateTime)currentSchoolPeriod.DateStart;
                dtpEndPeriod.Value = (DateTime)currentSchoolPeriod.DateFinish;
            }
            else if (currentSchoolPeriod.IdSchoolPeriod == "month")
            {
                dtpStartPeriod.Value = DateTime.Now.AddMonths(-1);
                dtpEndPeriod.Value = DateTime.Now;
            }
            else if (currentSchoolPeriod.IdSchoolPeriod == "week")
            {
                dtpStartPeriod.Value = DateTime.Now.AddDays(-7);
                dtpEndPeriod.Value = DateTime.Now;
            }
            else if (currentSchoolPeriod.IdSchoolPeriod == "year")
            {
                dtpStartPeriod.Value = DateTime.Now.AddYears(-1);
                dtpEndPeriod.Value = DateTime.Now;
            }
        }
        private void dgwTopics_CellClick(object sender, RoutedEvent e)
        {
            DataGrid grid = (DataGrid)sender;
            int RowIndex = grid.SelectedIndex;
            if (RowIndex > -1)
            {
                dgwTopics.Rows[RowIndex].Selected = true;
            }
        }
    }
}
