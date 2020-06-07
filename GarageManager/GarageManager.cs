namespace Ex03.ConsoleUIGarageManager
{

    using System;
    using System.Collections.Generic;
    using Ex03.GarageLogicGarageManagement;
    using Ex03.GarageLogicExceptions;

    class GarageManager
    {

        Garage m_CurrentGarage;

        internal GarageManager()
        {
            m_CurrentGarage = new Garage();
        }

        internal void RunGarage()
        {
            bool isWorking = true;
            int opCodeToExecute;

            while (isWorking)
            {
                startMessage();
                opCodeToExecute = getValidOperationCode();

                switch (opCodeToExecute)
                {
                    case 1:
                        insertNewVehicle();
                        break;
                    case 2:
                        displayAllVehiclesByLicenseNumber();
                        break;
                    case 3:
                        changeVehicleStatus();
                        break;
                    case 4:
                        addAirToVehicleWheels();
                        break;
                    case 5:
                        addFuelToVehicle();
                        break;
                    case 6:
                        chargeBatteryToVehicle();
                        break;
                    case 7:
                        displayVehiclesData();
                        break;
                    case 8:
                        isWorking = false;
                        exit();
                        break;
                }
            }
        }

        private void insertNewVehicle()
        {
            String clientName = null, clientPhoneNumber, energyType, fuelType, vehicleType, currentAirPressure, currentEnegryStatus, licenseNumber;
            int fuelOrElecrtic;
            Dictionary<string, string> newVehicleProperties = new Dictionary<string, string>();

            Console.WriteLine("You chose to add a vehicle");
            try
            {
                Console.WriteLine("Please enter client's name:");
                clientName = getValidName();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            newVehicleProperties.Add("name", clientName);

            Console.WriteLine("Please enter client's phone number: 10 digits");
            clientPhoneNumber = getValidPhoneNumber();

            newVehicleProperties.Add("phone number", clientPhoneNumber);

            Console.WriteLine("Please enter vehicle license number: 8 digits number");
            licenseNumber = getValidLicenseNumber();
            if (m_CurrentGarage.isLicenseNumberExists(licenseNumber))
            {
                Console.WriteLine("Your car already exists in our system. Your problem will be fixed quickly!");
            }

            Console.WriteLine("Please enter your vehicle type");
            Console.WriteLine(m_CurrentGarage.getListOfVehiclesTypes());
            vehicleType = getValidVehicleType();
            newVehicleProperties.Add("vehicle type", vehicleType);

            if (vehicleType.Equals("truck"))
            {
                energyType = "fuel";
            }
            else
            {
                Console.WriteLine("Please enter your energy type:\n 1 - fuel\n 2 - elecrtic\n");
                fuelOrElecrtic = vehicleRunOnFuelOrElectricity();
                energyType = (fuelOrElecrtic == 1) ? "fuel" : "electric";
            }
            newVehicleProperties.Add("energy type", energyType);

            if (energyType.Equals("fuel"))
            {
                Console.WriteLine("Please enter fuel type:");
                Console.WriteLine(m_CurrentGarage.getListOfFuelTypes()); // choose number instead of String name for less room for typing error
                fuelType = getValidFuelType();
                newVehicleProperties.Add("fuel type", fuelType);
            }

            if (vehicleType.Equals("car"))
            {
                String colorOfCar, numberOfDoors;

                Console.WriteLine("Please enter how many doors are in the car: 2, 3, 4 or 5");
                numberOfDoors = getValidNumberOfDoors();
                newVehicleProperties.Add("car number of doors", numberOfDoors);

                Console.WriteLine("Please enter the color of the car: choose from list below\n black, silver, red or white");
                colorOfCar = getValidColorOfCar();
                newVehicleProperties.Add("car color", colorOfCar);
            }
            else if (vehicleType.Equals("bike"))
            {
                String licenseType, engineVolume;

                Console.WriteLine("Please enter your bike license: choose from list below\n AA, A, A1, B");
                licenseType = getValidLicenseType();
                newVehicleProperties.Add("bike license type", licenseType);

                Console.WriteLine("Please enter the engine volume");
                engineVolume = getValidEngineVolume();
                newVehicleProperties.Add("bike engine volume", engineVolume);
            }
            else
            {
                String containDangerousMaterials, trunkVolume;

                Console.WriteLine("Please enter if you have dangerous materials in your truck: True - if yes, False - if no");
                containDangerousMaterials = getValidDangerousMaterials();
                newVehicleProperties.Add("bike license type", containDangerousMaterials);

                Console.WriteLine("Please enter the trunk volume");
                trunkVolume = getValidTrunkVolume();
                newVehicleProperties.Add("bike engine volume", trunkVolume);

            }

            Console.WriteLine("Please enter curernt amout of fuel in tank (in liters)");
            currentEnegryStatus = getCurrentEnergyAmount(vehicleType, energyType);
            newVehicleProperties.Add("current energy", currentEnegryStatus);

            Console.WriteLine("Please enter curernt amout of pressure in wheels");
            currentAirPressure = getCurrentAirPressureAmount(vehicleType);
            newVehicleProperties.Add("current air pressure", currentAirPressure);

            try
            {
                m_CurrentGarage.InsertNewVehicle(newVehicleProperties);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private String getValidLicenseNumber()
        {
            String licenseNumber;
            bool validName = true;

            licenseNumber = Console.ReadLine();
            if (licenseNumber.Equals(string.Empty))
            {
                throw new ArgumentException("Invalid number. license number must contain exactly 8 digits");
            }

            while (!validName)
            {
                for (int i = 1; i < licenseNumber.Length; i++)
                {
                    if (licenseNumber[i] < '0' || licenseNumber[i] > '9' || licenseNumber.Length != 8)
                    {
                        Console.WriteLine("Invalid license number");
                        Console.WriteLine("Please ender a valid license number contains 8 digits");
                        licenseNumber = Console.ReadLine();
                        break;
                    }

                    if (i == licenseNumber.Length - 1)
                    {
                        validName = false;
                    }
                }
            }

            return licenseNumber;
        }

        private String getCurrentAirPressureAmount(String o_VehicleType)
        {
            String inputCurrentAirPressure = Console.ReadLine();
            int currentAirPressure;
            bool isValidInput = int.TryParse(inputCurrentAirPressure, out currentAirPressure);

            try
            {
                while (currentAirPressure < 0 || m_CurrentGarage.isValidCurrentAirPressure(currentAirPressure, o_VehicleType)) // go to backend
                {
                    Console.WriteLine("Invalid input. Please check your current energy status and enter it");
                    inputCurrentAirPressure = Console.ReadLine();
                    isValidInput = int.TryParse(inputCurrentAirPressure, out currentAirPressure);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return inputCurrentAirPressure;
        }

        private String getCurrentEnergyAmount(String o_vehicleType, String o_energyType)
        {
            String inputCurrentEnergyAmount = Console.ReadLine();
            int currentEnergyAmount;
            bool isValidInput = int.TryParse(inputCurrentEnergyAmount, out currentEnergyAmount);

          ///  try
           // {
            //    while (m_CurrentGarage.checkValidCurrentEnergy(inputCurrentEnergyAmount, o_vehicleType, o_energyType)) // go to backend
             //   {
              //      Console.WriteLine("Invalid input. Please check your current energy status and enter it");
               //     inputCurrentEnergyAmount = Console.ReadLine();
                //    isValidInput = int.TryParse(inputCurrentEnergyAmount, out currentEnergyAmount);
               // }
           // }
           // catch (ArgumentOutOfRangeException ex)
           // {
            //    Console.WriteLine(ex.Message);
           // }

            return inputCurrentEnergyAmount;
        }

        private String getValidDangerousMaterials()
        {
            String isDangerousMaterials = Console.ReadLine().ToLower();

            while (!(isDangerousMaterials.Equals("false") || isDangerousMaterials.Equals("true")))
            {
                Console.WriteLine("Invalid input. Please enter True or False");
                isDangerousMaterials = Console.ReadLine();
            }

            return isDangerousMaterials;
        }

        private String getValidTrunkVolume()
        {
            String inputtrunkVolume = Console.ReadLine();
            int trunkVolumeInt;
            bool isValidNumberOfDoors = int.TryParse(inputtrunkVolume, out trunkVolumeInt);

            while (!isValidNumberOfDoors)
            {
                Console.WriteLine("Invalid number. Please enter a valid integer.");
                inputtrunkVolume = Console.ReadLine();
                isValidNumberOfDoors = int.TryParse(inputtrunkVolume, out trunkVolumeInt);
            }

            return inputtrunkVolume;
        }

        private String getValidEngineVolume()
        {
            String inputEngineVolume = Console.ReadLine();
            int engineVolumeInt;
            bool isValidNumberOfDoors = int.TryParse(inputEngineVolume, out engineVolumeInt);

            while (!isValidNumberOfDoors)
            {
                Console.WriteLine("Invalid number. Please enter a valid integer.");
                inputEngineVolume = Console.ReadLine();
                isValidNumberOfDoors = int.TryParse(inputEngineVolume, out engineVolumeInt);
            }

            return inputEngineVolume;
        }

        private String getValidLicenseType()
        {
            String inputLicenseType = Console.ReadLine();
            bool isValidColor = false;

            while (!isValidColor)
            {
                switch (inputLicenseType)
                {
                    case "AA":
                        isValidColor = true;
                        break;
                    case "A1":
                        isValidColor = true;
                        break;
                    case "A":
                        isValidColor = true;
                        break;
                    case "B":
                        isValidColor = true;
                        break;
                    default:
                        Console.WriteLine(inputLicenseType + " is invalid color. Please enter black, white, red or silver");
                        inputLicenseType = Console.ReadLine();
                        break;
                }
            }

            return inputLicenseType;
        }

        private String getValidNumberOfDoors()
        {
            String inputNumberOfDoors = Console.ReadLine();
            int numberOfDoorsInCar;
            bool isValidNumberOfDoors = int.TryParse(inputNumberOfDoors, out numberOfDoorsInCar);

            while (!isValidNumberOfDoors || numberOfDoorsInCar < 2 || numberOfDoorsInCar > 5)
            {
                Console.WriteLine("Invalid number of doors. Please enter a nubmer between 2-5");
                inputNumberOfDoors = Console.ReadLine();
                isValidNumberOfDoors = int.TryParse(inputNumberOfDoors, out numberOfDoorsInCar);
            }

            return inputNumberOfDoors;
        }

        private String getValidColorOfCar()
        {
            String inputColor = Console.ReadLine().ToLower();
            bool isValidColor = false;

            while (!isValidColor)
            {
                switch (inputColor)
                {
                    case "black":
                        isValidColor = true;
                        break;
                    case "white":
                        isValidColor = true;
                        break;
                    case "silver":
                        isValidColor = true;
                        break;
                    case "red":
                        isValidColor = true;
                        break;
                    default:
                        Console.WriteLine(inputColor + " is invalid color. Please enter black, white, red or silver");
                        inputColor = Console.ReadLine();
                        break;
                }
            }

            return inputColor;
        }

        private enum eColorOfCar
        {
            red,
            black,
            silver,
            white
        }

        private String getValidVehicleType()
        {
            String inputVehicleType = Console.ReadLine();
            String vehicleTypeStr;
            int vehicleType;
            bool isValidVehicleType = int.TryParse(inputVehicleType, out vehicleType);

            while (!isValidVehicleType || vehicleType < 1 || vehicleType > 3)
            {
                Console.WriteLine("Invalid choice. Please enter 1 if this vehicle is a bike, 2 if a car or 3 if its a truck");
                inputVehicleType = Console.ReadLine();
                isValidVehicleType = int.TryParse(inputVehicleType, out vehicleType);
            }

            switch (vehicleType)
            {
                case 1:
                    vehicleTypeStr = "bike";
                    break;
                case 2:
                    vehicleTypeStr = "car";
                    break;
                default:
                    vehicleTypeStr = "truck";
                    break;
            }

            return vehicleTypeStr;

        }

        private String getValidFuelType()
        {
            String inputFuelType = Console.ReadLine();
            String fuelTypeStr;
            int fuelType;

            bool isValidInput = int.TryParse(inputFuelType, out fuelType);

            while (!isValidInput || fuelType < 1 || fuelType > 4)
            {
                Console.WriteLine("Invalid fuel type. Please press:");
                Console.WriteLine(m_CurrentGarage.getListOfFuelTypes());
                inputFuelType = Console.ReadLine();
                isValidInput = int.TryParse(inputFuelType, out fuelType);
            }

            switch (fuelType)
            {
                case 1:
                    fuelTypeStr = "Octan95";
                    break;
                case 2:
                    fuelTypeStr = "Octan96";
                    break;
                case 3:
                    fuelTypeStr = "Octan98";
                    break;
                default:
                    fuelTypeStr = "Soler";
                    break;
            }

            return fuelTypeStr;
        }

        private int vehicleRunOnFuelOrElectricity()
        {
            int energyType;
            String inputTypeRecieved = Console.ReadLine();

            bool isValidNumber = int.TryParse(inputTypeRecieved, out energyType);

            while (!isValidNumber || !(energyType == 1 || energyType == 2))
            {
                Console.WriteLine("Invalid choice. Please enter 1 if your car run on fuel or 2 if on electricity");
                inputTypeRecieved = Console.ReadLine();
                isValidNumber = int.TryParse(inputTypeRecieved, out energyType);
            }

            return energyType;
        }

        private String getValidName()
        {
            String nameOfClient;
            bool validName = true;

            nameOfClient = Console.ReadLine();
            if (nameOfClient.Equals(string.Empty))
            {
                throw new ArgumentException("Invalid Name. Client's name has to have atleast one character");
            }

            nameOfClient = nameOfClient.ToLower();
            while (!validName)
            {
                for (int i = 0; i < nameOfClient.Length; i++)
                {
                    if ((nameOfClient[i] < 'a' || nameOfClient[i] > 'z') && nameOfClient[i] != ' ')
                    {
                        Console.WriteLine("Invalid Name. " + nameOfClient[i] + " is illigal char for a name");
                        Console.WriteLine("Please ender a valid name contains only a-z characters");
                        nameOfClient = Console.ReadLine().ToLower();
                        break;
                    }

                    if (i == nameOfClient.Length - 1)
                    {
                        validName = false;
                    }
                }
            }

            return nameOfClient;
        }

        private String getValidPhoneNumber()
        {
            String clientPhoneNumber;
            bool validName = true;

            clientPhoneNumber = Console.ReadLine();
            if (clientPhoneNumber.Equals(string.Empty))
            {
                throw new ArgumentException("Invalid number. Client's phone number must contain 10 digits");
            }

            while (!validName)
            {
                for (int i = 1; i < clientPhoneNumber.Length; i++)
                {
                    if (clientPhoneNumber[i] < '0' || clientPhoneNumber[i] > '9' || clientPhoneNumber[0] != '0' || clientPhoneNumber.Length != 10)
                    {
                        Console.WriteLine("Invalid phone number");
                        Console.WriteLine("Please ender a valid name contains only a-z characters");
                        clientPhoneNumber = Console.ReadLine();
                        break;
                    }

                    if (i == clientPhoneNumber.Length - 1)
                    {
                        validName = false;
                    }
                }
            }

            return clientPhoneNumber;
        }

        private void displayAllVehiclesByLicenseNumber()
        {
            String whichVehiclestoShow, listToPrint;

            Console.WriteLine("You chose to display list of License numbers.");
            Console.WriteLine("Please choose which vehicles you wish to view:\n 1 - All vehicles in garage\n 2 - Only vehicles at in-repair status\n 3 - Only vehicles at repaired status\n 4 - Only vehicles at paid\n");
            whichVehiclestoShow = Console.ReadLine();

            try
            {
                listToPrint = m_CurrentGarage.getVehicleList(whichVehiclestoShow);

                Console.WriteLine(listToPrint);
            }
            catch (Exception ex)
            {

            }
        }

        private void changeVehicleStatus()
        {
            String licenseNumber, newStatus;
            bool isValidNewStatus = false;

            try
            {
                Console.WriteLine("You chose to change status");
                Console.WriteLine("Please enter the license number of the vehicle you wish to change status");
                licenseNumber = Console.ReadLine();

                if (!m_CurrentGarage.isLicenseNumberExists(licenseNumber))
                {
                    Console.WriteLine("license number not in garage");
                }
                else
                {
                    Console.WriteLine("Please enter newStatus:\n 1 - In-repair\n 2 - Repaired\n 3 - Paid");
                    newStatus = Console.ReadLine();

                    while (!isValidNewStatus)
                    {
                        switch (newStatus)
                        {
                            case "1":
                                m_CurrentGarage.changeVehicleStatus(licenseNumber, "in-repair");
                                isValidNewStatus = true;
                                break;
                            case "2":
                                m_CurrentGarage.changeVehicleStatus(licenseNumber, "repaired");
                                isValidNewStatus = true;
                                break;
                            case "3":
                                m_CurrentGarage.changeVehicleStatus(licenseNumber, "paid");
                                isValidNewStatus = true;
                                break;
                            default:
                                Console.WriteLine("Invalid new status. Please press:\n 1 - in-repair\n 2 - repaired\n 3 - paid");
                                break;
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void addAirToVehicleWheels()
        {
            String licenseNumber;
            float airToFill;
            bool isValidNumber;

            Console.WriteLine("You chose to add air pressure to wheel");
            Console.WriteLine("Please enter the license number of the vehicle for which you wish to fill air");
            licenseNumber = Console.ReadLine();
            isValidNumber = int.TryParse(licenseNumber, out _);

            while (string.IsNullOrEmpty(licenseNumber) || !isValidNumber) 
            {
                Console.WriteLine("you did not enter a valid license plate - please enter a string consisting only of numbers");
                licenseNumber = Console.ReadLine();
            }

            Console.WriteLine("Please enter the amount of air you would like to fill");
            isValidNumber = float.TryParse(Console.ReadLine(), out airToFill);

            while (!isValidNumber)
            {
                Console.WriteLine("{0} is not a valid air ammount please enter a valid number", airToFill);
                isValidNumber = float.TryParse(Console.ReadLine(), out airToFill);
            }

            try
            {
                m_CurrentGarage.addAirToVehiclewheels(licenseNumber, airToFill);
                Console.WriteLine("mission complete. vehicle number " + licenseNumber + " Max air pressure in all wheels");
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void addFuelToVehicle()
        {
            String licenseNumber, fuelType, inputAmountOfFuelToAdd;
            float amoutOfFuelToAdd;

            Console.WriteLine("You chose to add Fuel");
            Console.WriteLine("Please enter the license number of the vehicle you wish to change status");
            licenseNumber = Console.ReadLine();

            Console.WriteLine("Please enter the fuel type: please choose from list\n 1 - Octan95\n 2 - Octan96\n 3 - Octan98\n 4 - Soler");
            fuelType = getValidFuelType();

            Console.WriteLine("Please enter the amout of fuel you want to add: (in liters)");
            inputAmountOfFuelToAdd = Console.ReadLine();

            bool isValidnumber = float.TryParse(inputAmountOfFuelToAdd, out amoutOfFuelToAdd);
            if (!isValidnumber)
            {
                throw new FormatException("unvalid float number");
            }

            try
            {
                m_CurrentGarage.AddEnergyToVehicle(licenseNumber, fuelType, amoutOfFuelToAdd);
                Console.WriteLine("mission complete. vehicle number " + licenseNumber + " was fueled" + inputAmountOfFuelToAdd + " liters");
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void chargeBatteryToVehicle()
        {
            String licenseNumber, inputTimeToCharge;
            float amoutToCharge;

            Console.WriteLine("You chose to charge battary");
            Console.WriteLine("Please enter the license number of the vehicle you wish to change status");
            licenseNumber = Console.ReadLine();

            Console.WriteLine("Please enter time to charge battary: (in hours)");
            inputTimeToCharge = Console.ReadLine();

            bool isValidnumber = float.TryParse(inputTimeToCharge, out amoutToCharge);
            if (!isValidnumber)
            {
                throw new FormatException("unvalid float number");
            }

            try
            {
                m_CurrentGarage.AddEnergyToVehicle(licenseNumber, "electricity", amoutToCharge);
                Console.WriteLine("mission complete. vehicle number " + licenseNumber + " was chraged " + inputTimeToCharge + " hours");
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void displayVehiclesData()
        {
            String licenseNumber, dataToPrint;

            Console.WriteLine("You chose to add Fuel to vehicle");
            Console.WriteLine("Please enter the license number of the vehicle you wish to change status");
            licenseNumber = Console.ReadLine();
            try
            {
                dataToPrint = m_CurrentGarage.getFullDataOnVehicle(licenseNumber);
                Console.WriteLine(dataToPrint);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void exit()
        {
            Console.WriteLine("Don't forget close all lights and lock the door after you. Have a nice day");
        }

        private String startMessage()
        {
            String startMessage = String.Format(
    @"Hello dear client
     Here in Tal and Matan workshop we guarantee the best workers and best prices!
     Tell us what would you like to do? ENTER THE RELEAVANT NUMBER
     1 - Insert new car to garage
     2 - Display current license numbers of all vehicles in garage
     3 - Change a vehicle's status
     4 - Add air in vehicle wheels
     5 - Refuel tank in vehicle (only fuel vehicle)
     6 - Recharge battery in vehicle (only electic vehicle)
     7 - Display full data on vehicle
     8 - Exit");

            return startMessage;
        }

        private int getValidOperationCode()
        {
            int operationCode;
            String operationFromClient = Console.ReadLine();
            bool isValidNumber = int.TryParse(operationFromClient, out operationCode);

            while (!isValidNumber || operationCode < 0 || operationCode > 8)
            {
                Console.WriteLine("Invalid option selected. Please press again a number between 0-8\n");
                operationFromClient = Console.ReadLine();
                isValidNumber = int.TryParse(operationFromClient, out operationCode);
            }

            return operationCode;
        }
    }
}
