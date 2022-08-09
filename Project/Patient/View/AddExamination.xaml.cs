﻿using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utility;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for AddExamination.xaml
    /// </summary>
    public partial class AddExamination : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private ExamController _examController;
        private DoctorController _doctorController;
        private PatientController _patientController;
        private RoomController _roomController;
        
        private List<String> doctorTypes;

        
        

        public ObservableCollection<Doctor> DoctorsObs
        {
            get;
            set;
        }

        public static DateTime startDate;
        private DateTime endDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public List<String> DoctorTypes
        {
            get
            {
                return doctorTypes;
            }
            set
            {
                doctorTypes = value;
                OnPropertyChanged("DoctorTypes");
            }
        }
        
        

        private static Random random = new Random((int)DateTime.Now.Ticks);

        

        private string RandomString(int Size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
        public AddExamination(DateTime sDate)
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _examController = app.ExamController;
            _doctorController = app.DoctorController;
            _patientController = app.PatientController;
            _roomController = app.RoomController;

            
            DoctorsObs = new ObservableCollection<Doctor>();
            doctorTypes = new List<string>();
            //StartDate = DateTime.Now.AddDays(1);
            StartDate = sDate;
            //EndDate = DateTime.Now.AddDays(7);
            EndDate = sDate.AddDays(7);
            List<Doctor> doctors = _doctorController.GetAllDoctors().ToList();
            
            DoctorTypeSelected.SelectedIndex = 0;
            foreach (Doctor doctor in doctors)
            {
                if(doctor.Type == Model.DoctorType.Pulmonology)
                {
                    DoctorsObs.Add(doctor);
                }
            }

            doctorTypes.Add("Pulmologija");
            doctorTypes.Add("Opšta praksa");
            doctorTypes.Add("Kardiologija");
        }

        private IList<Doctor> FindAllDoctors()
        {
            return _doctorController.GetAllDoctors()
                .ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DoctorCombo.SelectedIndex != -1)
            {
                this.DataContext = this;
                Doctor doctor = (Doctor)DoctorCombo.SelectedItem;
                
                StartDate = (DateTime)Start.SelectedDate;
                
                EndDate = (DateTime)End.SelectedDate;

                bool priority = true; //ako je doktor onda je true, termin false
                if(doctorPriority.IsChecked == true)
                {
                    priority = true;
                }
                else
                {
                    priority = false;
                }

                //List<Examination> listExaminationsWithRooms = _doctorController.GetFreeGetFreeExaminations(doctor, startDate, endDate, priority);

                //foreach(Examination exam in listExaminationsWithRooms)
                //{
                //    exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
                //}
                //ExamsAvailable.ItemsSource = listExaminationsWithRooms;
            }
            else
            {
                List<Doctor> doctors = _doctorController.GetAllDoctors().ToList();
                List<Examination> listExaminations = new List<Examination>();
                bool priority = false; //prioritet je datum jer lekar nije izabran
                foreach (Doctor doctor in doctors)
                {
                    if (doctor.Type == (DoctorType)DoctorTypeSelected.SelectedIndex)
                    {
                        
                        //List<Examination> listExaminationsWithRooms = _doctorController.GetFreeGetFreeExaminations(doctor, startDate, endDate, priority);
                        //foreach (Examination exam in listExaminationsWithRooms)
                        //{
                        //    exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
                        //}
                        //listExaminations.AddRange(listExaminationsWithRooms);
                    }
                }

                ExamsAvailable.ItemsSource = listExaminations;
            }
            
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Model.Patient patient = _patientController.ReadPatient(Login.loggedId);
            if (ExamsAvailable.SelectedIndex != -1)
            {

                Doctor doctor = (Doctor)DoctorCombo.SelectedItem;
                Examination selectedExamination = (Examination)ExamsAvailable.SelectedItem;
                if (doctor == null)
                {
                    doctor = _doctorController.GetDoctor(selectedExamination.DoctorId);
                }
                DateTime dt = selectedExamination.Date;
                
                
                

                int id = 0;
                for(int i = 0; i < _examController.GetExaminations().Count; ++i)
                {
                    ++id;
                }
                ++id;
                Examination newExam = new Examination(null, dt, RandomString(6), 2, HospitalMain.Enums.ExaminationTypeEnum.OrdinaryExamination, patient.ID, doctor.Id);

                _examController.PatientCreateExam(newExam, dt);
                _examController.SaveExaminationRepo();
                ObservableCollection<Examination> examinations = _examController.ReadPatientExams(Login.loggedId);
                foreach (Examination exam in examinations)
                {
                    exam.DoctorNameSurname = _doctorController.GetDoctor(exam.DoctorId).NameSurname;
                }
                //ExaminationsList.Examinations = examinations;
                this.Close();
            }
            

        }

        private void ChangeType(object sender, SelectionChangedEventArgs e)
        {
            DoctorType selectedType = DoctorType.Pulmonology;
            switch (DoctorTypeSelected.SelectedIndex)
            {
                case 0:
                    selectedType = DoctorType.Pulmonology;
                    DoctorTypeSelected.SelectedIndex = 0;
                    break;
                case 1:
                    selectedType = DoctorType.General;
                    DoctorTypeSelected.SelectedIndex = 1;
                    break;
                case 2:
                    selectedType = DoctorType.Cardiology;
                    DoctorTypeSelected.SelectedIndex = 2;
                    break;
            }

            
            List<Doctor> doctors = _doctorController.GetAllDoctors().ToList();
            DoctorsObs = new ObservableCollection<Doctor>();
            foreach (Doctor doctor in doctors)
            {
                if(doctor.Type == selectedType)
                {
                    DoctorsObs.Add(doctor);
                }
            }
            DoctorCombo.ItemsSource = DoctorsObs;
        }
    }
}
