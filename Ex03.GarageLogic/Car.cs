using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    { 
         private const int k_MaxNumberOfDoors = 5;
        private const int k_MinNumberOfDoors = 2;
        private const int k_NumberOfWheels = 4;
        private const float k_MaxWheelsPsi = 32;
        private eCarColor m_CarColor;
        private int m_NumberOfDoors;

        protected Car(Engine i_Engine) : base(i_Engine, k_MaxWheelsPsi)
        {
        }

        public override Dictionary<string, string> GetDetails()
        {
            Dictionary<string, string> carDetails = base.GetDetails();
            carDetails.Add("Car color", m_CarColor.ToString());
            carDetails.Add("Number of doors", m_NumberOfDoors.ToString());

            return carDetails;
        }

        public override bool CheckUserInput(string i_UserInput, int i_QuestionNumber, out string o_ErrorMessage)
        {
            bool isValidAnswer = base.CheckUserInput(i_UserInput, i_QuestionNumber, out o_ErrorMessage);

            switch (i_QuestionNumber)
            {
                case 7:
                    isValidAnswer = checkCarColor(i_UserInput, out o_ErrorMessage);
                    break;
                case 8:
                    isValidAnswer = checkNumberOfDoors(i_UserInput, out o_ErrorMessage);
                    break;
            }

            return isValidAnswer;
        }

        private bool checkNumberOfDoors(string i_UserInput, out string o_ErrorMessage)
        {
            bool isNumber = int.TryParse(i_UserInput, out int numberOfDoors);
            bool isValidNumberOfDoors = true;

            o_ErrorMessage = "No Error";

            if (isNumber)
            {
                if (numberOfDoors < 2 || numberOfDoors > 5)
                {
                    o_ErrorMessage = "Invalid number of doors";
                    isValidNumberOfDoors = false;
                }
            }
            else
            {
                o_ErrorMessage = "Please enter a number";
                isValidNumberOfDoors = false;
            }

            return isValidNumberOfDoors;
        }

        private bool checkCarColor(string i_UserInput, out string o_ErrorMessage)
        {
            bool isValid = Enum.IsDefined(typeof(eCarColor), i_UserInput);

            o_ErrorMessage = "No error";

            if (!isValid)
            {
                o_ErrorMessage = "Cant chose this color";
            }

            return isValid;
        }

        protected List<string> SetQuestions()
        {
            List<string> carQuestions = new List<string>();
            foreach (string question in Vehicle.s_VehicleQuestions)
            {
                carQuestions.Add(question);
            }

            carQuestions.Add("7. Please enter the color of the car: Red, White, Black or Silver");
            carQuestions.Add("8. Please enter the car number of doors: 2,3,4 or 5");

            return carQuestions;
        }

        public override void SetInfo(List<string> i_VehicleInfo)
        {
            base.SetInfo(i_VehicleInfo);
            m_WheelsList = Wheel.SetWheels(i_VehicleInfo[4], i_VehicleInfo[5], k_MaxWheelsPsi, k_NumberOfWheels);
            m_CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_VehicleInfo[6]);
            m_NumberOfDoors = int.Parse(i_VehicleInfo[7]);
        }

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                if (!Enum.IsDefined(typeof(eCarColor), value))
                {
                    throw new FormatException("invalid car color");
                }
                else
                {
                    m_CarColor = value;
                }
            }
        }

        public int NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                if (value < 2 || value > 5)
                {
                    throw new ValueOutOfRangeException(value.ToString(), k_MinNumberOfDoors, k_MaxNumberOfDoors);
                }

                if(value.GetTypeCode() != TypeCode.Int32)
                {
                     throw new FormatException(@"Please enter a number");
                }
                else
                {
                    NumberOfDoors = value;
                }
            }
        }

        public enum eCarColor
        {
            Red = 0,
            White = 1,
            Black = 2,
            Silver = 3,
        }
    }
}