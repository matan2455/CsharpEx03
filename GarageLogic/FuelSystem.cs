using System;
using System.Collections.Generic;
using Ex03.GarageLogicExceptions;
using System.Text;


namespace Ex03.GarageLogicVehicleParts
{
    class FuelSystem : EnergySystem
    {
        readonly float r_maxFuelCapacity;
        float m_CurrentammountOfFuel;
        readonly eTypeOfFuel r_TypeOfFuel;

        internal FuelSystem(VehicleProperties.EnergySystemProperties i_fuelSystemProperties)
            :base(i_fuelSystemProperties)
        {
            r_maxFuelCapacity = i_fuelSystemProperties.MaxEnergyCapacity;
            m_CurrentammountOfFuel = i_fuelSystemProperties.CurrentEnergy;     
            r_TypeOfFuel = (eTypeOfFuel)Enum.Parse(typeof(eTypeOfFuel), i_fuelSystemProperties.EnergySource);
        }

        internal static string getListOfFuelTypes()
        {
            int i = 1;
            StringBuilder fuelTypeList = new StringBuilder();
            string[] fuelTypeArray = Enum.GetNames(typeof(eTypeOfFuel));
            foreach (string fuelType in fuelTypeArray)
            {
                fuelTypeList.Append(string.Format("{0} : {1} {2}", i++, fuelType, Environment.NewLine));
            }

            return fuelTypeList.ToString();
        }

        internal override void AddEnergyToSystem(float i_amountToAdd) 
        {

            if (m_CurrentammountOfFuel + i_amountToAdd > r_maxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, r_maxFuelCapacity);
            }
            else if(i_amountToAdd < 0)
            {
                throw new ArgumentException(string.Format("you can only fill gas, yet you chose a negetive value : {0} ", i_amountToAdd));
            }
            else
            {
                m_CurrentammountOfFuel += i_amountToAdd;
            }
        }

        internal string TypeOfFule 
        {
            get { return r_TypeOfFuel.ToString(); }
        }
    }

    enum eTypeOfFuel
    {
        Soler,
        Octan95,
        Octan96,
        Octan98
    }
}

