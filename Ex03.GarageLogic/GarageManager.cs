using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
     public class GarageManager
     {
          private readonly Dictionary<string, Vehicle> r_AllVehicles;

          public GarageManager()
          {
               r_AllVehicles = new Dictionary<string, Vehicle>();
          }

          public Dictionary<string, Vehicle> AllVehicles
          {
               get
               {
                    return r_AllVehicles;
               }
          }

          public void ChargeVehicle(string i_LicenseNumber, float i_HoursToFill)
          {
               bool vehicleExists = r_AllVehicles.TryGetValue(i_LicenseNumber, out Vehicle requestedVehicle);
               if(vehicleExists)
               {
                    ElectricEngine thisElectricEngine = requestedVehicle.VehicleEngine as ElectricEngine;
                    if(thisElectricEngine != null)
                    {
                         if(thisElectricEngine.MaxEnergyCapacity > thisElectricEngine.CurrentEnergyCapacity)
                         {
                              if(thisElectricEngine.MaxEnergyCapacity
                                 < i_HoursToFill + thisElectricEngine.CurrentEnergyCapacity || i_HoursToFill <= 0)
                              {
                                   float maxAmountToFill = thisElectricEngine.MaxEnergyCapacity
                                                           - thisElectricEngine.CurrentEnergyCapacity;
                                   throw new ValueOutOfRangeException(@"Invalid amount to fill", 0, maxAmountToFill);
                              }

                              thisElectricEngine.ChargeBattery(i_HoursToFill);
                         }
                    }
                    else
                    {
                         throw new KeyNotFoundException(@"This is not an electric vehicle");
                    }
               }
               else
               {
                    throw new KeyNotFoundException(@"This vehicle is not it the garage");
               }
          }

          public void FillTank(string i_LicenseNumber, FuelEngine.eGasType i_GasType, float i_AmountToFill)
          {
               bool vehicleExists = r_AllVehicles.TryGetValue(i_LicenseNumber, out Vehicle requestedVehicle);
               if(vehicleExists)
               {
                    FuelEngine thisFuelEngine = requestedVehicle.VehicleEngine as FuelEngine;
                    if(thisFuelEngine != null)
                    {
                         if(thisFuelEngine.MaxEnergyCapacity > thisFuelEngine.CurrentEnergyCapacity)
                         {
                              if(thisFuelEngine.GasType != i_GasType)
                              {
                                   string errorMessage = string.Format(
                                        @"This car takes {0} fuel",
                                        thisFuelEngine.GasType);
                                   throw new ArgumentException(errorMessage);
                              }
                              else if(thisFuelEngine.MaxEnergyCapacity <= thisFuelEngine.CurrentEnergyCapacity + i_AmountToFill || i_AmountToFill <= 0)
                              {
                                   float maxAmountToFill =
                                        thisFuelEngine.MaxEnergyCapacity - thisFuelEngine.CurrentEnergyCapacity;
                                   throw new ValueOutOfRangeException(@"Invalid amount to fill", 0, maxAmountToFill);
                              }
                              else
                              {
                                   thisFuelEngine.AddFuel(i_AmountToFill, i_GasType);
                              }
                         }
                    }
                    else
                    {
                         throw new KeyNotFoundException(@"This is not an fuel vehicle");
                    }
               }
               else
               {
                    throw new KeyNotFoundException(@"This vehicle is not it the garage");
               }
          }

          public void AddNewVehicleToGarage(Vehicle i_VehicleToAdd, List<string> i_VehicleDetails)
          {
               i_VehicleToAdd.SetInfo(i_VehicleDetails);
               r_AllVehicles.Add(i_VehicleToAdd.LicenseNumber, i_VehicleToAdd);
          }

          public List<string> CreateLicenseNumbersListByState(Vehicle.eVehicleStatus i_SortState)
          {
               List<string> vehiclesCurrentlyInGarageLicenseNumber = new List<string>();

               foreach(KeyValuePair<string, Vehicle> vehicle in r_AllVehicles)
               {
                    if(i_SortState.Equals(vehicle.Value.VehicleStatus))
                    {
                         vehiclesCurrentlyInGarageLicenseNumber.Add(vehicle.Value.LicenseNumber);
                    }
               }

               return vehiclesCurrentlyInGarageLicenseNumber;
          }

          public void ChangeVehicleState(string i_LicenseNumber, Vehicle.eVehicleStatus i_NewStatus)
          {
               bool vehicleExists = r_AllVehicles.TryGetValue(i_LicenseNumber, out Vehicle requestedVehicle);

               if(vehicleExists)
               {
                    requestedVehicle.VehicleStatus = i_NewStatus;
               }
               else
               {
                    throw new ArgumentException("The license number entered is not in the system.");
               }
          }

          public void InflateWheelsToMax(string i_LicenseNumber)
          {
               bool vehicleExists = r_AllVehicles.TryGetValue(i_LicenseNumber, out Vehicle requestedVehicle);

               if(vehicleExists)
               {
                    float amountToInflate = requestedVehicle.VehicleWheelsList[0].MaximalPsi - requestedVehicle.VehicleWheelsList[0].CurrentPsi;
                    requestedVehicle.InflateWheels(amountToInflate);
               }
               else
               {
                    throw new ArgumentException("The license number entered is not in the system.");
               }
          }

          public bool CheckIfVehicleStatusStringValid(string i_StatusToCheck)
          {
               return i_StatusToCheck == "1" || i_StatusToCheck == "2" || i_StatusToCheck == "3";
          }
     }
}