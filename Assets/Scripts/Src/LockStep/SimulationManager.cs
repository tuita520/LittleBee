﻿using System;
using System.Collections.Generic;

using System.Threading;


namespace LogicFrameSync.Src.LockStep
{
    public class SimulationManager
    {
        static SimulationManager ins;
        public static SimulationManager Instance
        {
            get{
                if (ins == null) ins = new SimulationManager();
                return ins;
            }         
        }
        private Thread m_Thread;
        private List<Simulation> m_Sims;

        private SimulationManager()
        {
            m_Sims = new List<Simulation>();           
        }
        public void Start()
        {
            foreach (Simulation sim in m_Sims)
                sim.Start();
            m_Thread = new Thread(Run);
            m_Thread.IsBackground = true;
            m_Thread.Start();
        }
        public void Stop()
        {
            if (m_Thread != null)
            {
                m_Thread.Abort();
                m_Thread = null;
                foreach (Simulation sim in m_Sims)
                    sim.Stop();
            }
        }
        void ThreadPoolRunner(object state)
        {
            Run();
        }
        bool m_StopState = false;
        void Run()
        {
            while(!m_StopState)
            {
                for (int i = 0; i < m_Sims.Count; ++i)
                {
                    m_Sims[i].Run();
                }
                Thread.Sleep(10);
            }           
        }
        public void AddSimulation(Simulation sim)
        {
            if (!ContainSimulation(sim))
                m_Sims.Add(sim);
        }
        public void RemoveSimulation(Simulation sim)
        {
            if (ContainSimulation(sim))
                m_Sims.Remove(sim);
        }
        public void RemoveSimulation(byte id)
        {
            Simulation sim = GetSimulation(id);
            if(sim!=null)
                RemoveSimulation(sim);
        }
        public bool ContainSimulation(Simulation sim)
        {
            return m_Sims.Contains(sim);
        }
        public Simulation GetSimulation(byte id)
        {
            for(int i=0;i<m_Sims.Count;++i)
            {
                if (m_Sims[i].GetSimulationId() == id) return m_Sims[i];
            }
            return null;
        }
    }
}
