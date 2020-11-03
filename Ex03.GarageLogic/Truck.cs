using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_MinTruckCapacity = 0;
        private const float k_MaxFuelCapacity = 120f;
        private const float k_MaxPsi = 28;
        private const int k_NumberOfWheels = 16;
        private const FuelEngine.eGasType k_FuelTruckGasType = FuelEngine.eGasType.Soler;
        private bool m_IsCarryingDangerousMaterials;
        private float m_TruckCapacity;

        public Truck(string i_LicenseNumber) : base(new FuelEngine(k_MaxFuelCapacity, k_FuelTruckGasType), k_MaxPsi)
        {
            m_LicenseNumber = i_LicenseNumber;
            s_VehicleQuestions = setQuestions();
        }

        public override Dictionary<string, string> GetDetails()
        {
            Dictionary<string, string> truckDetails = base.GetDetails();
            truckDetails.Add("Ability to carry dangerous materials", m_IsCarryingDangerousMaterials.ToString());
            truckDetails.Add("Truck capacity", m_TruckCapacity.ToString("F"));

            return truckDetails;
        }

        public override bool CheckUserInput(string i_UserInput, int i_QuestionNumber, out string o_ErrorMessage)
        {
            bool isValidAnswer = base.CheckUserInput(i_UserInput, i_QuestionNumber, out o_ErrorMessage);

            switch (i_QuestionNumber)
            {
                case 7:
                    isValidAnswer = checkCarryingDangerousMaterialsAnswerIsValid(i_UserInput, out o_ErrorMessage);
                    break;
                case 8:
                    isValidAnswer = checkIfCarryingCapacityIsValid(i_UserInput, out o_ErrorMessage);
                    break;
            }

            return isValidAnswer;
        }

        private bool checkIfCarryingCapacityIsValid(string i_UserInput, out string o_ErrorMessage)
        {
            bool isValidCarryingCapacity = float.TryParse(i_UserInput, out float carryingCapacity);

            o_ErrorMessage = "No error";
            if (!isValidCarryingCapacity)
            {
                o_ErrorMessage = "Please enter a number";
            }
            else if (carryingCapacity < 0)
            {
                o_ErrorMessage = "Carrying capacity cant be negative";
                isValidCarryingCapacity = false;
            }

            return isValidCarryingCapacity;
        }

        private bool checkCarryingDangerousMaterialsAnswerIsValid(string i_UserInput, out string o_ErrorMessage)
        {
            bool isValidAnswer = i_UserInput == "Y" || i_UserInput == "N";

            o_ErrorMessage = "No error";
            if (!isValidAnswer)
            {
                o_ErrorMessage = "Please enter Y/N";
            }

            return isValidAnswer;
        }

        public override void SetInfo(List<string> i_VehicleInfo)
        {
            base.SetInfo(i_VehicleInfo);
            m_WheelsList = Wheel.SetWheels(i_VehicleInfo[4], i_VehicleInfo[5], k_MaxPsi, k_NumberOfWheels);
            m_IsCarryingDangerousMaterials = setIfTruckCarryingDangerousMaterials(i_VehicleInfo[6]);
            m_TruckCapacity = float.Parse(i_VehicleInfo[7]);
        }

        private bool setIfTruckCarryingDangerousMaterials(string i_IsCarryingDangerousMaterials)
        {
            bool carryingDangerousMaterials;
            if (i_IsCarryingDangerousMaterials == "Y")
            {
                carryingDangerousMaterials = true;
            }
            else if (i_IsCarryingDangerousMaterials == "N")
            {
                carryingDangerousMaterials = false;
            }
            else
            {
                throw new ArgumentException(@"The value entered at the carrying dangerous materials should be Y/N");
            }

            return carryingDangerousMaterials;
        }

        private List<string> setQuestions()
        {
            List<string> truckQuestions = new List<string>();

            foreach (string question in Vehicle.s_VehicleQuestions)
            {
                truckQuestions.Add(question);
            }

            truckQuestions.Add("7. Please enter if the truck is carrying dangerous materials Y/N");
            truckQuestions.Add("8. Please enter the truck carrying capacity");

            return truckQuestions;
        }

        public bool IsCarryingDangerousMaterials
        {
            get
            {
                return m_IsCarryingDangerousMaterials;
            }

            set
            {
                m_IsCarryingDangerousMaterials = value;
            }
        }

        public float TruckCapacity
        {
            get
            {
                return m_TruckCapacity;
            }

            set
            {
                if (value < k_MinTruckCapacity)
                {
                    throw new ArgumentException("Truck capacity cant be negative");
                }
                else
                {
                    m_TruckCapacity = value;
                }
            }
        }
    }
}