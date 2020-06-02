using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Model.Brain
{
    public class Brain:MonoBehaviour
    {
        public BehaviorTree[] bts;
        private void Awake()
        {
            bts= GetComponents<BehaviorTree>();
        }
        
        
        
    }
}