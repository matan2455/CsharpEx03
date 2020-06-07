using Ex03.GarageLogicVehicleParts;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ex03.GarageLogicVehicle
{
    abstract class Vehicle
    {
        internal readonly String r_Model;
        internal readonly String r_LicenseId;
        internal readonly EnergySystem m_Energy;
        internal readonly VehicleProperties r_VehicleProperties;
        internal float m_energyPrecent;
        internal readonly List<Wheel> r_VehicleWheels;

        internal Vehicle(VehicleProperties i_VehicleProperties)
        {
       
            this.r_VehicleProperties = i_VehicleProperties;
            this.r_LicenseId = i_VehicleProperties.LicenseNumber;
            this.r_Model = i_VehicleProperties.Model;
            r_VehicleWheels = new List<Wheel>();
            if (i_VehicleProperties.EnergySystem.EnergySource.ToLower().Equals("fuel"))
            {
                this.m_Energy = new FuelSystem(i_VehicleProperties.EnergySystem);
            }
            else
            {
                this.m_Energy = new ElectricSystem(i_VehicleProperties.EnergySystem);
            }
   
        }

        internal string LicensId
        {
            get
            {
                return this.r_LicenseId;
            }
        }

        internal void pumpAir(float i_AirToAdd)
        {
            foreach(Wheel wheel in r_VehicleWheels)
            {
                wheel.FileAir(i_AirToAdd);
            }
        }

        internal VehicleProperties VehicleProperties
        {
            get
            {
                return r_VehicleProperties;
            }
        }
    }
}
