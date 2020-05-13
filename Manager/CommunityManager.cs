using System;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEditor;

namespace Manager
{
    public class CommunityManager:Singleton<CommunityManager>
    {
        public Community community;

        public GameObject[] rank1;
        public GameObject[] rank2;
        public GameObject[] rank3;

        void Start()
        {

        }

        public void CreateCommunity()
        {
            var Roles1 = new Class();
            var Roles2 = new Class();
            var Roles3 = new Class();

        }

        
    }

    [CustomEditor(typeof(CommunityManager))]
    public class CommunityManagerEidtor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CommunityManager a = (CommunityManager)target;
            if (GUILayout.Button("Create Community"))
            {
                a.CreateCommunity();
            }

            if (GUILayout.Button("Log"))
            {
                Debug.Log(a.community);
            }
        }
    }
}
