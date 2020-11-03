namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxElectricityCapacity = 2.1f;

        public ElectricCar(string i_LicenseNumber) : base(new ElectricEngine(k_MaxElectricityCapacity))
        {
            m_LicenseNumber = i_LicenseNumber;
            s_VehicleQuestions = SetQuestions();
        }
    }
}