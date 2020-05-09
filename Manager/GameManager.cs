using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;


namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public Animal player { get; private set; }

        void Start()
        {
            player = GameObject.Find("player").GetComponent<Animal>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
