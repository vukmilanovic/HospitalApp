﻿using Commands;
using Controller;
using Doctor;
using Doctor.View;
using Enums;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ViewModel
{
    public class FreeDaysRequestViewModel: ViewModelBase
    {
        public IEnumerable<FreeDaysReasons> Reasons
        {
            get
            {
                return Enum.GetValues(typeof(FreeDaysReasons)).Cast<FreeDaysReasons>();
            }
        }

        private string freeDaysLeft;
        private string startDate;
        private string endDate;
        private FreeDaysReasons reason;
        private FreeDaysReasons selectedItem;

        private readonly DoctorController _doctorController;
        private readonly FreeDaysRequestController _freeDaysRequestController;
        private readonly FreeDaysRequestRepo _freeDaysRequestRepo;
        private readonly DoctorRepo _doctorRepo;

        public MyICommand SendRequestCommand { get; set; }
        public MyICommand EquipmentCommand { get; set; }
        public MyICommand WorkHoursCommand { get; set; }
        public MyICommand ShowAll { get; set; }


        public FreeDaysReasons Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged("Reason");
            }
        }
        public FreeDaysReasons SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }


        public FreeDaysRequestViewModel()
        {
            var app = System.Windows.Application.Current as App;
            _doctorController = app.doctorController;
            _freeDaysRequestController = app.requestController;
            _freeDaysRequestRepo = app.requestRepo;
            _doctorRepo = app.doctorRepo;

            Model.Doctor doctor = _doctorController.GetDoctor(MainWindow._uid);
            FreeDaysLeft = doctor.FreeDaysLeft.ToString();

            SendRequestCommand = new MyICommand(OnSend, CanSend);
            EquipmentCommand = new MyICommand(OnEquipment);
            WorkHoursCommand = new MyICommand(OnWorkHours);
            ShowAll = new MyICommand(OnExecute);

        }

        public string FreeDaysLeft
        {
            get { return freeDaysLeft; }
            set
            {
                if (freeDaysLeft != value)
                {
                    freeDaysLeft = value;
                    OnPropertyChanged("FreeDaysLeft");
                }
            }
        }
        public string EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                    SendRequestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                    SendRequestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public bool CanSend()
        {
            if(endDate != null && startDate != null)
                return (DateTime.Parse(endDate) - DateTime.Parse(startDate)).TotalDays <= _doctorController.GetDoctor(MainWindow._uid).FreeDaysLeft
                    && DateTime.Parse(startDate) < DateTime.Parse(endDate)
                    && DateTime.Parse(startDate) > DateTime.Now 
                    && DateTime.Parse(endDate) > DateTime.Now;
            else
                return false;
        }
        public void OnSend()
        {
            FreeDaysRequest request = new FreeDaysRequest(_freeDaysRequestController.GenerateID().ToString(), StatusEnum.Pending, _doctorController.GetDoctor(MainWindow._uid).Id, DateTime.Parse(startDate), DateTime.Parse(endDate), selectedItem, "");
            _freeDaysRequestController.NewRequest(request);
            _freeDaysRequestRepo.SaveRequest();
            _doctorController.GetDoctor(MainWindow._uid).FreeDaysLeft -= (DateTime.Parse(endDate) - DateTime.Parse(startDate)).TotalDays;
            _doctorRepo.SaveDoctor();
        }


        public void OnExecute()
        {
            ShowAllRequests requestsPage = new ShowAllRequests();
            DoctorNavBar.navigation.Navigate(requestsPage);
        }
        public void OnEquipment()
        {
            EquipmentRequestPage requestPage = new EquipmentRequestPage();
            DoctorNavBar.navigation.Navigate(requestPage);
        }
        public void OnWorkHours()
        {
            WorkHoursRequest requestPage = new WorkHoursRequest();
            DoctorNavBar.navigation.Navigate(requestPage);
        }

    }
}
