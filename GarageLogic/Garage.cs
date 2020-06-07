using System;
using System.Collections.Generic;
using Ex03.GarageLogicVehicle;
using Ex03.GarageLogicVehicleFactory;
using Ex03.GarageLogicVehicleParts;
using Ex03.GarageLogicVehicleRegestration;
using System.Text;

namespace Ex03.GarageLogicGarageManagement
{
    public class Garage
    {
        readonly VehicleFactory r_VehicleFactory;
        readonly Dictionary<string, VehicleInGarage> r_VehicleDictionary;


        public Garage()
        {
            r_VehicleFactory = new VehicleFactory();
            r_VehicleDictionary = new Dictionary<string, VehicleInGarage>();
        }

        public void InsertNewVehicle(Dictionary<string,string> i_VehicleData)
        {

            bool isVehicleAlreadyInGarage = false;
            string licenseNumber = i_VehicleData["License Number"];
            VehicleInGarage vehicleInGarage;

            isVehicleAlreadyInGarage = r_VehicleDictionary.TryGetValue(licenseNumber, out vehicleInGarage);

            if (isVehicleAlreadyInGarage) 
            {
                vehicleInGarage.Status = "InRepair";
            }
            else
            {
                Vehicle newVehicle = r_VehicleFactory.createNewVehicle(i_VehicleData);
                RegisterNewVehicle(newVehicle, i_VehicleData);
            }
        }

        public bool isLicenseNumberExists(string i_LicenseNumber)
        {
            return r_VehicleDictionary.ContainsKey(i_LicenseNumber);
        }

        public bool isValidCurrentAirPressure(float i_CurrentAirPressure, string i_VehicleType)
        {
            bool isValidAirPressure = r_VehicleFactory.isValidAitPressure(i_CurrentAirPressure, i_VehicleType);
            return isValidAirPressure;
        }

        public string[] getListOfVehiclesTypes()
        {
            return this.r_VehicleFactory.getListOfVehicleTypes();
        }


        private void RegisterNewVehicle(Vehicle i_NewVehicle, Dictionary<String, String> i_VehicleData)
        {

            string ownerName = i_VehicleData["Owner Name"];
            string ownerPhoneNumber = i_VehicleData["Owner Phone Number"];
            VehicleInGarage newVehicleInGarage = new VehicleInGarage(i_NewVehicle, ownerName, ownerPhoneNumber);
        }

        public string getVehicleList(string status)
        {

            StringBuilder VehiclesListToPrint = new StringBuilder();

            foreach (VehicleInGarage registeredVehicle in r_VehicleDictionary.Values)
            {
                if(status.Equals("All") || registeredVehicle.Status.ToLower().Equals(status))
                {
                    VehiclesListToPrint.Append(registeredVehicle.Vehicle.LicensId + Environment.NewLine);
                }
            }

            return VehiclesListToPrint.ToString();
        }

        public void changeVehicleStatus(string i_LicenseNumber, string newStatus)
        {
            VehicleInGarage vehicleInGarage = null;
            r_VehicleDictionary.TryGetValue(i_LicenseNumber, out vehicleInGarage);

            if(vehicleInGarage != null)
            {
                vehicleInGarage.Status = newStatus;
            }
        }

        public void addAirToVehiclewheels(string i_LicenseNumber, float i_AirToAdd)
        {
            VehicleInGarage vehicleInGarage = null;
            r_VehicleDictionary.TryGetValue(i_LicenseNumber, out vehicleInGarage);

            if (vehicleInGarage != null)
            {
                vehicleInGarage.RegisterdVehicle.pumpAir(i_AirToAdd);
            }
        }

        //implement polymorpizem
        public void AddEnergyToVehicle(string i_LicenseNumber,string i_FuelType, float i_AmoutOfFuelToAdd)
        {
            VehicleInGarage vehicleInGarage = null;
            r_VehicleDictionary.TryGetValue(i_LicenseNumber, out vehicleInGarage);

            if (vehicleInGarage != null)
            {
             
            }
        }

        //improve method to include energy data
        public string getFullDataOnVehicle(string i_LicenseNumber)
        {
            VehicleInGarage vehicleInGarage = null;
            r_VehicleDictionary.TryGetValue(i_LicenseNumber, out vehicleInGarage);
            string result = string.Empty; ;

            if (vehicleInGarage != null)
            {
                result = vehicleInGarage.getVehicleDataAsString();
            }

            return result;
        }

        public string getListOfFuelTypes()
        {
            return FuelSystem.getListOfFuelTypes();
        }

    }
}
