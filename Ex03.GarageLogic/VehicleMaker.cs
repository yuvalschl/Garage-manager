using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleMaker
    {
        private static readonly List<string> sr_AvailableVehiclesList;

        static VehicleMaker()
        {
            sr_AvailableVehiclesList = new List<string>
               {
                    "Fuel car",
                    "Electric car",
                    "Fuel bike",
                    "Electric bike",
                    "Truck"
               };
        }

        public static List<string> AvailableVehiclesList
        {
            get
            {
                return sr_AvailableVehiclesList;
            }
        }

        public static Vehicle CreateNewVehicle(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle newVehicle;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    newVehicle = new FuelCar(i_LicenseNumber);
                    break;
                case eVehicleType.ElectricCar:
                    newVehicle = new ElectricCar(i_LicenseNumber);
                    break;
                case eVehicleType.FuelMotorbike:
                    newVehicle = new FuelMotorbike(i_LicenseNumber);
                    break;
                case eVehicleType.ElectricMotorbike:
                    newVehicle = new ElectricMotorbike(i_LicenseNumber);
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck(i_LicenseNumber);
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }

            return newVehicle;
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar = 2,
            FuelMotorbike = 3,
            ElectricMotorbike = 4,
            Truck = 5,
        }
    }
}