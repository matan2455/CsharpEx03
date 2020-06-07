using System;
using Ex03.GarageLogicExceptions;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogicVehicleParts
{
    public class Wheel
    {

        private readonly String r_ManufactuerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        internal Wheel(VehicleProperties.WheelSystemProperties i_WheelProperties) 
        { 
            this.r_ManufactuerName = i_WheelProperties.Wheelmanufacturer;
            this.r_MaxAirPressure = i_WheelProperties.MaxAirPressure;
            this.m_CurrentAirPressure = i_WheelProperties.CurrentAirPressure;
        }

        internal String ManufactuerName
        {
            get { return r_ManufactuerName; }
        }

        internal float currentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        internal float maxAirPressure
        {
            get { return r_MaxAirPressure; }
        }
        
        internal void FileAir(float i_airToAdd)
        {
            if (currentAirPressure + i_airToAdd > r_MaxAirPressure || i_airToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure - currentAirPressure);
            }
            else
            {
                currentAirPressure = currentAirPressure + i_airToAdd;
            }
        }
    }
}
