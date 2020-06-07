using System;
using System.Collections.Generic;
using Ex03.GarageLogicExceptions;
using System.Text;

namespace Ex03.GarageLogicVehicleParts
{
    class ElectricSystem : EnergySystem
    {
        readonly float r_maxBatteryCapacity;
        float m_BatteryTimeLeft;

        internal ElectricSystem(VehicleProperties.EnergySystemProperties i_ElectricSystemProperties)
            : base(i_ElectricSystemProperties)
        {
            r_maxBatteryCapacity = i_ElectricSystemProperties.MaxEnergyCapacity;
            m_BatteryTimeLeft = i_ElectricSystemProperties.CurrentEnergy;
        }

        internal override void AddEnergyToSystem(float i_AmountToCharge)
        {

            if (m_BatteryTimeLeft + i_AmountToCharge > r_maxBatteryCapacity)
            {
                throw new ValueOutOfRangeException(0, r_maxBatteryCapacity);
            }
            else if (i_AmountToCharge < 0)
            {
                throw new ArgumentException(string.Format("you can only charge the battery, yet you chose a negetive value : {0} ", i_AmountToCharge));
            }
            else
            {
                m_BatteryTimeLeft += i_AmountToCharge;
            }
        }
    }
}
