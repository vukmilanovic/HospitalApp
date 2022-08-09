﻿using Controller;
using Model;
using Patient.Views;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for ExaminationsList.xaml
    /// </summary>
    public partial class ExaminationsList : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private ExamController _examinationController;
        //private ExaminationRepo _examinationRepo;
        private DoctorController _doctorController;
        private PatientController _patientController;

        private ObservableCollection<Examination> examinations;
        private List<Examination> examinationsForDate;
        private List<DateOnly> datesExaminations;
        public static Examination selected;
        public static bool remove;

        public ObservableCollection<Examination> Examinations
        {
            get
            {
                return examinations;
            }
            set
            {
                examinations = value;
                OnPropertyChanged("Examinations");
            }
        }

        public List<Examination> ExaminationsForDate
        {
            get
            {
                return examinationsForDate;
            }
            set
            {
                examinationsForDate = value;
                OnPropertyChanged("ExaminationsForDate");
            }
        }

        public List<DateOnly> DatesExaminations
        {
            get
            {
                return datesExaminations;
            }
            set
            {
                datesExaminations = value;
                OnPropertyChanged("DatesExamiantions");
            }
        }

        public ExaminationsList()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            //_examinationRepo = app.ExaminationRepo;
            _examinationController = app.ExamController;
            _doctorController = app.DoctorController;
            _patientController = app.PatientController;

            //if (File.Exists(_examinationRepo.dbPath))
            //    _examinationRepo.LoadExamination();

            examinations = new ObservableCollection<Examination>();
            examinations = _examinationController.ReadPatientExams(Login.loggedId);
            foreach (Examination exam in examinations)
            {
                exam.DoctorNameSurname = _doctorController.GetDoctor(exam.DoctorId).NameSurname;
            }
            //Examinations = examinations;

            DateTime today = DateTime.Now;
            examinationsForDate = new List<Examination>();
            datesExaminations = new List<DateOnly>();
            Calendar.SelectedDate = DateTime.Now;
            Calendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(1990, 1, 1), DateTime.Now.AddDays(-1)));
            //foreach(Examination exam in examinations)
            //{
            //    if(exam.Date.Date == today.Date)
            //    {
            //        if(exam.DoctorType == DoctorType.Pulmonology)
            //        {
            //            exam.DoctorTypeString = "Pulmologija";
            //        }else if (exam.DoctorType == DoctorType.Cardiology)
            //        {
            //            exam.DoctorTypeString = "Kardiologija";
            //        }
            //        else
            //        {
            //            exam.DoctorTypeString = "Opšta praksa";
            //        }
            //        ExaminationsForDate.Add(exam);
            //    }
            //    if (!DatesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
            //    {
            //        DatesExaminations.Add(DateOnly.FromDateTime(exam.Date));

            //    }
            //}
            //dataGridExaminations.ItemsSource = ExaminationsForDate;
            foreach (Examination exam in Examinations)
            {
                if (!datesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    datesExaminations.Add(DateOnly.FromDateTime(exam.Date));

                }
            }
            remove = false;
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            Message.Visibility = Visibility.Hidden;
            DateTime selected = (DateTime)Calendar.SelectedDate;
            if (selected == null || selected.Date == DateTime.Now.Date){
                selected = DateTime.Now.AddDays(1);
            }

            if (_patientController.CheckStatusCancelled(Login.loggedId))
            {
                if (_patientController.CheckStatusAdded(Login.loggedId))
                {
                    Message.Visibility = Visibility.Hidden;
                    //AddExamination addExamination = new AddExamination(selected);
                    //addExamination.ShowDialog();
                    AddExaminationMVVM addExaminationMVVM = new AddExaminationMVVM(selected);
                    addExaminationMVVM.ShowDialog();
                }
                else
                {
                    Message.Content = "Blokirano je zakazivanje";
                    Message.Visibility = Visibility.Visible;
                }
                
            }
            else
            {
                Message.Content = "Blokirani ste";
                Message.Visibility = Visibility.Visible;
            }
           
            //dataGridExaminations.ItemsSource = _examinationController.ReadPatientExams(Login.loggedId);
            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            
            examinationsForDate = new List<Examination>();
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == selected.Date)
                {
                    Doctor doctor = _doctorController.GetDoctor(exam.DoctorId);
                    if (doctor.Type == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (doctor.Type == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    //else if (doctor.Type == DoctorType.Neurology)
                    //{
                    //    exam.DoctorTypeString = "Neurologija";
                    //}
                    //else if (doctor.Type == DoctorType.Dermatology)
                    //{
                    //    exam.DoctorTypeString = "Dermatologija";
                    //}
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    examinationsForDate.Add(exam);
                }
                if (!datesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    datesExaminations.Add(DateOnly.FromDateTime(exam.Date));
                }
            }
            //dataGridExaminations.ItemsSource = ExaminationsForDate;
            Calendar.DataContext = DatesExaminations;
            //Window.GetWindow(this).Content = new PatientMenu();
            Calendar.SelectedDate = selected.AddDays(1);
            Calendar.SelectedDate = selected;
        }

        private void EditExamination_Click(object sender, RoutedEventArgs e)
        {
            selected = (Examination)dataGridExaminations.SelectedItem;
            DateTime selectedInCalendar = (DateTime)Calendar.SelectedDate;
            if (selected != null)
            {
                if (_patientController.CheckStatusCancelled(Login.loggedId))
                {
                    //if (selected.Date.CompareTo(DateTime.Now) >= 0)
                    if (selected.Date.Year == DateTime.Now.Year && (selected.Date.Month<DateTime.Now.Month || (selected.Date.Month == DateTime.Now.Month && selected.Date.Day < DateTime.Now.Day)))
                    {
                        Message.Visibility = Visibility.Hidden;
                        //EditExamination editExamination = new EditExamination();
                        //editExamination.ShowDialog();
                        EditExaminationMVVM editExamiantion = new EditExaminationMVVM();
                        editExamiantion.ShowDialog();
                    }
                    else
                    {
                        Message.Content = "Ne mozete pomeriti danasnji termin";
                        Message.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    Message.Content = "Blokirani ste";
                    Message.Visibility = Visibility.Visible;
                }
                

            }
            else
            {
                Message.Content = "Morate izabrati termin za pomeranje";
                Message.Visibility = Visibility.Visible;
            }
            //dataGridExaminations.ItemsSource = _examinationController.ReadPatientExams(Login.loggedId);
            examinations = _examinationController.ReadPatientExams(Login.loggedId);
            datesExaminations = new List<DateOnly>();
            DateTime today = DateTime.Now;
            examinationsForDate = new List<Examination>();
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == selectedInCalendar.Date)
                {

                    Doctor doctor = _doctorController.GetDoctor(exam.DoctorId);
                    if (doctor.Type == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (doctor.Type == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    //else if (doctor.Type == DoctorType.Neurology)
                    //{
                    //    exam.DoctorTypeString = "Neurologija";
                    //}
                    //else if (doctor.Type == DoctorType.Dermatology)
                    //{
                    //    exam.DoctorTypeString = "Dermatologija";
                    //}
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    examinationsForDate.Add(exam);
                }
                if (!datesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    datesExaminations.Add(DateOnly.FromDateTime(exam.Date));
                }
            }
            Calendar.SelectedDate = selectedInCalendar.AddDays(1);
            Calendar.SelectedDate = selectedInCalendar;
            //dataGridExaminations.ItemsSource = ExaminationsForDate;
            Calendar.DataContext = DatesExaminations;
            
        }

        private void RemoveExamination_Click(object sender, RoutedEventArgs e)
        {
            selected = (Examination)dataGridExaminations.SelectedItem;
            DateTime selectedInCalendar = (DateTime)Calendar.SelectedDate;
            if (selected != null)
            {
                if (_patientController.CheckStatusCancelled(Login.loggedId))
                {
                    if (selected.Date.Year == DateTime.Now.Year && (selected.Date.Month < DateTime.Now.Month || (selected.Date.Month == DateTime.Now.Month && selected.Date.Day < DateTime.Now.Day)))
                    //if (selected.Date.CompareTo(DateTime.Now) >= 0)
                    {
                        Message.Visibility = Visibility.Hidden;
                        WesNowRemove yesNoRemove = new WesNowRemove();
                        yesNoRemove.ShowDialog();
                        if (remove)
                        {
                            _examinationController.RemoveExam((Examination)dataGridExaminations.SelectedItem);
                            //_examinationRepo.SaveExamination();
                            _examinationController.SaveExaminationRepo();
                            //dataGridExaminations.ItemsSource = _examinationController.ReadPatientExams(Login.loggedId);
                            examinationsForDate = _examinationController.ReadPatientExams(Login.loggedId).ToList();
                        }
                    }
                    else
                    {
                        Message.Content = "Ne mozete otkazati danasnji termin";
                        Message.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    Message.Content = "Blokirani ste";
                    Message.Visibility = Visibility.Visible;
                }
                
            }
            else
            {
                Message.Content = "Morate izabrati termin za otkazivanje";
                Message.Visibility = Visibility.Visible;
            }
            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            examinationsForDate = new List<Examination>();
            datesExaminations = new List<DateOnly>();
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == selectedInCalendar.Date)
                {
                    Doctor doctor = _doctorController.GetDoctor(exam.DoctorId);
                    if (doctor.Type == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (doctor.Type == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    //else if (doctor.Type == DoctorType.Neurology)
                    //{
                    //    exam.DoctorTypeString = "Neurologija";
                    //}
                    //else if (doctor.Type == DoctorType.Dermatology)
                    //{
                    //    exam.DoctorTypeString = "Dermatologija";
                    //}
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    ExaminationsForDate.Add(exam);
                }
                if (!datesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    datesExaminations.Add(DateOnly.FromDateTime(exam.Date));
                }
            }
            //dataGridExaminations.ItemsSource = ExaminationsForDate;
            Calendar.DataContext = DatesExaminations;
            //Window.GetWindow(this).Content = new PatientMenu();
            Calendar.SelectedDate = selectedInCalendar.AddDays(1);
            Calendar.SelectedDate = selectedInCalendar;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //_examinationRepo.SaveExamination();
            _examinationController.SaveExaminationRepo();
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            //Window.GetWindow(this).Content = new PatientMenu();
            //this.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Collapsed;
            Window.GetWindow(this).Content = new PatientMenu();
        }

        private void ChangeSelected(object sender, SelectionChangedEventArgs e)
        {
            DateTime selected = (DateTime)Calendar.SelectedDate;
            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            DateTime today = DateTime.Now;
            examinationsForDate = new List<Examination>();
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == selected.Date)
                {
                    Doctor doctor = _doctorController.GetDoctor(exam.DoctorId);
                    if (doctor.Type == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (doctor.Type == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    //ovde dodati neurologiju i dermatologiju
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    ExaminationsForDate.Add(exam);
                }
            }
            dataGridExaminations.ItemsSource = ExaminationsForDate;
        }
        private void calendarButton_Loaded(object sender, EventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
            button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);
        }

        private void HighlightDay(CalendarDayButton button, DateTime date)
        {
            if (DatesExaminations.Contains(DateOnly.FromDateTime(date)))
                button.Background = Brushes.LightBlue;
            else
                button.Background = Brushes.White;
        }

        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }



    }
    
   
}
