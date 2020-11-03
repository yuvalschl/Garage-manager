namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxElectricityCapacity) : base(i_MaxElectricityCapacity)
        {
        }

        public void ChargeBattery(float i_NumberOfMinutes)
        {
            i_NumberOfMinutes /= 60;
            if (m_CurrentEnergyCapacity + i_NumberOfMinutes > m_MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException("Minutes to charge", k_MinEnergyCapacity, m_MaxEnergyCapacity);
            }
            else
            {
                m_CurrentEnergyCapacity += i_NumberOfMinutes;
                m_EnergyLeftPercentage = (m_CurrentEnergyCapacity / m_MaxEnergyCapacity) * 100;
            }
        }
    }
}