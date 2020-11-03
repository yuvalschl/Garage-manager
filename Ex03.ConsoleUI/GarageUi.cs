using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
     public class GarageUi
     {
          private readonly GarageManager r_CurrentGarage;

          public void RunProgram()
          {
               eGarageActions garageAction;
               Console.WriteLine(@"Welcome to the garage, please choose an action:");
               do
               {
                    showMainMenu();
                    garageAction = getGarageAction();
                    Console.Clear();
                    if((garageAction != eGarageActions.AddVehicle && r_CurrentGarage.AllVehicles.Count == 0) && garageAction != eGarageActions.Quit)
                    {
                         Console.WriteLine(
                              @"The garage is empty
You can add a new vehicle");
                    }
                    else
                    {
                         string vehicleLicenseNumber;
                         if(garageAction != eGarageActions.ShowVehiclesLicenseNumbers
                            && garageAction != eGarageActions.Quit)
                         {
                              Console.WriteLine(@"Please enter the vehicle license number");
                              vehicleLicenseNumber = getLicenseNumber();
                         }
                         else
                         {
                              vehicleLicenseNumber = null;
                         }

                         switch(garageAction)
                         {
                              case eGarageActions.AddVehicle:
                                   addVehicleToGarage(vehicleLicenseNumber);
                                   Console.WriteLine(Environment.NewLine);
                                   break;
                              case eGarageActions.ShowVehiclesLicenseNumbers:
                                   showVehiclesCurrentlyInTheGarage();
                                   break;
                              case eGarageActions.ChangeState:
                                   changeVehicleState(vehicleLicenseNumber);
                                   break;
                              case eGarageActions.InflateAir:
                                   inflateAirUi(vehicleLicenseNumber);
                                   break;
                              case eGarageActions.FuelVehicle:
                                   if(checkIfVehicleWithEngineIsInGarage(typeof(FuelEngine).ToString()))
                                   {
                                        fuelVehicleUi(vehicleLicenseNumber);
                                   }
                                   else
                                   {
                                        Console.WriteLine(@"There are no fuel vehicles in the garage");
                                        Console.WriteLine(@"Press any key to continue");
                                        Console.ReadLine();
                                   }

                                   break;
                              case eGarageActions.ChargeVehicle:
                                   if(checkIfVehicleWithEngineIsInGarage(typeof(ElectricEngine).ToString()))
                                   {
                                        chargeVehicleUi(vehicleLicenseNumber);
                                   }
                                   else
                                   {
                                        Console.WriteLine(@"There are no electric vehicles in the garage");
                                        Console.WriteLine(@"Press any key to continue");
                                        Console.ReadLine();
                                   }

                                   break;
                              case eGarageActions.ShowVehicleDetails:
                                   showVehicleDetails(vehicleLicenseNumber);
                                   break;
                              case eGarageActions.Quit:
                                   Environment.Exit(1);
                                   break;
                              default:
                                   Console.WriteLine(@"Invalid selection, try again");
                                   break;
                         }

                         Console.Clear();
                    }
               }
               while(garageAction != eGarageActions.Quit);
          }

          public GarageUi()
          {
               r_CurrentGarage = new GarageManager();
          }

          private void inflateAirUi(string i_LicenseNumber)
          {
               bool successfulChange = false;

               do
               {
                    try
                    {
                         while(!r_CurrentGarage.AllVehicles.ContainsKey(i_LicenseNumber))
                         {
                              Console.WriteLine(@"This vehicle is not in the garage, please enter a valid License Number:
Please chose license number form list");
                              showVehiclesCurrentlyInTheGarage();
                              i_LicenseNumber = getLicenseNumber();
                         }

                         r_CurrentGarage.InflateWheelsToMax(i_LicenseNumber);
                         successfulChange = true;
                    }
                    catch(Exception exception)
                    {
                         Console.WriteLine(exception.Message);
                    }
               }
               while(!successfulChange);
          }

          private void changeVehicleState(string i_LicenseNumber)
          {
               bool successfulChange = false;

               do
               {
                    try
                    {
                         while(!r_CurrentGarage.AllVehicles.ContainsKey(i_LicenseNumber))
                         {
                              Console.WriteLine(@"This vehicle is not in the garage, please enter a valid License Number:
Please chose license number form list");
                              showVehiclesCurrentlyInTheGarage();
                              i_LicenseNumber = getLicenseNumber();
                         }

                         string vehicleStates = string.Format(
                              @"Please select state
In repair press 1
Repaired  press 2
Paid      press 3");

                         Console.WriteLine(vehicleStates);

                         string vehicleStateString = Console.ReadLine();
                         int.TryParse(vehicleStateString, out int vehicleState);

                         while(vehicleStateString == null || !Enum.IsDefined(typeof(Vehicle.eVehicleStatus), vehicleState))
                         {
                              Console.WriteLine(@"Please enter a valid state");
                              vehicleStateString = Console.ReadLine();
                              int.TryParse(vehicleStateString, out vehicleState);
                         }

                         Vehicle.eVehicleStatus vehicleStatus = (Vehicle.eVehicleStatus)Enum.Parse(
                              typeof(Vehicle.eVehicleStatus),
                              vehicleStateString);
                         r_CurrentGarage.ChangeVehicleState(i_LicenseNumber, vehicleStatus);
                         successfulChange = true;
                    }
                    catch(Exception exception)
                    {
                         Console.WriteLine(exception.Message);
                    }
               }
               while(!successfulChange);
          }

          private void chargeVehicleUi(string i_LicenseNumber)
          {
               bool isValidInput = false;

               do
               {
                    try
                    {
                         while(!r_CurrentGarage.AllVehicles.ContainsKey(i_LicenseNumber))
                         {
                              Console.WriteLine(@"This vehicle is not in the garage, please enter a valid License Number
Please chose a vehicle from the list:");
                              printVehiclesByEngineType(typeof(ElectricEngine).ToString());
                              i_LicenseNumber = getLicenseNumber();
                         }

                         Console.WriteLine("Please enter how many hour you want to charge battery");
                         string hoursToFillString = Console.ReadLine();
                         float hoursToFill;

                         while(!float.TryParse(hoursToFillString, out hoursToFill))
                         {
                              Console.WriteLine("Please enter a number");
                              hoursToFillString = Console.ReadLine();
                         }

                         r_CurrentGarage.ChargeVehicle(i_LicenseNumber, hoursToFill);
                         isValidInput = true;
                    }
                    catch(KeyNotFoundException keyNotFound)
                    {
                         Console.WriteLine(keyNotFound.Message);
                         isValidInput = true;
                    }
                    catch(Exception exception)
                    {
                         Console.WriteLine(exception.Message);
                    }
               }
               while(!isValidInput);
          }

          private void fuelVehicleUi(string i_LicenseNumber)
          {
               bool isValidInput = false;
               do
               {
                    try
                    {
                         while(!r_CurrentGarage.AllVehicles.ContainsKey(i_LicenseNumber))
                         {
                              Console.WriteLine(@"This vehicle is not in the garage, please enter a valid License Number
Please chose a vehicle from the list:");
                              printVehiclesByEngineType(typeof(FuelEngine).ToString());
                              i_LicenseNumber = getLicenseNumber();
                         }

                         Console.WriteLine("Please enter how many litters to fill");
                         string littersToFillString = Console.ReadLine();
                         float littersToFill;

                         while(!float.TryParse(littersToFillString, out littersToFill))
                         {
                              Console.WriteLine("Please enter a number");
                              littersToFillString = Console.ReadLine();
                         }

                         Console.WriteLine(
                              @"Please enter the full type to fill
Octan 95 press 1
Octan 96 press 2
Octan 98 press 3
Soler    press 4");
                         string fuelTypeString = Console.ReadLine();
                         int.TryParse(fuelTypeString, out int fuelType);

                         while(fuelTypeString == null || !Enum.IsDefined(typeof(FuelEngine.eGasType), fuelType))
                         {
                              Console.WriteLine(@"Please enter a valid fuel type");
                              fuelTypeString = Console.ReadLine();
                              int.TryParse(fuelTypeString, out fuelType);
                         }

                         FuelEngine.eGasType gasTypeToFill = (FuelEngine.eGasType)Enum.Parse(
                              typeof(FuelEngine.eGasType),
                              fuelTypeString);

                         r_CurrentGarage.FillTank(i_LicenseNumber, gasTypeToFill, littersToFill);
                         isValidInput = true;
                    }
                    catch(KeyNotFoundException keyNotFound)
                    {
                         Console.WriteLine(keyNotFound.Message);
                         isValidInput = true;
                    }
                    catch(Exception exception)
                    {
                         Console.WriteLine(exception.Message);
                    }
               }
               while(!isValidInput);
          }

          private void addVehicleToGarage(string i_VehicleLicenseNumber)
          {
               if(r_CurrentGarage.AllVehicles.ContainsKey(i_VehicleLicenseNumber))
               {
                    r_CurrentGarage.ChangeVehicleState(i_VehicleLicenseNumber, Vehicle.eVehicleStatus.InRepair);
                    Console.WriteLine(@"This vehicle is already in the system. 
The status changed into 'In repair'.
Press any key to continue");
                    Console.ReadLine();
               }
               else
               {
                    showAvailableVehiclesTypes();
                    string vehicleTypePickString = Console.ReadLine();
                    while(vehicleTypePickString == null)
                    {
                         Console.WriteLine(@"Please enter a valid type");
                         vehicleTypePickString = Console.ReadLine();
                    }

                    VehicleMaker.eVehicleType vehicleTypePick = (VehicleMaker.eVehicleType)Enum.Parse(
                         typeof(VehicleMaker.eVehicleType),
                         vehicleTypePickString);
                    Vehicle newVehicle = VehicleMaker.CreateNewVehicle(vehicleTypePick, i_VehicleLicenseNumber);
                    List<string> vehicleAnswers = getVehicleDetails(newVehicle);

                    r_CurrentGarage.AddNewVehicleToGarage(newVehicle, vehicleAnswers);
                    Console.WriteLine(Environment.NewLine);
               }
          }

          private List<string> getVehicleDetails(Vehicle i_VehicleToGetDetails)
          {
               List<string> vehicleQuestions = i_VehicleToGetDetails.VehicleQuestions;
               List<string> vehicleAnswers = new List<string>(9);
               int questionsNumber = 1;

               foreach(string question in vehicleQuestions)
               {
                    Console.WriteLine(question);
                    string userAnswer = Console.ReadLine();
                    bool isAnswerValid = i_VehicleToGetDetails.CheckUserInput(userAnswer, questionsNumber, out string errorMessage);

                    while(!isAnswerValid)
                    {
                         Console.WriteLine(errorMessage);
                         userAnswer = Console.ReadLine();
                         isAnswerValid = i_VehicleToGetDetails.CheckUserInput(userAnswer, questionsNumber, out errorMessage);
                    }

                    questionsNumber++;
                    vehicleAnswers.Add(userAnswer);
               }

               return vehicleAnswers;
          }

          private string getLicenseNumber()
          {
               string licenseNumber = Console.ReadLine();

               while(licenseNumber == null || licenseNumber.Length < 7 || licenseNumber.Length > 8)
               {
                    Console.WriteLine(@"Please enter a valid number");
                    licenseNumber = Console.ReadLine();
               }

               foreach(char digit in licenseNumber)
               {
                    if(!char.IsDigit(digit))
                    {
                         Console.WriteLine(@"License number must contain numbers only");
                         getLicenseNumber();
                    }
               }

               return licenseNumber;
          }

          private void showAvailableVehiclesTypes()
          {
               int pickNumberForPrinting = 1;
               Console.WriteLine(@"Please choose the type of vehicle to add:");
               foreach(string vehicle in VehicleMaker.AvailableVehiclesList)
               {
                    string vehicleToAddMessage = string.Format(
                         @"To add {0}, please press {1}",
                         vehicle,
                         pickNumberForPrinting++);

                    Console.WriteLine(vehicleToAddMessage);
               }
          }

          private void showMainMenu()
          {
               string mainMenuMessage = string.Format(
                    @"Add a new vehicle to the garage         press 1
Show vehicles currently in the garage   press 2
Change the state of a vehicle           press 3
Inflate vehicles wheels to the max      press 4
Fuel a fuel engine vehicle.             press 5
Charge an electric vehicle              press 6
Show vehicle details                    press 7
To quit                                 press 8");

               Console.WriteLine(mainMenuMessage);
          }

          private void showVehiclesCurrentlyInTheGarage()
          {
               List<string> licenseNumbersToPrint;
               Console.WriteLine(
                    @"Please select the vehicles status:
In repair press 1
Repaired press 2
Paid press 3
All vehicles press 4");
               string vehicleStatusToPrint = Console.ReadLine();
               if(vehicleStatusToPrint != null && r_CurrentGarage.CheckIfVehicleStatusStringValid(vehicleStatusToPrint))
               {
                    Vehicle.eVehicleStatus statusToPrint = (Vehicle.eVehicleStatus)Enum.Parse(
                         typeof(Vehicle.eVehicleStatus),
                         vehicleStatusToPrint);
                    licenseNumbersToPrint = r_CurrentGarage.CreateLicenseNumbersListByState(statusToPrint);
               }
               else if(vehicleStatusToPrint == "4")
               {
                    licenseNumbersToPrint = r_CurrentGarage.AllVehicles.Keys.ToList();
               }
               else
               {
                    throw new ArgumentException(@"Invalid status entered");
               }

               foreach(string licenseNumber in licenseNumbersToPrint)
               {
                    Console.WriteLine(licenseNumber);
               }

               Console.WriteLine(@"Press any key to continue");
               Console.ReadLine();
          }

          private eGarageActions getGarageAction()
          {
               string actionChosen = Console.ReadLine();
               int.TryParse(actionChosen, out int actionInt);

               while(actionChosen == null || !Enum.IsDefined(typeof(eGarageActions), actionInt))
               {
                    Console.WriteLine(@"Invalid selection, try again.");
                    actionChosen = Console.ReadLine();
                    int.TryParse(actionChosen, out actionInt);
               }

               return (eGarageActions)Enum.Parse(typeof(eGarageActions), actionChosen);
          }

          private void showVehicleDetails(string i_LicenseNumber)
          {
               bool vehicleExists = r_CurrentGarage.AllVehicles.TryGetValue(i_LicenseNumber, out Vehicle requestedVehicle);

               if(vehicleExists)
               {
                    Dictionary<string, string> vehicleDetails = requestedVehicle.GetDetails();
                    foreach(KeyValuePair<string, string> detail in vehicleDetails)
                    {
                         string vehicleDetail = string.Format(@"{0} - {1}", detail.Key, detail.Value);

                         Console.WriteLine(vehicleDetail);
                    }

                    Console.WriteLine(@"Press any key to continue");
                    Console.ReadLine();
               }
               else
               {
                    throw new KeyNotFoundException(@"This vehicle was not found");
               }
          }

          private void printVehiclesByEngineType(string i_EngineType)
          {
               foreach(Vehicle vehicle in r_CurrentGarage.AllVehicles.Values)
               {
                    if(vehicle.VehicleEngine.GetType().ToString() == i_EngineType) 
                    {
                         Console.WriteLine(vehicle.LicenseNumber);
                    }
               }
          }

          private bool checkIfVehicleWithEngineIsInGarage(string i_EngineType)
          {
               bool vehicleInGarage = false;

               foreach(Vehicle vehicle in r_CurrentGarage.AllVehicles.Values)
               {
                    if(vehicle.VehicleEngine.GetType().ToString() == i_EngineType)
                    {
                         vehicleInGarage = true;
                    }
               }

               return vehicleInGarage;
          }

          public enum eGarageActions
          {
               AddVehicle = 1,
               ShowVehiclesLicenseNumbers = 2,
               ChangeState = 3,
               InflateAir = 4,
               FuelVehicle = 5,
               ChargeVehicle = 6,
               ShowVehicleDetails = 7,
               Quit = 8
          }
     }
}