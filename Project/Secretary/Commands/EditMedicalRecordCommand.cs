﻿using Controller;
using HospitalMain.Enums;
using Model;
using Secretary.ViewModel;
using Secretary.ViewUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Secretary.Commands
{
    public class EditMedicalRecordCommand : CommandBase
    {
        private readonly MedicalRecordController _medicalRecordController;
        private readonly CRUDMedicalRecordViewModel _cRUDMedicalRecordViewModel;
        private readonly EditMedicalRecordViewModel _editMedicalRecordViewModel;
        private readonly MedicalRecordsViewModel _medicalRecordsViewModel;

        public EditMedicalRecordCommand(EditMedicalRecordViewModel editMedicalRecordViewModel, CRUDMedicalRecordViewModel cRUDMedicalRecordViewModel, MedicalRecordController medicalRecordController, MedicalRecordsViewModel medicalRecordsViewModel)
        {
            _medicalRecordController = medicalRecordController;
            _editMedicalRecordViewModel = editMedicalRecordViewModel;
            _cRUDMedicalRecordViewModel = cRUDMedicalRecordViewModel;
            _medicalRecordsViewModel = medicalRecordsViewModel;

            _editMedicalRecordViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {

            foreach(SelectableItemWrapper<Allergens> allergen in _editMedicalRecordViewModel.AllergensListBoxData)
            {
                //ako je selektovan alergen
                if (allergen.IsSelected)
                {
                    _editMedicalRecordViewModel.Allergens.Add(allergen.Item);
                }
            }

            if(parameter.ToString() == "Edit")
            {
                _medicalRecordsViewModel.CurrentCRUDMedRecView = new CRUDMedicalRecordViewModel(_medicalRecordsViewModel);
            }

            //izmena kartona
            _medicalRecordController.EditMedicalRecord(_editMedicalRecordViewModel.ID, _editMedicalRecordViewModel.UCIN, _editMedicalRecordViewModel.Name, _editMedicalRecordViewModel.Surname, _editMedicalRecordViewModel.PhoneNumber, _editMedicalRecordViewModel.Mail, _editMedicalRecordViewModel.Adress, _editMedicalRecordViewModel.Gender, _editMedicalRecordViewModel.DateOfBirth, _editMedicalRecordViewModel.BloodType, _editMedicalRecordViewModel.Reports, _editMedicalRecordViewModel.Allergens, new ObservableCollection<HospitalMain.Model.Notification>());

            //update kartona
            UpdateMedicalRecords();

            //zatvaranje prozora
            //_editMedicalRecord.Close();
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_editMedicalRecordViewModel.UCIN) && !string.IsNullOrEmpty(_editMedicalRecordViewModel.Name) && !string.IsNullOrEmpty(_editMedicalRecordViewModel.Surname) && !string.IsNullOrEmpty(_editMedicalRecordViewModel.Adress) && !string.IsNullOrEmpty(_editMedicalRecordViewModel.Mail) && !string.IsNullOrEmpty(_editMedicalRecordViewModel.PhoneNumber) && base.CanExecute(parameter);
        }

        private void UpdateMedicalRecords()
        {
            _cRUDMedicalRecordViewModel.MedicalRecords.Clear();
            ObservableCollection<MedicalRecord> medicalRecordsFromBase = _medicalRecordController.GetAllMedicalRecords();

            foreach (MedicalRecord medicalRecord in medicalRecordsFromBase)
            {
                _cRUDMedicalRecordViewModel.MedicalRecords.Add(new MedicalRecordViewModel(medicalRecord));
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(EditMedicalRecordViewModel.UCIN) || e.PropertyName == nameof(EditMedicalRecordViewModel.Name) || e.PropertyName == nameof(EditMedicalRecordViewModel.Surname) || e.PropertyName == nameof(EditMedicalRecordViewModel.Mail) || e.PropertyName == nameof(EditMedicalRecordViewModel.Adress) || e.PropertyName == nameof(EditMedicalRecordViewModel.PhoneNumber))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
