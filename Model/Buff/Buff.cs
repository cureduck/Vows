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
        protected virtual void OnStart(){}
        protected virtual void OnInterrupt()
        {
            TakeOff();
        }

        protected virtual void OnComplete()
        {
            TakeOff();
        }

        public Buff(Entity owner)
        {
            Owner = owner;
            TakeOn();
        }

        protected virtual void TakeOn()
        {
            
        }

        protected virtual void TakeOff()
        {
            Owner.buffs.Remove(this);
        }

        [Button]
        public void Interrupt()
        {
            OnInterrupt();
        }
    }
}