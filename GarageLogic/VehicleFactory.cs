using System;
using System.Collections.Generic;
using Ex03.GarageLogicVehicleParts;
using Ex03.GarageLogicVehicle;
using Ex03.GarageLogicExceptions;
using System.Text;

namespace Ex03.GarageLogicVehicleFactory
{

    class VehicleFactory
    {
        private const int k_CarMaxAirPressure = 32;
        private const int k_BikeMaxAirPressure = 30;
        private const int k_TruckAirPressure = 28;
        private const int k_CarTankSize = 60;
        private const int k_BikeTankSize = 7;
        private const int k_TruckTankSize = 120;
        private const float k_CarMaxBatteryTime = (float)2.1;
        private const float k_BikeMaxBatteryTime = (float)1.2;

        internal Vehicle createNewVehicle(Dictionary<string, string> i_VehicleData)
        {

            Vehicle newVehicle = null;
            eVehicleType vehicleType = (eVehicleType) Enum.Parse(typeof(eVehicleType), i_VehicleData["Vehicle Type"]);
            VehicleProperties vehicleProperties = new VehicleProperties(i_VehicleData, vehicleType.ToString());
            


            switch (vehicleType)
            {
                case eVehicleType.ElectricCar:
                    validateEnergyLevel(vehicleProperties.EnergySystem.MaxEnergyCapacity, k_CarMaxBatteryTime);
                    vehicleProperties.EnergySystem.MaxEnergyCapacity = k_CarMaxBatteryTime;
                    newVehicle = new Car(vehicleProperties);
                    break;
                case eVehicleType.FuelCar:
                    validateEnergyLevel(vehicleProperties.EnergySystem.MaxEnergyCapacity, k_CarMaxBatteryTime);
                    vehicleProperties.EnergySystem.MaxEnergyCapacity = k_CarTankSize;
                    newVehicle = new Car(vehicleProperties);
                    break;
                case eVehicleType.ElectricBike:
                    validateEnergyLevel(vehicleProperties.EnergySystem.MaxEnergyCapacity, k_CarMaxBatteryTime);
                    vehicleProperties.EnergySystem.MaxEnergyCapacity = k_BikeMaxBatteryTime;
                    newVehicle = new Bike(vehicleProperties);
                    break;
                case eVehicleType.FuelBike:
                    validateEnergyLevel(vehicleProperties.EnergySystem.MaxEnergyCapacity, k_CarMaxBatteryTime);
                    vehicleProperties.EnergySystem.MaxEnergyCapacity = k_BikeTankSize;
                    newVehicle = new Bike(vehicleProperties);
                    break;
                case eVehicleType.Truck:
                    validateEnergyLevel(vehicleProperties.EnergySystem.MaxEnergyCapacity, k_CarMaxBatteryTime);
                    vehicleProperties.EnergySystem.MaxEnergyCapacity = k_TruckTankSize;
                    newVehicle = new Bike(vehicleProperties);
                    break;
            }

            return newVehicle;
        }

        internal bool isValidAitPressure(float i_CurrentAirPressure,string i_VehicleType)
        {
            eVehicleType VehicleType = (eVehicleType) Enum.Parse(typeof(eVehicleType), i_VehicleType);
            bool isValidAirPressure = false;

            switch (VehicleType)
            {
                case eVehicleType.ElectricCar:
                case eVehicleType.FuelCar:
                    isValidAirPressure = k_CarMaxAirPressure > i_CurrentAirPressure;
                    break;
                case eVehicleType.ElectricBike:
                case eVehicleType.FuelBike:
                    isValidAirPressure = k_BikeMaxAirPressure > i_CurrentAirPressure;
                    break;
                case eVehicleType.Truck:
                    isValidAirPressure = k_TruckAirPressure > i_CurrentAirPressure;
                    break;
            }

            return isValidAirPressure;
        }

        internal string getListOfVehicleTypes()
        {
            int i = 1;
            StringBuilder carTypeList = new StringBuilder();
            string[] carTypeArray = Enum.GetNames(typeof(eVehicleType));
            foreach(string carType in carTypeArray)
            {
                carTypeList.Append(string.Format("{0} : {1} {2}", i++, carType,Environment.NewLine));
            }

            return carTypeList.ToString();
        }

        private void validateEnergyLevel(float i_MaxEnergyCapacity, float i_currentEnergyCapacity)
        {
            if(i_currentEnergyCapacity > i_MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(0, i_MaxEnergyCapacity);
            }
        }



        public enum eVehicleType
        {
            ElectricCar,
            FuelCar,
            ElectricBike,
            FuelBike,
            Truck
        }


    }
}

