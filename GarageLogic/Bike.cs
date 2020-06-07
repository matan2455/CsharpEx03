using System;
using System.Collections.Generic;
using Ex03.GarageLogicVehicleParts;
using System.Text;

namespace Ex03.GarageLogicVehicle
{
    class Bike : Vehicle
    {
        const int k_MaxAirPressure = 30;
        const int k_NumOfWeels = 2;
        readonly eLicenseType r_LicenseType;
        readonly int r_EngineVolume;

        internal Bike(VehicleProperties i_BikeProperties)
            : base(i_BikeProperties)
        {
            //add exceptions
            r_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_BikeProperties.SpecialFeatures["License Type"]);
            r_EngineVolume = int.Parse(i_BikeProperties.SpecialFeatures["Engine Volume"]);
            for (int i = 0; i < k_NumOfWeels; i++)
            {
                this.r_VehicleWheels.Add(new Wheel(i_BikeProperties.WheelProperties));
            }
        }

    }

    enum eLicenseType
    {
         A,
         A1,
         AA,
         B
    }
}
