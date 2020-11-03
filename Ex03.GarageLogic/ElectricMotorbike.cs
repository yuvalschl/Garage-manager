namespace Ex03.GarageLogic
{
    public class ElectricMotorbike : Motorbike
    {
        private const float k_MaxElectricityCapacity = 1.2f;

        public ElectricMotorbike(string i_LicenseNumber) : base(new ElectricEngine(k_MaxElectricityCapacity))
        {
            m_LicenseNumber = i_LicenseNumber;
            s_VehicleQuestions = SetQuestions();
        }
    }
}