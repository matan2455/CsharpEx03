using System;
using System.Collections.Generic;
using Ex03.GarageLogicVehicleParts;
using System.Text;

namespace Ex03.GarageLogicVehicle
{
    class Truck : Vehicle
    {

        const int k_MaxAirPressure = 28;
        const int k_NumOfWeels = 16;
        readonly bool r_Containsdangerousmaterials;
        readonly int r_volumeOfCargo;

        internal Truck(VehicleProperties i_TruckProperties)
         : base(i_TruckProperties)
        {
            r_Containsdangerousmaterials = i_TruckProperties.SpecialFeatures["Contains dangerous materials"].ToLower().Equals("true");
            r_volumeOfCargo = int.Parse(i_TruckProperties.SpecialFeatures["Volume of cargo"]);
        }

        internal int VolumeOfCargo
        {
            get
            {
                return r_volumeOfCargo;
            }
        }

        internal bool Containsdangerousmaterials
        {
            get
            {
                return r_Containsdangerousmaterials;
            }
        }

    }
}
