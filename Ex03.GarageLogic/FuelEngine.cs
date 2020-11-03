using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eGasType m_GasType;

        public FuelEngine(float i_MaxEnergyCapacity, eGasType i_GasType) : base(i_MaxEnergyCapacity)
        {
            m_GasType = i_GasType;
        }

        public eGasType GasType
        {
            get
            {
                return m_GasType;
            }

            set
            {
                if (!Enum.IsDefined(typeof(eGasType), value))
                {
                    throw new ArgumentException("Invalid Gas type");
                }
                else
                {
                    m_GasType = value;
                }
            }
        }

        public void AddFuel(float i_AmontOfFuelToAdd, eGasType i_GasType)
        {
            if (i_GasType != m_GasType)
            {
                throw new ArgumentException("{0} fuel dose no match this vihecel engine", i_GasType.ToString());
            }
            else if (i_AmontOfFuelToAdd + m_CurrentEnergyCapacity > m_MaxEnergyCapacity || i_AmontOfFuelToAdd < 0)
            {
                throw new ValueOutOfRangeException("Fuel", k_MinEnergyCapacity, m_MaxEnergyCapacity);
            }
            else
            {
                m_CurrentEnergyCapacity += i_AmontOfFuelToAdd;
                m_EnergyLeftPercentage = (m_CurrentEnergyCapacity / m_MaxEnergyCapacity) * 100;
            }
        }

        public enum eGasType
          {
               Octan95 = 1,
               Octan96 = 2,
               Octan98 = 3,
               Soler = 4
          }
     }
}