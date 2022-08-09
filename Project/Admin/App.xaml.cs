﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Threading;

using Controller;
using Service;
using Repository;
using Utility;
using Model;
using Enums;
using HospitalMain.Enums;
using HospitalMain.Service;

namespace Admin
{
    public partial class App : Application
    {
        public RoomController roomController { get; set; }
        public EquipmentController equipmentController { get; set; }
        public EquipmentTransferController equipmentTransferController { get; set; }
        public RenovationController renovationController { get; set; }
        public UserAccountController userAccountController { get; set; }
        public MedicineController medicineController { get; set; }
        public DoctorController doctorController { get; set; }
        public AnswerController answerController { get; set; }

        public App()
        {
            var equipmentRepo = new EquipmentRepo(GlobalPaths.EquipmentDBPath);
            var roomRepo = new RoomRepo(GlobalPaths.RoomsDBPath, equipmentRepo);
            var equipmentTransferRepo = new EquipmentTransferRepo(GlobalPaths.EquipmentTransfersDBPath, roomRepo, equipmentRepo);
            var renovationRepo = new RenovationRepo(GlobalPaths.RenovationDBPath, roomRepo);
            var userAccountRepo = new UserAccountRepo(GlobalPaths.UserDBPath);
            var examinationRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            var medicineRepo = new MedicineRepo(GlobalPaths.MedicineDBPath);
            var doctorRepo = new DoctorRepo(GlobalPaths.DoctorsDBPath);
            var patientRepo = new PatientRepo(GlobalPaths.PatientsDBPath);
            var questionnaireRepo = new QuestionnaireRepo(GlobalPaths.QuestionnaireDBPath);
            var freeDaysRequestsRepo = new FreeDaysRequestRepo(GlobalPaths.RequestDBPath);
            
            var roomService = new RoomService(roomRepo);
            var equipmentService = new EquipmentService(equipmentRepo, roomRepo);
            var equipmentTransferService = new EquipmentTransferService(equipmentTransferRepo, roomRepo, equipmentRepo, examinationRepo);
            var renovationService = new RenovationService(renovationRepo, roomRepo, examinationRepo);
            var userAccountService = new UserAccountService(userAccountRepo);
            var medicineService = new MedicineService(medicineRepo);
            var doctorService = new DoctorService(doctorRepo, examinationRepo, roomRepo, patientRepo);
            var emergencyService = new EmergencyService(examinationRepo, doctorRepo);
            var freeDaysRequestsService = new FreeDaysRequestService(freeDaysRequestsRepo, doctorRepo);
            var patientService = new PatientService(patientRepo, examinationRepo, doctorRepo, roomRepo, questionnaireRepo, freeDaysRequestsService);
            var answerService = new AnswerService(patientService, doctorService);

            roomController = new RoomController(roomService);
            equipmentController = new EquipmentController(equipmentService);
            equipmentTransferController = new EquipmentTransferController(equipmentTransferService);
            renovationController = new RenovationController(renovationService);
            userAccountController = new UserAccountController(userAccountService);
            medicineController = new MedicineController(medicineService);
            doctorController = new DoctorController(doctorService, emergencyService);
            answerController = new AnswerController(answerService);

            var finishRenovations = new Timer(state => renovationController.FinishRenovation(), null, 0, 6000);
            
            if(File.Exists(GlobalPaths.EquipmentDBPath))
                equipmentController.LoadEquipment();
            
            if (File.Exists(GlobalPaths.RoomsDBPath))
                roomController.LoadRoom();

            if (File.Exists(GlobalPaths.EquipmentTransfersDBPath))
                equipmentTransferController.LoadEquipmentTransfer();

            if(File.Exists(GlobalPaths.RenovationDBPath))
                renovationController.LoadRenovation();

            if(File.Exists(GlobalPaths.MedicineDBPath))
                medicineController.LoadMedicine();

            int f = 0;
            for(int i = 1; i < 40; i++)
            {
                Room r = new Room(i.ToString(), f + 1, 10 * f + i % 10, false, (RoomTypeEnum)(i % 5), (RoomTypeEnum)(i % 5 + 1));
                Equipment e = new Equipment(i.ToString(), i.ToString(), (EquipmentTypeEnum)(i % 10));

                ObservableCollection<IngredientEnum> ingredients = new ObservableCollection<IngredientEnum>();
                for(int j = 0; j < 3; j++)
                {
                    ingredients.Add((IngredientEnum)((i + j) % 5));
                }
                MedicineTypeEnum t = (MedicineTypeEnum)(i % 8);
                Medicine m = new Medicine(medicineController.GenerateID(), MedicineController.GenerateName(t), t, ingredients, StatusEnum.Pending, i.ToString(), DateTime.Now, "");

                roomController.CreateRoom(r);
                equipmentController.CreateEquipment(e);
                roomController.AddEquipment(r.Id, e);
                medicineController.NewMedicine(m);

                f = (i % 10) == 0 ? ++f : f;
            }
        }

        public static Window GetSpecificWindowType<T>()
        {
            // return first instance of type T of window
            foreach(Window window in App.Current.Windows)
                if(window is T)
                    return window;
            return null;
        }

        public static void HideAllWindows()
        {
            foreach (Window window in App.Current.Windows)
                window.Hide();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //equipmentController.SaveEquipment();
            //roomController.SaveRoom();
            //equipmentTransferController.SaveEquipmentTransfer();
            //renovationController.SaveRenovation();
            //medicineController.SaveMedicine();
        }
    }
}
