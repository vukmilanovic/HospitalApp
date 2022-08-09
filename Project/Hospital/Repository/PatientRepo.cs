using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class PatientRepo
    {
        private String dbPath;
        private List<Patient> patients;

        public PatientRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.patients = new List<Patient>();

            Patient p1 = new Patient("idPatient1", "namePatient1", "surnamePatient1", new DateTime(2000, 11, 1), new List<Examination>());
            this.patients.Add(p1);
        }

        public PatientRepo(string dbPath, List<Patient> listPatient)
        {
            this.dbPath = dbPath;
            this.patients = new List<Patient>();

            Patient p1 = new Patient("1234567", "Jelena", "Dinic", new DateTime(2000, 11, 1), new List<Examination>());
            this.patients.Add(p1);
        }


        public bool NewPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatient(String patientId)
        {
            foreach (Patient patient in patients)
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
            throw new NotImplementedException();
        }

        public bool DeletePatient(String patientId)
        {
            throw new NotImplementedException();
        }

        public bool LoadPatient()
        {
            throw new NotImplementedException();
        }

        public bool SavePatient()
        {
            throw new NotImplementedException();
        }

        //public PatientAccountService patientAccountService;
        public List<Model.Patient> GetAllPatients()
        {
            return patients;
        }

    }
}