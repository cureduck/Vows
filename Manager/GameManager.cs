using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using System;
using System.Reflection;
using System.Linq;
using Utils;

namespace Manager
{
    public sealed class GameManager : Singleton<GameManager>
    {
        public Animal player { get; private set; }

        private void Awake()
        {
            player = GameObject.Find("player").GetComponent<Animal>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
