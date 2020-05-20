using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Model.Buff
{
    [Serializable]
    public abstract class Buff
    {
        [ShowInInspector]
        protected Entity Owner;
        
        /// <summary>
        /// 在开始时调用的方法
        /// </summary>
        protected virtual void OnStart(){}
        /// <summary>
        /// 被打断时调用的方法
        /// </summary>
        protected virtual void OnInterrupt()
        {
            TakeOff();
        }
        
        /// <summary>
        /// 完成时调用的方法
        /// </summary>
        protected virtual void OnComplete()
        {
            TakeOff();
        }

        public Buff(Entity owner)
        {
            Owner = owner;
        }
        
        /// <summary>
        /// 启动Buff的方法
        /// </summary>
        public virtual void TakeOn()
        {
            
        }

        /// <summary>
        /// 移除buff的方法，在OnComplete和OnInterrupt中都有调用
        /// </summary>
        protected virtual void TakeOff()
        {
            Owner.buffs.Remove(this);
        }

        [Button]
        public virtual void Interrupt()
        {
            OnInterrupt();
        }
    }
}