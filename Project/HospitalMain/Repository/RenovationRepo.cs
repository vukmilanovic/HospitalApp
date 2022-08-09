﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

using Model;
using Utility;
using Repository;
using HospitalMain.Enums;

namespace Repository
{
    public class RenovationRepo
    {
        public String dbPath { get; set; }
        private RoomRepo _roomRepo;
        public ObservableCollection<Renovation> Renovations { get; set; }
        public Renovation clipboardRenovation { get; set; }

        public RenovationRepo(String db_path, RoomRepo roomRepo)
        {
            dbPath = db_path;
            _roomRepo = roomRepo;
            Renovations = new ObservableCollection<Renovation>();
        }

        public bool NewRenovation(Renovation renovation)
        {
            Renovations.Add(renovation);
            return true;
        }

        public Renovation GetRenovation(String renovationId)
        {
            foreach(Renovation renovation in Renovations)
                if(renovation.Id.Equals(renovationId))
                    return renovation;

            return null;
        }

        public void SetRenovation(Renovation newRenovation)
        {
            for(int i = 0; i < Renovations.Count; i++)
                if (Renovations[i].Id.Equals(newRenovation.Id))
                {
                    Renovations[i].OriginRoom = newRenovation.OriginRoom;
                    Renovations[i].DestinationRoom = newRenovation.DestinationRoom;
                    Renovations[i].Type = newRenovation.Type;
                    Renovations[i].StartDate = newRenovation.StartDate;
                    Renovations[i].EndDate = newRenovation.EndDate;
                    break;
                }
        }
        public bool DeleteRenovation(String renovationId)
        {
            foreach(Renovation renovation in Renovations)
                if (renovation.Id.Equals(renovationId))
                {
                    Renovations.Remove(renovation);
                    return true;
                }

            return false;
        }
        public bool SetClipboardRenovation(Renovation renovation)
        {
            clipboardRenovation = renovation;
            return true;
        }
        public Renovation GetClipboardRenovation()
        {
            return clipboardRenovation;
        }

        public String GenerateID()
        {
            int id = 0;
            if (Renovations.Count > 0)
                id = Renovations.Max(r => int.Parse(r.Id)) + 1;

            return id.ToString();
        }

        public bool LoadRenovation()
        {
            using FileStream fileStream = File.OpenRead(dbPath);

            List<RenovationAnnotation> renovationAnnotations = JsonSerializer.Deserialize<List<RenovationAnnotation>>(fileStream);

            foreach(RenovationAnnotation annotation in renovationAnnotations)
            {
                Renovation renovation = new Renovation(annotation);
                renovation.OriginRoom = _roomRepo.GetRoom(annotation.OriginRoomId);
                renovation.DestinationRoom = _roomRepo.GetRoom(annotation.DestinationRoomId);
                Renovations.Add(renovation);
            }

            return true;
        }
        public bool SaveRenovation()
        {
            List<RenovationAnnotation> renovationAnnotations = new List<RenovationAnnotation>();
            foreach(Renovation renovation in Renovations)
                renovationAnnotations.Add(new RenovationAnnotation(renovation));

            String jsonString = JsonSerializer.Serialize(renovationAnnotations);
            File.WriteAllText(dbPath, jsonString);

            return true;
        }
    }
}
