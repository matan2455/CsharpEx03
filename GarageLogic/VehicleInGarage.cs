using System;
using Ex03.GarageLogicVehicleParts;
using Ex03.GarageLogicVehicle;
using System.Text;

namespace Ex03.GarageLogicVehicleRegestration
{
    internal class VehicleInGarage
    {
        eStatus m_Status;
        readonly Vehicle r_RegisteredVehicle;
        readonly string r_OwnerName;
        readonly string r_OwnerPhoneNumber;
        
        internal VehicleInGarage(Vehicle i_NewVehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            this.m_Status = eStatus.InRepair;
            this.r_OwnerName = i_OwnerName;
            this.r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            this.r_RegisteredVehicle = i_NewVehicle;
        }


        internal string Status
        {
            get
            {
                return m_Status.ToString();
            }
            set
            {
                this.m_Status = (eStatus)Enum.Parse(typeof(eStatus), value);
            }
        }

        internal string getVehicleDataAsString()
        {
            VehicleProperties vehicleProperties = this.r_RegisteredVehicle.VehicleProperties;
            StringBuilder VehicleData = new StringBuilder();

            VehicleData.Append("LicenseNumber :" + vehicleProperties.LicenseNumber);
            VehicleData.Append("Vehicle Model :" + vehicleProperties.Model);
            VehicleData.Append("Owner Name :" + r_OwnerName);
            VehicleData.Append("Vehicle status :" + m_Status);
            VehicleData.Append(string.Format("Wheels : {0} manufacturer name - {1}, air pressure - {2},  "
                ,Environment.NewLine, vehicleProperties.WheelProperties.Wheelmanufacturer, vehicleProperties.WheelProperties.Wheelmanufacturer, vehicleProperties.WheelProperties.CurrentAirPressure));
            //add energy status
            VehicleData.Append(string.Format("{0} : " + r_OwnerName));
            foreach(string key in vehicleProperties.SpecialFeatures.Keys)
            {
                VehicleData.Append(string.Format("{0} : {1}", key, vehicleProperties.SpecialFeatures[key]));
            }

            return VehicleData.ToString();
        }

        internal Vehicle Vehicle
        {
            get
            {
                return r_RegisteredVehicle;
            }
        }

        internal Vehicle RegisterdVehicle
        {
            get
            {
                return r_RegisteredVehicle;
            }
        }
    }

    enum eStatus
    {
        InRepair,
        Repaired,
        PayedFor
    }
}
