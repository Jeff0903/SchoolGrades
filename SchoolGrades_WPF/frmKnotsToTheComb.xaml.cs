﻿using SchoolGrades;
using SchoolGrades.BusinessObjects;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolGrades_WPF
{
    /// <summary>
    /// Interaction logic for KnotsToTheComb.xaml
    /// </summary>
    public partial class frmKnotsToTheComb : Window
    {
        private Question chosenQuestion = new Question();
        private frmMicroAssessment grandparentForm;

        private Student currentStudent;
        private SchoolSubject currentSubject;
        private string currentIdSchoolYear;
        private int currentIdGrade;

        bool isLoading = true;

        internal Question ChosenQuestion { get; private set; }
        public frmKnotsToTheComb(frmMicroAssessment GrandparentForm, int? IdStudent, SchoolSubject SchoolSubject, string Year)
        {
            InitializeComponent();
            currentStudent = Commons.dl.GetStudent(IdStudent);
            lblStudent.Text = currentStudent.LastName + " " + currentStudent.FirstName;
            currentIdSchoolYear = Year;
            currentSubject = SchoolSubject;
            grandparentForm = GrandparentForm;

            // fills the lookup tables' combos
            cmbSchoolSubject.DisplayMember = "Name";
            cmbSchoolSubject.ValueMember = "idSchoolSubject";
            cmbSchoolSubject.ItemsSource = Commons.dl.GetListSchoolSubjects(true);

            currentSubject = SchoolSubject;
            ChosenQuestion = null;
        }
        private void FrmKnotsToTheComb_Load(object sender, EventArgs e)
        {
            if (currentSubject.IdSchoolSubject != null)
                cmbSchoolSubject.SelectedValue = currentSubject.IdSchoolSubject;
            RefreshData();
        }
        private void RefreshData()
        {
            dgwQuestions.ItemsSource = Commons.dl.GetUnfixedGrades(currentStudent, currentSubject.IdSchoolSubject, 60);
        }
        private void DgwQuestions_CellContentClick(object sender, RoutedEvent e)
        {

        }
        private void DgwQuestions_RowEnter(object sender, RoutedEvent e)
        {
            DataGrid grid = (DataGrid)sender;
            int RowIndex = grid.SelectedIndex;
            if (RowIndex > -1)
            {
                DataGridRow r = dgwQuestions.Rows[RowIndex];
                txtQuestionText.Text = (string)r.Cells["Text"].Value;
                currentIdGrade = (int)r.Cells["IdQuestion"].Value;
            }
        }
        private void DgwQuestions_CellClick(object sender, RoutedEvent e)
        {
            DataGrid grid = (DataGrid)sender;
            int RowIndex = grid.SelectedIndex;
            if (RowIndex > -1)
            {
                dgwQuestions.Rows[RowIndex].Selected = true;
            }
        }
        private void DgwQuestions_CellDoubleClick(object sender, RoutedEvent e)
        {
            // choose this question
            // !!!! TODO !!!!
        }
        private void BtnFix_Click(object sender, EventArgs e)
        {
            if (dgwQuestions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selezionare la domanda che è stata riparata");
                return;
            }
            DataGridRow r = dgwQuestions.SelectedItems[0];
            currentIdGrade = (int)r.Cells["IdGrade"].Value;
            if (MessageBox.Show("La domanda '" + (string)r.Cells["Text"].Value + "' è stata riparata?", "Riparazione domanda",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Commons.bl.FixQuestionInGrade(currentIdGrade);
                RefreshData();
            }
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (dgwQuestions.SelectedItems.Count > 0)
            {
                //int key = int.Parse(dgwQuestions.SelectedItems[0].Cells[6].Value.ToString());
                int key = (int)dgwQuestions.SelectedItems[0].Cells[6].Value;
                ChosenQuestion = Commons.bl.GetQuestionById(key);
                if (grandparentForm != null)
                {
                    // form called by student's assessment form 
                    grandparentForm.CurrentQuestion = ChosenQuestion;
                    grandparentForm.DisplayCurrentQuestion();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Scegliere una domanda nella griglia");
                return;
            }
        }
        private void cmbSchoolSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSubject = (SchoolSubject)cmbSchoolSubject.SelectedItem;
            this.Background = CommonsWinForms.ColorFromNumber(currentSubject);
            RefreshData();
        }
    }
}
