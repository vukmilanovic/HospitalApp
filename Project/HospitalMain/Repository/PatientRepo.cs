using System;
using System.Collections.ObjectModel;
using Model;
using Service;
using System.IO;
using System.Text.Json;
using HospitalMain.Enums;
using System.Collections.Generic;
using HospitalMain.Model;
using System.Linq;

namespace Repository
{
   public class PatientRepo
   {
      public String DBPath { get; set; }
      public ObservableCollection<Patient> Patients { get; set; }
      
      public PatientRepo(String dbPath)
      {

            this.DBPath = dbPath;
            this.Patients = new ObservableCollection<Patient>();

            Patient p1 = new Patient("1", "0605994802463", "Pera", "Peric", "0605235548", "pera@mail.com", "Partizanska 13, Novi Sad", Gender.Muški, new DateTime(1994, 05, 06), "1", false, new List<Answer>(), DateTime.Now.ToString("MM"), 0, 0);
            Patient p2 = new Patient("2", "0808985802463", "Ivan", "Ivic", "0605234548", "ivan@mail.com", "Partizanska 14, Novi Sad", Gender.Muški, new DateTime(1985, 08, 08), "2", false, new List<Answer>(), DateTime.Now.ToString("MM"), 0, 0);
            Patient p3 = new Patient("3", "1111001802463", "Zika", "Zikic", "0605235598", "zika@mail.com", "Partizanska 15, Novi Sad", Gender.Muški, new DateTime(2001, 11, 11), "3", false, new List<Answer>(), DateTime.Now.ToString("MM"), 0, 0);
            Patient p4 = new Patient("4", "", "John", "Doe", "", "", "", Gender.Muški, new DateTime(), "", true, new List<Answer>(), DateTime.Now.ToString("MM"), 0, 0);

            this.Patients.Add(p1);
            this.Patients.Add(p2);
            this.Patients.Add(p3);
            this.Patients.Add(p4);

            //SavePatient();
            if (File.Exists(dbPath))
                LoadPatient();
        }

      public bool NewPatient(Patient patient)
      {

         foreach (Patient _patient in Patients)
            {
                if (_patient.ID.Equals(patient.ID))
                {
                    return false;
                }
            }

         Patients.Add(patient);
         SavePatient();
         return true;
      }
      
      public Patient GetPatient(String patientId)
      {
            foreach (Patient patient in Patients)
            {
                if (patient.ID.Equals(patientId))
                {
                    return patient;
                }
            }

            return null;
      }
      
      public void SetPaetient(String patientId, Patient newPatient)
      {
         for (int i = 0; i < Patients.Count; i++)
            {
                if (Patients[i].ID.Equals(patientId))
                {
                    Patients[i].ID = newPatient.ID;
                    Patients[i].UCIN = newPatient.UCIN;
                    Patients[i].Name = newPatient.Name;
                    Patients[i].Surname = newPatient.Surname;
                    Patients[i].PhoneNumber = newPatient.PhoneNumber;
                    Patients[i].Mail = newPatient.Mail;
                    Patients[i].Adress = newPatient.Adress;
                    Patients[i].Gender = newPatient.Gender;
                    Patients[i].DoB = newPatient.DoB;
                    Patients[i].MedicalRecordID = newPatient.MedicalRecordID;
                    Patients[i].IsGuest = newPatient.IsGuest;
                    break;
                }
            }
            SavePatient();
        }
      
      public bool DeletePatient(String patientId)
      {
         for (int i = 0; i < Patients.Count; i++)
            {
                if (Patients[i].ID.Equals(patientId))
                {
                    Patients.RemoveAt(i);
                    SavePatient();
                    return true;
                }
            }

            SavePatient();
            return false;
      }
      
      public bool LoadPatient()
      {
            using FileStream fileStream = File.OpenRead(DBPath);
            Patients = JsonSerializer.Deserialize<ObservableCollection<Patient>>(fileStream);
            return true;
      }
      
      public bool SavePatient()
      {
            string jsonString = JsonSerializer.Serialize(Patients);
            File.WriteAllText(DBPath, jsonString);
            return true;
      }

    }

   }
