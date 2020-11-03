using System;

namespace Ex03.GarageLogic
{
    public class OwnerInfo
    {
        private string m_OwnerOfVehicleName;
        private string m_OwnerPhoneNumber;

        public string OwnerOfVehicleName
        {
            get
            {
                return m_OwnerOfVehicleName;
            }

            set
            {
                foreach (char letter in value)
                {
                    if (!char.IsLetter(letter) && !char.IsWhiteSpace(letter))
                    {
                        throw new FormatException(@"Owner name must contain letters or spaces only");
                    }
                }

                m_OwnerOfVehicleName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }

            set
            {
                foreach (char digit in value)
                {
                    if (!char.IsDigit(digit))
                    {
                        throw new FormatException(@"Phone number must contain digits only");
                    }
                }

                m_OwnerPhoneNumber = value;
            }
        }
    }
}