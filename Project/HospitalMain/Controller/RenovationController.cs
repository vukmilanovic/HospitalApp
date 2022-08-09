﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Service;
using Model;
using HospitalMain.Enums;

namespace Controller
{
    public class RenovationController
    {
        private readonly RenovationService _renovationService;

        public RenovationController(RenovationService renovationService)
        {
            _renovationService = renovationService;
        }

        public bool ScheduleRenovation(Renovation renovation)
        {
            return _renovationService.ScheduleRenovation(renovation);
        }

        public bool RecordRenovation(String renovationId)
        {
            return _renovationService.RecordRenovation(renovationId);
        }
        public bool OccupiedAtTheTime(Renovation renovation)
        {
            return _renovationService.OccupiedAtTheTime(renovation);
        }

        public void FinishRenovation()
        {
            _renovationService.FinishRenovation();
        }

        public void MergeRooms(Renovation renovation)
        {
            _renovationService.MergeRooms(renovation);
        }

        public void SplitRoom(Renovation renovation)
        {
            _renovationService.SplitRoom(renovation);
        }

        public bool DeleteRenovation(String renovationId)
        {
            return _renovationService.DeleteRenovation(renovationId);
        }

        public bool SetClipboardRenovation(Renovation renovation)
        {
            return _renovationService.SetClipboardRenovation(renovation);
        }

        public Renovation GetClipboardRenovation()
        {
            return _renovationService.GetClipboardRenovation();
        }

        public ObservableCollection<Renovation> ReadAll()
        {
            return _renovationService.ReadAll();
        }
        public String GenerateID()
        {
            return _renovationService.GenerateID();
        }

        public ObservableCollection<Renovation> QueryRenovations(String query)
        {
            return _renovationService.QueryRenovations(query);
        }

        public bool LoadRenovation()
        {
            return _renovationService.LoadRenovation();
        }

        public bool SaveRenovation()
        {
            return _renovationService.SaveRenovation();
        }
    }
}
