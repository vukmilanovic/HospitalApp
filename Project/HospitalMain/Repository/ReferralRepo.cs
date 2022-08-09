﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class ReferralRepo
    {
        public string DBPath { get; set; }
        public ObservableCollection<Referral> Referrals { get; set; }
        public ReferralRepo(string dbPath)
        {
            DBPath = dbPath;
            Referrals = new ObservableCollection<Referral>();

            Referral ref1 = new Referral("d14", "1", "idRef1", DoctorType.Pulmonology, DateTime.Now);
            Referral ref2 = new Referral("d12", "1", "idRef1", DoctorType.Pulmonology, DateTime.Now);
            Referrals.Add(ref1);
            Referrals.Add(ref2);

            if (File.Exists(dbPath))
                LoadReferral();
            //SaveReferral();

        }
        public bool NewReferral(Referral referral)
        {
            foreach (Referral _request in Referrals)
            {
                if (_request.DoctorId.Equals(referral.DoctorId))
                {
                    return false;
                }
            }
            Referrals.Add(referral);
            SaveReferral();
            return true;
        }
        public bool LoadReferral()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            Referrals = JsonSerializer.Deserialize<ObservableCollection<Referral>>(fileStream);
            return true;
        }

        public bool SaveReferral()
        {
            string jsonString = JsonSerializer.Serialize(Referrals);
            File.WriteAllText(DBPath, jsonString);
            return true;
        }

        
    }
}
