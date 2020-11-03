namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const float k_MaxFuelCapacity = 60f;
        private const FuelEngine.eGasType k_FuelCarGasType = FuelEngine.eGasType.Octan96;

        public FuelCar(string i_LicenseNumber) : base(new FuelEngine(k_MaxFuelCapacity, k_FuelCarGasType))
        {
            m_LicenseNumber = i_LicenseNumber;
            s_VehicleQuestions = SetQuestions();
        }
    }
}