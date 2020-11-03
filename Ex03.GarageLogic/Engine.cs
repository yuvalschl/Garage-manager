using System;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected const int k_MinEnergyCapacity = 0;
        protected float m_CurrentEnergyCapacity;
        protected float m_EnergyLeftPercentage;
        protected float m_MaxEnergyCapacity;

        protected Engine(float i_MaxEnergyCapacity)
        {
            m_MaxEnergyCapacity = i_MaxEnergyCapacity;
        }

        public float MaxEnergyCapacity
        {
            get
            {
                return m_MaxEnergyCapacity;
            }

            set
            {
                if (value <= k_MinEnergyCapacity)
                {
                    throw new ArgumentException("Energy capacity must be positive");
                }
                else
                {
                    m_MaxEnergyCapacity = value;
                }
            }
        }

        public float CurrentEnergyCapacity
        {
            get
            {
                return m_CurrentEnergyCapacity;
            }

            set
            {
                if (value > m_MaxEnergyCapacity || value < 0)
                {
                    throw new ValueOutOfRangeException("Energy", k_MinEnergyCapacity, m_MaxEnergyCapacity);
                }
                else
                {
                    m_CurrentEnergyCapacity = value;
                    m_EnergyLeftPercentage = (value / m_MaxEnergyCapacity) * 100;
                }
            }
        }

        public float EnergyLeftPercentage
        {
            get
            {
                return m_EnergyLeftPercentage;
            }
        }
    }
}