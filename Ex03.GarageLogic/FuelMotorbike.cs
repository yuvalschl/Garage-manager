using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelMotorbike : Motorbike
    {
        private const float k_MaxFuelCapacity = 7f;
        private const int k_MinEngineVolume = 0;
        private const FuelEngine.eGasType k_FuelMotorbikeGasType = FuelEngine.eGasType.Octan95;
        private int m_EngineVolume;

        public FuelMotorbike(string i_LicenseNumber) : base(new FuelEngine(k_MaxFuelCapacity, k_FuelMotorbikeGasType))
        {
            m_LicenseNumber = i_LicenseNumber;
            s_VehicleQuestions = setFuelMotorbikeQuestions();
        }

        public override Dictionary<string, string> GetDetails()
        {
            Dictionary<string, string> motorbikeDictionary = base.GetDetails();

            motorbikeDictionary.Add("Engine volume", m_EngineVolume.ToString());

            return motorbikeDictionary;
        }

        public override bool CheckUserInput(string i_UserInput, int i_QuestionNumber, out string o_ErrorMessage)
        {
            bool isValidAnswer = base.CheckUserInput(i_UserInput, i_QuestionNumber, out o_ErrorMessage);

            switch (i_QuestionNumber)
            {
                case 8:
                    isValidAnswer = isValidEngineVolume(i_UserInput, out o_ErrorMessage);
                    break;
            }

            return isValidAnswer;
        }

        private bool isValidEngineVolume(string i_UserInput, out string o_ErrorMessage)
        {
            o_ErrorMessage = "No error";
            bool isValidVolume = int.TryParse(i_UserInput, out int engineVolume);

            if (!isValidVolume)
            {
                o_ErrorMessage = "Please enter a number";
            }
            else if (engineVolume < 0)
            {
                o_ErrorMessage = "Engine volume cant be negative";
                isValidVolume = false;
            }

            return isValidVolume;
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }

            set
            {
                if (value <= k_MinEngineVolume)
                {
                    throw new ValueOutOfRangeException("Engine volume must be positive", 0, k_MaxFuelCapacity);
                }
                else
                {
                    m_EngineVolume = value;
                }
            }
        }

        private List<string> setFuelMotorbikeQuestions()
        {
            List<string> fuelMotorbikeQuestions = SetQuestions();
            fuelMotorbikeQuestions.Add("8. Please enter the motorbike engine volume: ");

            return fuelMotorbikeQuestions;
        }

        public override void SetInfo(List<string> i_VehicleInfo)
        {
            base.SetInfo(i_VehicleInfo);
            m_EngineVolume = int.Parse(i_VehicleInfo[7]);
        }
    }
}