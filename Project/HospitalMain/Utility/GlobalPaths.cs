﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utility
{
    public class GlobalPaths
    {
        public static String DBPath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\HospitalMain\Database");
        public static String RoomsDBPath = Path.Combine(DBPath, "Rooms.json");
        public static String EquipmentDBPath = Path.Combine(DBPath, "Equipment.json");
        public static String EquipmentTransfersDBPath = Path.Combine(DBPath, "EquipmentTransfers.json");
        public static String RenovationDBPath = Path.Combine(DBPath, "Renovations.json");
        public static String ExamsDBPath = Path.Combine(DBPath, "Exams.json");
        public static String PatientsDBPath = Path.Combine(DBPath, "Patients.json");
        public static String DoctorsDBPath = Path.Combine(DBPath, "Doctors.json");
        public static String MedicalRecordDBPath = Path.Combine(DBPath, "MedicalRecords.json");
        public static String MedicineDBPath = Path.Combine(DBPath, "Medicines.json");
        public static String TherapyDBPath = Path.Combine(DBPath, "Therapy.json");
        public static String ReportDBPath = Path.Combine(DBPath, "Reports.json");
        public static String UserDBPath = Path.Combine(DBPath, "Users.json");
        public static String DynamicEquipmentDBPath = Path.Combine(DBPath, "DynamicEquipment.json");
        public static String QuestionnaireDBPath = Path.Combine(DBPath, "Questionnaires.json");
        public static String RequestDBPath = Path.Combine(DBPath, "Request.json");
        public static String ReferralDBPath = Path.Combine(DBPath, "Referrals.json");
        public static String MeetingsDBPath = Path.Combine(DBPath, "Meetings.json");
        public static String PersonalNotificationDBPath = Path.Combine(DBPath, "PersonalNotifications.json");

    }
}
