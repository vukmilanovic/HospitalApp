﻿using Controller;
using Model;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class CRUDMedicalRecordViewModel : ViewModelBase
    {
        private readonly MedicalRecordController _medicalRecordController;
        private ObservableCollection<MedicalRecordViewModel> _medicalRecords;
        private ICollectionView _dataGridCollection;
        private String _filter;

        public ObservableCollection<MedicalRecordViewModel> MedicalRecords => _medicalRecords;

        private MedicalRecordViewModel _medicalRecordViewModel;
        public MedicalRecordViewModel MedicalRecordViewModel
        {
            get { return _medicalRecordViewModel; }
            set { _medicalRecordViewModel = value; OnPropertyChanged(nameof(MedicalRecordViewModel)); }
        }

        public ICollectionView DataGridCollection
        {
            get { return _dataGridCollection; }
            set { _dataGridCollection = value; OnPropertyChanged(nameof(DataGridCollection)); }
        }

        public String Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged(nameof(Filter)); DataGridCollection.Filter = FilterByNameSurnameOrID; }
        }

        public ICommand AddMedicalRecordCommand { get; }
        public ICommand RemoveMedicalRecordCommand { get; }
        public ICommand EditMedicalRecordCommand { get; }

        private bool FilterByNameSurnameOrID(object pat)
        {
            if (!string.IsNullOrEmpty(Filter))
            {
                var data = pat as MedicalRecordViewModel;
                return data != null && (data.Name.ToLower().Contains(Filter.ToLower()) || data.Surname.ToLower().Contains(Filter.ToLower()) || data.ID.Contains(Filter));
            }
            return true;
        }

        public CRUDMedicalRecordViewModel(MedicalRecordsViewModel medicalRecordsViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;

            _medicalRecords = new ObservableCollection<MedicalRecordViewModel>();
            DataGridCollection = CollectionViewSource.GetDefaultView(MedicalRecords);

            AddMedicalRecordCommand = new GoToAddMedicalRecordCommand(this, medicalRecordsViewModel);
            EditMedicalRecordCommand = new  GoToEditMedicalRecordCommand(this, medicalRecordsViewModel);
            RemoveMedicalRecordCommand = new DeleteMedicalRecordCommand(this, _medicalRecordController);

            ObservableCollection<MedicalRecord> medicalRecordsFromBase = _medicalRecordController.GetAllMedicalRecords();
            foreach(MedicalRecord mr in medicalRecordsFromBase)
            {
                _medicalRecords.Add(new MedicalRecordViewModel(mr));
            }
        }

    }
}
