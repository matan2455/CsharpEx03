using System;
using System.Collections.Generic;
using Ex03.GarageLogicVehicleParts;


namespace Ex03.GarageLogicVehicle
{
    class Car : Vehicle
    {
        const int k_MaxAirPressure = 32;
        const int k_NumOfWeels = 4;
        readonly eNumOfDoors r_NumOfDoors;
        readonly eCarColor r_CarColor;
        
        internal Car(VehicleProperties i_CarProperties)
            : base(i_CarProperties)
        {
            r_NumOfDoors = (eNumOfDoors)Enum.Parse(typeof(eNumOfDoors), i_CarProperties.SpecialFeatures["Number Of Doors"]);
            r_CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_CarProperties.SpecialFeatures["Car color"]);
            for (int i = 0; i < k_NumOfWeels; i++)
            {
                this.r_VehicleWheels.Add(new Wheel(i_CarProperties.WheelProperties));
            }
        }

    }

    enum eNumOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }

    enum eCarColor
    {
        Silver,
        Black,
        Red,
        White
    }
}
