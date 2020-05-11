﻿using System;
using UnityEngine;

namespace Manager
{
    public class SettingManager : Singleton<SettingManager>
    {
        private void Start()
        {
            Resources.Load<GameSetting>("Settings");
        }

    }


    [Serializable, CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObject/Settings")]
    public class GameSetting : ScriptableObject
    {
        public float Volume;
        public float SpeedMult;
    }
}
