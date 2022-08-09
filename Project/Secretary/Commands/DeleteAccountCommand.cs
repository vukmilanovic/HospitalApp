﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Secretary.ViewModel;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Secretary.Commands
{
    public class DeleteAccountCommand : CommandBase
    {
        private readonly PatientController _patientController;
        private readonly MedicalRecordController _medicalRecordController;
        private readonly ExamController _examController;
        private readonly CRUDAccountOptionsViewModel _cruDAccountOptionsViewModel;

        public DeleteAccountCommand(CRUDAccountOptionsViewModel cRUDAccountOptionsViewModel, PatientController patientController, MedicalRecordController medicalRecordController, ExamController examController)
        {
            _cruDAccountOptionsViewModel = cRUDAccountOptionsViewModel;
            _patientController = patientController;
            _examController = examController;
            _medicalRecordController = medicalRecordController;

            _cruDAccountOptionsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_cruDAccountOptionsViewModel.PatientViewModel == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _examController.DeletePatientExams(_cruDAccountOptionsViewModel.PatientViewModel.ID);
            _medicalRecordController.DeletePatientMedicalRecord(_cruDAccountOptionsViewModel.PatientViewModel.ID);
            _patientController.RemovePatient(_cruDAccountOptionsViewModel.PatientViewModel.ID);

            UpdatePatients();
        }

        private void UpdatePatients()
        {
            _cruDAccountOptionsViewModel.PatientList.Clear();
            ObservableCollection<Patient> patientsFromBase = _patientController.ReadAllPatients();

            foreach (Patient patient in patientsFromBase)
            {
                _cruDAccountOptionsViewModel.PatientList.Add(new PatientViewModel(patient));
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CRUDAccountOptionsViewModel.PatientViewModel))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
