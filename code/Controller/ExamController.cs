using System;

namespace Controller
{
   public class ExamController
   {
      public bool PatientCreateExam(Model.Patient patient, DateTime date, Enumerate examType)
      {
         throw new NotImplementedException();
      }
      
      public bool DoctorCreateExam(Model.Patient patient, Doctor doctor, Model.Room examRoom, DateTime date)
      {
         throw new NotImplementedException();
      }
      
      public bool RemoveExam()
      {
         throw new NotImplementedException();
      }
      
      public List<Examination> ReadPatientExams()
      {
         throw new NotImplementedException();
      }
      
      public void DoctorEditExam()
      {
         throw new NotImplementedException();
      }
      
      public void PatientEditExam()
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.ArrayList doctorService;
      
      
      public System.Collections.ArrayList DoctorService
      {
         get
         {
            if (doctorService == null)
               doctorService = new System.Collections.ArrayList();
            return doctorService;
         }
         set
         {
            RemoveAllDoctorService();
            if (value != null)
            {
               foreach (Service.DoctorService oDoctorService in value)
                  AddDoctorService(oDoctorService);
            }
         }
      }
      
      
      public void AddDoctorService(Service.DoctorService newDoctorService)
      {
         if (newDoctorService == null)
            return;
         if (this.doctorService == null)
            this.doctorService = new System.Collections.ArrayList();
         if (!this.doctorService.Contains(newDoctorService))
            this.doctorService.Add(newDoctorService);
      }
      
      
      public void RemoveDoctorService(Service.DoctorService oldDoctorService)
      {
         if (oldDoctorService == null)
            return;
         if (this.doctorService != null)
            if (this.doctorService.Contains(oldDoctorService))
               this.doctorService.Remove(oldDoctorService);
      }
      
      
      public void RemoveAllDoctorService()
      {
         if (doctorService != null)
            doctorService.Clear();
      }
      public System.Collections.ArrayList patientService;
      
      
      public System.Collections.ArrayList PatientService
      {
         get
         {
            if (patientService == null)
               patientService = new System.Collections.ArrayList();
            return patientService;
         }
         set
         {
            RemoveAllPatientService();
            if (value != null)
            {
               foreach (Service.PatientService oPatientService in value)
                  AddPatientService(oPatientService);
            }
         }
      }
      
      
      public void AddPatientService(Service.PatientService newPatientService)
      {
         if (newPatientService == null)
            return;
         if (this.patientService == null)
            this.patientService = new System.Collections.ArrayList();
         if (!this.patientService.Contains(newPatientService))
            this.patientService.Add(newPatientService);
      }
      
      
      public void RemovePatientService(Service.PatientService oldPatientService)
      {
         if (oldPatientService == null)
            return;
         if (this.patientService != null)
            if (this.patientService.Contains(oldPatientService))
               this.patientService.Remove(oldPatientService);
      }
      
      
      public void RemoveAllPatientService()
      {
         if (patientService != null)
            patientService.Clear();
      }
   
   }
}