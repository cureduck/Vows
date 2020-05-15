using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.EventSystems;

namespace Manager
{
    public class PlayerReactHelper : MonoBehaviour
    {
        private Entity _entity;
        public Behaviour halo;
        public Entity player;

        private void Start()
        {
            _entity = GetComponent<Entity>();
            player = GameManager.Instance.player;
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
            if (_entity.GetReactions(player).Length != 1) return;
            if (player is Animal a) { a.Move2React(_entity); }
            //entity.actions[0](player);
        }
    }
}

