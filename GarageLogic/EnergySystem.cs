using System;
using System.Collections.Generic;



namespace Ex03.GarageLogicVehicleParts
{
    abstract class EnergySystem
    {
        readonly float r_MaxEnergyCapacity;
        float m_CurrentEnergyAmmount;

        internal EnergySystem(VehicleProperties.EnergySystemProperties i_EnergyProperties)
        {
            r_MaxEnergyCapacity = i_EnergyProperties.MaxEnergyCapacity;
            m_CurrentEnergyAmmount = i_EnergyProperties.CurrentEnergy;
        }


        internal virtual float CurrentAmountOfEnergy
        {
            get
            {
                return r_MaxEnergyCapacity;
            }
        }

        internal float MaxEnergyCapacity
        {
            get
            {
                return m_CurrentEnergyAmmount;
            }
        }
        
        internal abstract void AddEnergyToSystem(float i_amountToAdd);

    }
}




