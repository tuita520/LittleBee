﻿using UnityEngine;
using Entitas;
using LogicFrameSync.Src.LockStep;

namespace Renderers
{
    /// <summary>
    /// 动作变化渲染
    /// 由Component数据驱动显示
    /// </summary>
    public class ActionRenderer:MonoBehaviour
    {
        protected Entity m_Entity;
        protected int m_EntityId=0;
        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }
        public void SetEntityId(int entityId)
        {
            m_EntityId = entityId;
        }
        protected Simulation GetSimulation(string name)
        {
            return SimulationManager.Instance.GetSimulation(name);
        }

        private void Update()
        {
            if (m_EntityId == 0) return;
            Simulation sim = GetSimulation("client");
            if (sim == null) return;
            m_Entity = sim.GetEntityWorld().GetEntity(m_EntityId);
            if (m_Entity == null) return;

            OnRender();
        }

        protected virtual void OnRender()
        {

        }
    }
}