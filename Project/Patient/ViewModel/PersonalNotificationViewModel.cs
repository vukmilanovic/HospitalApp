﻿using HospitalMain.Controller;
using HospitalMain.Model;
using HospitalMain.Repository;
using Patient.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    public class PersonalNotificationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private PersonalNotificationController _personalNotificationController;

        private String text;
        private int hours;
        private int minutes;
        private List<String> days;
        private int monday;
        private int tuesday;
        private int wednesday;
        private int thursday;
        private int friday;
        private int saturday;
        private int sunday;

        private bool mondayChecked;
        private bool tuesdayChecked;
        private bool wednesdayChecked;
        private bool thursdayChecked;
        private bool fridayChecked;
        private bool saturdayChecked;
        private bool sundayChecked;

        public MyICommand AddPersonalNotificationCommand { get; set; }
        public MyICommand SelectionChangedMondayCommand { get; set; }

        private Window thisWindow;

        public String Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public int Hours
        {
            get
            {
                return hours;
            }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public int Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        public int Monday
        {
            get
            {
                return monday;
            }
            set
            {
                monday = value;
                OnPropertyChanged("Monday");
            }
        }

        public int Tuesday
        {
            get
            {
                return tuesday;
            }
            set
            {
                tuesday = value;
                OnPropertyChanged("Tuesday");
            }
        }

        public int Wednesday
        {
            get
            {
                return wednesday;
            }
            set
            {
                wednesday = value;
                OnPropertyChanged("Wednesday");
            }
        }

        public int Thursday
        {
            get
            {
                return thursday;
            }
            set
            {
                thursday = value;
                OnPropertyChanged("Thursday");
            }
        }

        public int Friday
        {
            get
            {
                return friday;
            }
            set
            {
                friday = value;
                OnPropertyChanged("Friday");
            }
        }

        public int Saturday
        {
            get
            {
                return saturday;
            }
            set
            {
                saturday = value;
                OnPropertyChanged("Saturday");
            }
        }

        public int Sunday
        {
            get
            {
                return sunday;
            }
            set
            {
                sunday = value;
                OnPropertyChanged("Sunday");
            }
        }

        public bool MondayChecked
        {
            get
            {
                return mondayChecked;
            }
            set
            {
                if(mondayChecked == value) return;
                mondayChecked = value;
                OnPropertyChanged("MondayChecked");
            }
        }

        public bool TuesdayChecked
        {
            get
            {
                return tuesdayChecked;
            }
            set
            {
                if (tuesdayChecked == value) return;
                tuesdayChecked = value;
                OnPropertyChanged("TuesdayChecked");
            }
        }

        public bool WednesdayChecked
        {
            get
            {
                return wednesdayChecked;
            }
            set
            {
                if (wednesdayChecked == value) return;
                wednesdayChecked = value;
                OnPropertyChanged("WednesdayChecked");
            }
        }

        public bool ThursdayChecked
        {
            get
            {
                return thursdayChecked;
            }
            set
            {
                if (thursdayChecked == value) return;
                thursdayChecked = value;
                OnPropertyChanged("ThursdayChecked");
            }
        }

        public bool FridayChecked
        {
            get
            {
                return fridayChecked;
            }
            set
            {
                if (fridayChecked == value) return;
                fridayChecked = value;
                OnPropertyChanged("FridayChecked");
            }
        }

        public bool SaturdayChecked
        {
            get
            {
                return saturdayChecked;
            }
            set
            {
                if (saturdayChecked == value) return;
                saturdayChecked = value;
                OnPropertyChanged("SaturdayChecked");
            }
        }

        public bool SundayChecked
        {
            get
            {
                return sundayChecked;
            }
            set
            {
                if (sundayChecked == value) return;
                sundayChecked = value;
                OnPropertyChanged("SundayChecked");
            }
        }

        public PersonalNotificationViewModel(Window window)
        {
            App app = Application.Current as App;
            _personalNotificationController = app.personalNotificationController;

            AddPersonalNotificationCommand = new MyICommand(OnAddPersonalNotification);
            SelectionChangedMondayCommand = new MyICommand(OnSelectionChangedMonday);


            Hours = 0;
            Minutes = 0;

            //Monday = -1;
            //Tuesday = -1;
            //Wednesday = -1;
            //Thursday = -1;
            //Friday = -1;
            //Saturday = -1;
            //Sunday = -1;

            //mondayChecked = true;
            //tuesdayChecked = true;
            //wednesdayChecked = true;
            //thursdayChecked = true;
            //fridayChecked = true;
            //saturdayChecked = true;
            //sundayChecked = true;

            Text = "Naslov alarma";
            thisWindow = window;
        }

        public void OnAddPersonalNotification()
        {
            List<int> selectedDays = new List<int>();
            //if (Monday == 0)
            //{
            //    selectedDays.Add(1);
            //}
            //if (Tuesday == 0)
            //{
            //    selectedDays.Add(2);
            //}
            //if (Wednesday == 0)
            //{
            //    selectedDays.Add(3);
            //}
            //if (Thursday == 0)
            //{
            //    selectedDays.Add(4);
            //}
            //if (Friday == 0)
            //{
            //    selectedDays.Add(5);
            //}
            //if (Saturday == 0)
            //{
            //    selectedDays.Add(6);
            //}
            //if (Sunday == 0)
            //{
            //    selectedDays.Add(0);
            //}
            //checkbox
            if (MondayChecked == true)
            {
                selectedDays.Add(1);
            }
            if (TuesdayChecked == true)
            {
                selectedDays.Add(2);
            }
            if (WednesdayChecked == true)
            {
                selectedDays.Add(3);
            }
            if (ThursdayChecked == true)
            {
                selectedDays.Add(4);
            }
            if (FridayChecked == true)
            {
                selectedDays.Add(5);
            }
            if (SaturdayChecked == true)
            {
                selectedDays.Add(6);
            }
            if (SundayChecked == true)
            {
                selectedDays.Add(0);
            }
            PersonalNotification personalNotification = new PersonalNotification(Login.loggedId, Text, selectedDays, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hours, Minutes, 0));
            personalNotification.TimeString = personalNotification.Time.ToString("HH:mm");
            _personalNotificationController.AddPersonalNotification(personalNotification);
            thisWindow.Close();
        }

        public void OnSelectionChangedMonday()
        {
            if(Monday == -1)
            {
                Monday = 0;
            }
            else
            {
                Monday = 1;
            }
        }
    }
}
