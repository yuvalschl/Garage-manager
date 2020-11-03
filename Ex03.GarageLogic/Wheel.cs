using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const int k_MinPsi = 0;
        private string m_ManufacturerName;
        private float m_CurrentPsi;
        private float m_MaxPsi;

        public Wheel(float i_MaxPsi, string i_ManufacturerName, float i_CurrentPsi)
        {
            m_MaxPsi = i_MaxPsi;
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentPsi = i_CurrentPsi;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float MaximalPsi
        {
            get
            {
                return m_MaxPsi;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Max Psi must be positive");
                }
                else
                {
                    m_MaxPsi = value;
                }
            }
        }

        public float CurrentPsi
        {
            get
            {
                return m_CurrentPsi;
            }

            set
            {
                if (value > m_MaxPsi)
                {
                    throw new ValueOutOfRangeException("Max Air Pressure", k_MinPsi, m_MaxPsi);
                }
                else
                {
                    m_CurrentPsi = value;
                }
            }
        }

        public static List<Wheel> SetWheels(string i_WheelManufacturer, string i_CurrentPsi, float i_MaxPsi, int i_NumberOfWheels)
        {
            List<Wheel> wheelsList = new List<Wheel>(i_NumberOfWheels);
            if (i_MaxPsi < 0)
            {
                throw new ArgumentException(@"max PSI cant be negative");
            }

            if (!float.TryParse(i_CurrentPsi, out float currentPsiAsFloat))
            {
                throw new ArgumentException(@"current PSI must be a number");
            }

            if (currentPsiAsFloat < 0 || currentPsiAsFloat > i_MaxPsi)
            {
                throw new ValueOutOfRangeException("current Psi", k_MinPsi, currentPsiAsFloat);
            }

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                wheelsList.Add(new Wheel(i_MaxPsi, i_WheelManufacturer, currentPsiAsFloat));
            }

            return wheelsList;
        }

        public void InflateWheel(float i_AirPressureToAdd)
        {
            if (m_CurrentPsi + i_AirPressureToAdd > m_MaxPsi)
            {
                throw new ValueOutOfRangeException("Air Pressure", k_MinPsi, m_MaxPsi);
            }
            else
            {
                m_CurrentPsi += i_AirPressureToAdd;
            }
        }
    }
}