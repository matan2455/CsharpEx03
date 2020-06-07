using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogicVehicleParts
{
    class VehicleProperties
    {

        private readonly string r_LicenseNumber;
        private readonly string r_Model;
        private readonly eVehicleGeneralType r_VehicleType;
        private readonly EnergySystemProperties r_EnergySystemProperties;
        private readonly WheelSystemProperties r_WheelProperties;
        private Dictionary<string, string> m_NonGenericVehicleFeatures;




        public VehicleProperties(Dictionary<string,string> i_VehicleData, string i_VehicleSpecificType)
        {
            this.r_LicenseNumber = i_VehicleData["license number"];
            this.r_Model = i_VehicleData["Model"];
            r_EnergySystemProperties = new EnergySystemProperties(i_VehicleData);
            r_WheelProperties = new WheelSystemProperties(i_VehicleData);
            r_VehicleType = getVehicleGeneralType(i_VehicleSpecificType); 
            setSpacielFeatures(i_VehicleData);
        }


        private eVehicleGeneralType getVehicleGeneralType(string i_VehicleSpecificType)
        {
            eVehicleGeneralType VehicleGeneralType;
            string result = String.Empty;

            foreach (string VehicleType in Enum.GetValues(typeof(eVehicleGeneralType)))
            {
                if (i_VehicleSpecificType.Contains(VehicleType))
                {
                    result = VehicleType;
                    break;
                }
            }
            VehicleGeneralType = (eVehicleGeneralType)Enum.Parse(typeof(eVehicleGeneralType), result);

            return VehicleGeneralType;
        }


        private void setSpacielFeatures(Dictionary<string, string> i_vehicleData)
        {
            switch (r_VehicleType)
            {
                case eVehicleGeneralType.Car:
                    m_NonGenericVehicleFeatures["Number Of Doors"] = i_vehicleData["Number Of Doors"];
                    m_NonGenericVehicleFeatures["Car color"] = i_vehicleData["Car color"];
                    break;
                case eVehicleGeneralType.Bike:
                    m_NonGenericVehicleFeatures["License Type"] = i_vehicleData["License Type"];
                    m_NonGenericVehicleFeatures["Engine volume"] = i_vehicleData["Engine volume"];
                    break;
                case eVehicleGeneralType.Truck:
                    m_NonGenericVehicleFeatures["Contains dangerous materials"] = i_vehicleData["Contains dangerous materials"];
                    m_NonGenericVehicleFeatures["Volume of cargo"] = i_vehicleData["Volume of cargo"];
                    break;
            }
        }

        public enum eVehicleGeneralType
        {
            Car,
            Bike,
            Truck
        }



        internal Dictionary<string, string> SpecialFeatures
        {
            get
            {
                return m_NonGenericVehicleFeatures;
            }
        }

        internal EnergySystemProperties EnergySystem
        {
            get
            {
                return r_EnergySystemProperties;
            }
        }

        internal WheelSystemProperties WheelProperties
        {
            get
            {
                return r_WheelProperties;
            }
        }

        internal string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        internal eVehicleGeneralType VehicleType
        {
            get
            {
                return r_VehicleType;
            }
        }

        public string Model
        {
            get
            {
                return r_Model;
            }
        }

        internal enum eNumberOfDoors
        {
            NotACar = 0,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }



        internal class WheelSystemProperties
        {
            readonly string r_Wheelmanufacturer;
            float m_CurrentAirPressure;
            float m_MaxAirPressure;

            internal WheelSystemProperties(Dictionary<string, string> i_vehicleData)
            {
                bool inputExists = true;
                bool isValidFloat = true;

                inputExists = i_vehicleData.TryGetValue("Wheel Manufacturer", out r_Wheelmanufacturer);
                if (!inputExists)
                {
                    throw new ArgumentException("Wheel Manufacturer was not provided");
                }
                inputExists = i_vehicleData.ContainsKey("current air pressure");
                isValidFloat = float.TryParse(i_vehicleData["current air pressure"],out m_CurrentAirPressure);

                if (!inputExists)
                {
                    throw new ArgumentException("Wheel air pressure was not provided");
                }
                else if (!isValidFloat)
                {
                    throw new ArgumentException(string.Format("Wheel air pressure {0} is not a valid number", i_vehicleData["current air pressure"]));
                }
            }


            internal string Wheelmanufacturer
            {
                get { return r_Wheelmanufacturer; }
            }

            internal float CurrentAirPressure
            {
                get { return m_CurrentAirPressure; }
                set { m_CurrentAirPressure = value; }
            }

            internal float MaxAirPressure
            {
                get { return m_MaxAirPressure; }
                set { m_MaxAirPressure = value; }
            }
        }

        internal class EnergySystemProperties
        {
            readonly eEnergySystem r_EnergySystem;
            readonly eEnergySource r_EnergySource;
            float m_CurrentEnergy;
            float m_maxEnergyCapacity;

            internal EnergySystemProperties(Dictionary<string,string> i_vehicleData)
            {
                r_EnergySystem = (eEnergySystem)Enum.Parse(typeof(eEnergySystem), i_vehicleData["Energy System"]);
                r_EnergySource = (eEnergySource)Enum.Parse(typeof(eEnergySource), i_vehicleData["Energy Source"]);
                m_CurrentEnergy = float.Parse(i_vehicleData["Current Energy"]);
                //check that fuel source match the fuel system
            }

            internal string EnergySystem
            {
                get { return r_EnergySystem.ToString(); }
            }

            internal string EnergySource
            {
                get { return r_EnergySource.ToString(); }
            }

            internal float CurrentEnergy
            {
                get
                {
                    return m_CurrentEnergy;
                }
            }

            internal float MaxEnergyCapacity
            {
                get
                {
                    return m_maxEnergyCapacity;
                }
                set
                {
                    m_maxEnergyCapacity = value;
                }
            }

            internal enum eEnergySystem
            {
                Electric = 0,
                Fuel = 1,
            }

            internal enum eEnergySource
            {
                Electricity = 0,
                Soler = 1,
                Octan95 = 2,
                Octan96 = 3,
                Octan98 = 4
            }
        }
    }
}
