using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    { 
        protected static List<string> s_VehicleQuestions;
        protected readonly float r_MaxWheelsPsi;
        protected OwnerInfo m_OwnerInfo;
        protected eVehicleStatus m_VehicleStatus;
        protected Engine m_VehicleEngine;
        protected List<Wheel> m_WheelsList;
        protected string m_ModelName;
        protected string m_LicenseNumber;

        protected Vehicle(Engine i_Engine, float i_MaxWheelsPsi)
        {
            m_VehicleStatus = eVehicleStatus.InRepair;
            m_VehicleEngine = i_Engine;
            r_MaxWheelsPsi = i_MaxWheelsPsi;
            OwnerInfo = new OwnerInfo();
            s_VehicleQuestions = setQuestions();
        }

        private static List<string> setQuestions()
        {
            List<string> vehicleQuestions = new List<string>();

            vehicleQuestions.Add("1. Please enter owner name:");
            vehicleQuestions.Add("2. Please enter owner phone number:");
            vehicleQuestions.Add("3. Please enter the model of the vehicle");
            vehicleQuestions.Add("4. Please enter how much fuel(Liters) / electricity(Hours) left:");
            vehicleQuestions.Add("5. Please enter the wheels manufacturer:");
            vehicleQuestions.Add("6. Please enter the current PSI:");

            return vehicleQuestions;
        }

        public virtual Dictionary<string, string> GetDetails()
        {
            Dictionary<string, string> vehicleDetails = new Dictionary<string, string>();
            vehicleDetails.Add("Owner name", OwnerInfo.OwnerOfVehicleName);
            vehicleDetails.Add("Owner phone number", OwnerInfo.OwnerPhoneNumber);
            vehicleDetails.Add("License number", m_LicenseNumber);
            vehicleDetails.Add("Model name", m_ModelName);
            FuelEngine thisFuelEngine = m_VehicleEngine as FuelEngine;
            if (thisFuelEngine != null)
            {
                vehicleDetails.Add("Engine type", "Fuel engine");
                vehicleDetails.Add("Fuel capacity", m_VehicleEngine.MaxEnergyCapacity.ToString("F"));
                vehicleDetails.Add("Current amount of fuel", m_VehicleEngine.CurrentEnergyCapacity.ToString("F"));
                vehicleDetails.Add("Fuel type", thisFuelEngine.GasType.ToString());
            }
            else
            {
                vehicleDetails.Add("Engine type", "Electric engine");
                vehicleDetails.Add("Battery capacity", m_VehicleEngine.MaxEnergyCapacity.ToString("F"));
                vehicleDetails.Add("Current amount of battery", m_VehicleEngine.CurrentEnergyCapacity.ToString("F"));
            }

            vehicleDetails.Add("Vehicle status", m_VehicleStatus.ToString());
            vehicleDetails.Add("Wheels manufacturer", m_WheelsList[0].ManufacturerName);
            vehicleDetails.Add("Number of Wheels", m_WheelsList.Count.ToString());
            vehicleDetails.Add("Maximum PSI", m_WheelsList[0].MaximalPsi.ToString("F"));
            vehicleDetails.Add("Current PSI", m_WheelsList[0].CurrentPsi.ToString("F"));

            return vehicleDetails;
        }

        public virtual bool CheckUserInput(string i_UserInput, int i_QuestionNumber, out string o_ErrorMessage)
        {
            bool isValiAnswer;

            o_ErrorMessage = "No error";
            switch (i_QuestionNumber)
            {
                case 1:
                    isValiAnswer = nameCheck(i_UserInput, out o_ErrorMessage);
                    break;
                case 2:
                    isValiAnswer = checkPhoneNumber(i_UserInput, out o_ErrorMessage);
                    break;
                case 3:
                    isValiAnswer = checkModelOrManufacturer(i_UserInput, out o_ErrorMessage);
                    break;
                case 4:
                    isValiAnswer = checkEnergy(i_UserInput, out o_ErrorMessage);
                    break;
                case 5:
                    isValiAnswer = checkModelOrManufacturer(i_UserInput, out o_ErrorMessage);
                    break;
                case 6:
                    isValiAnswer = checkPsi(i_UserInput, out o_ErrorMessage);
                    break;
                default:
                    isValiAnswer = true;
                    break;
            }

            return isValiAnswer;
        }

        private bool checkEnergy(string i_UserInput, out string o_ErrorMessage)
        {
            bool isNumber = float.TryParse(i_UserInput, out float currentEnergy);
            bool isValidEnergy = true;

            o_ErrorMessage = "No error";

            if (isNumber)
            {
                if (currentEnergy > this.m_VehicleEngine.MaxEnergyCapacity)
                {
                    isValidEnergy = false;
                    string errorMessage = string.Format(@"Current energy cant be bigger then {0}", m_VehicleEngine.MaxEnergyCapacity);
                    o_ErrorMessage = errorMessage;
                }
                else if (currentEnergy < 0)
                {
                    isValidEnergy = false;
                    o_ErrorMessage = "Current energy cant be negative";
                }
            }
            else
            {
                isValidEnergy = false;
            }

            return isValidEnergy;
        }

        private bool checkPsi(string i_UserInput, out string o_ErrorMessage)
        {
            bool isNumber = float.TryParse(i_UserInput, out float currentPsi);
            bool isValidPsi = true;

            o_ErrorMessage = "No error";

            if (isNumber)
            {
                if (currentPsi > r_MaxWheelsPsi)
                {
                    isValidPsi = false;
                    string errorMessage = string.Format("current PSI cant be bigger then {0}", r_MaxWheelsPsi);
                    o_ErrorMessage = errorMessage;
                }
                else if (currentPsi < 0)
                {
                    isValidPsi = false;
                    o_ErrorMessage = "Current PSI cant be negative";
                }
            }
            else
            {
                isValidPsi = false;
            }

            return isValidPsi;
        }

        private bool checkModelOrManufacturer(string i_UserInput, out string o_ErrorMessage)
        {
            bool isValid = true;

            o_ErrorMessage = "No error";
            if (string.IsNullOrEmpty(i_UserInput))
            {
                o_ErrorMessage = "Please enter a name";
                isValid = false;
            }
            else if (i_UserInput[0] == ' ')
            {
                o_ErrorMessage = "Model name cant start with a space";
                isValid = false;
            }

            return isValid;
        }

        private bool nameCheck(string i_UserInput, out string o_ErrorMessage)
        {
            bool isValid = true;

            o_ErrorMessage = "No error";
            if (string.IsNullOrEmpty(i_UserInput))
            {
                o_ErrorMessage = "Please enter a name";
                isValid = false;
            }
            else if (i_UserInput[0] == ' ')
            {
                o_ErrorMessage = "Name cant start with a space";
                isValid = false;
            }

            if(i_UserInput != null)
            {
                 foreach(char letter in i_UserInput)
                 {
                      if(!char.IsLetter(letter) && letter != ' ')
                      {
                           o_ErrorMessage = "Name must contain letters only";
                           isValid = false;
                      }
                 }
            }

            return isValid;
        }

        private bool checkPhoneNumber(string i_PhoneNumberToCheck, out string o_ErrorMessage)
        {
            bool isValid = true;

            o_ErrorMessage = "No error";

            if (i_PhoneNumberToCheck.Length != 10)
            {
                o_ErrorMessage = "Phone number must be 10 digits long";
                isValid = false;
            }

            foreach (char digit in i_PhoneNumberToCheck)
            {
                if (!char.IsDigit(digit))
                {
                    o_ErrorMessage = "Phone number must contain digits only";
                    isValid = false;
                }
            }

            return isValid;
        }

        public List<string> VehicleQuestions
        {
            get
            {
                return s_VehicleQuestions;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                 m_ModelName = value;
            }
        }

        public OwnerInfo OwnerInfo
        {
            get
            {
                return m_OwnerInfo;
            }

            set
            {
                m_OwnerInfo = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                if (!Enum.IsDefined(typeof(eVehicleStatus), value))
                {
                    throw new FormatException("Invalid vehicle status");
                }
                else
                {
                    m_VehicleStatus = value;
                }
            }
        }

        public Engine VehicleEngine
        {
            get
            {
                return m_VehicleEngine;
            }

            set
            {
                m_VehicleEngine = value;
            }
        }

        public List<Wheel> VehicleWheelsList
        {
            get
            {
                return m_WheelsList;
            }

            set
            {
                m_WheelsList = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public virtual void SetInfo(List<string> i_VehicleInfo)
        {
            m_OwnerInfo.OwnerOfVehicleName = i_VehicleInfo[0];
            m_OwnerInfo.OwnerPhoneNumber = i_VehicleInfo[1];
            m_ModelName = i_VehicleInfo[2];
            m_VehicleEngine.CurrentEnergyCapacity = setCurrentEnergy(i_VehicleInfo[3]);
        }

        private float setCurrentEnergy(string i_CurrentEnergyToSet)
        {
            if (!float.TryParse(i_CurrentEnergyToSet, out float currentEnergyAsFloat))
            {
                throw new ArgumentException(@"current energy must be a number");
            }

            if (currentEnergyAsFloat < 0)
            {
                throw new ArgumentException(@"current energy cant be negative");
            }

            return currentEnergyAsFloat;
        }

          public void InflateWheels(float i_AmountToInflate)
          {
               foreach(Wheel wheel in m_WheelsList)
               {
                    wheel.InflateWheel(i_AmountToInflate);
               }
          }

          public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired = 2,
            Paid = 3,
        }
    }
}