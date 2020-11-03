using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Motorbike : Vehicle
    {
         private const float k_MaxPsi = 30f;
        private const int k_NumberOfWheels = 2;
        private eLicenseType m_LicenseType;

        protected Motorbike(Engine i_Engine) : base(i_Engine, k_MaxPsi)
        {
        }

        protected List<string> SetQuestions()
        {
            List<string> motorbikeQuestions = new List<string>();

            foreach (string question in Vehicle.s_VehicleQuestions)
            {
                motorbikeQuestions.Add(question);
            }

            motorbikeQuestions.Add("7. Please enter the motorbike license type: A, A1, AA or B: ");

            return motorbikeQuestions;
        }

        public override Dictionary<string, string> GetDetails()
        {
            Dictionary<string, string> motorbikeDetails = base.GetDetails();

            motorbikeDetails.Add("License type", m_LicenseType.ToString());

            return motorbikeDetails;
        }

        public override bool CheckUserInput(string i_UserInput, int i_QuestionNumber, out string o_ErrorMessage)
        {
            bool isValidAnswer = base.CheckUserInput(i_UserInput, i_QuestionNumber, out o_ErrorMessage);

            switch (i_QuestionNumber)
            {
                case 7:
                    isValidAnswer = isLicenseTypeValid(i_UserInput, out o_ErrorMessage);
                    break;
            }

            return isValidAnswer;
        }

        private bool isLicenseTypeValid(string i_UserInput, out string o_ErrorMessage)
        {
            o_ErrorMessage = "No error";
            if(i_UserInput == "AA")
            {
                 i_UserInput = "Aa";
            }

            bool isValidLicenseType = Enum.IsDefined(typeof(eLicenseType), i_UserInput);

            if (!isValidLicenseType)
            {
                o_ErrorMessage = "Please enter a valid license type";
            }

            return isValidLicenseType;
        }

        public override void SetInfo(List<string> i_VehicleInfo)
        {
            base.SetInfo(i_VehicleInfo);
            m_WheelsList = Wheel.SetWheels(i_VehicleInfo[4], i_VehicleInfo[5], k_MaxPsi, k_NumberOfWheels);
            m_LicenseType = checkLicenseType(i_VehicleInfo[6]);
        }

        private eLicenseType checkLicenseType(string i_LicenseType)
        {
            eLicenseType licenseType;

            switch (i_LicenseType.ToUpper())
            {
                case "A":
                    licenseType = eLicenseType.A;
                    break;
                case "B":
                    licenseType = eLicenseType.B;
                    break;
                case "A1":
                    licenseType = eLicenseType.A1;
                    break;
                case "AA":
                    licenseType = eLicenseType.Aa;
                    break;
                default:
                    throw new ArgumentException(@"{0} is not a valid license type", i_LicenseType);
            }

            return licenseType;
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                if (!Enum.IsDefined(typeof(eLicenseType), value))
                {
                    throw new FormatException("Invalid license type");
                }
                else
                {
                    m_LicenseType = value;
                }
            }
        }

        public enum eLicenseType
        {
            A = 1,
            A1 = 2,
            Aa = 3,
            B = 4
        }
    }
}