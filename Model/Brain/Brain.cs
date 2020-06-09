using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Model.Brain
{
    public class Brain:MonoBehaviour
    {
        public BehaviorTree Harvest;
        public BehaviorTree Sleep;
        public BehaviorTree Rest;


        private void Update()
        {
            
        }
    }
}