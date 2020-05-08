﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.EventSystems;

namespace Manager
{
    public class PlayerReactHelper : MonoBehaviour
    {
        private Entity entity;
        public Behaviour halo;
        public Entity player;

        void Start()
        {
            entity = GetComponent<Entity>();
        }

        private void OnMouseEnter()
        {
            halo.enabled = true;            
        }

        private void OnMouseExit()
        {
            halo.enabled = false;
        }

        private void OnMouseUp()
        {
            if (entity.actions.Length==1)
            {
                if (player is Animal a) { a.Move2React(entity); }
                //entity.actions[0](player);
            }
        }

        void Update()
        {

        }
    }
}

