using System;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEditor;
using System.Linq;

namespace Manager
{
    public class CommunityManager:Singleton<CommunityManager>
    {
        public Community[] communities;

        void Start()
        {
            communities = new Community[12];
            foreach (var item in communities)
            {
                Debug.Log(item);
            }
        }

        public void AddNew(Community community)
        {
            for (int i = 0; i < communities.Length; i++)
            {
                if (communities[i]==null)
                {
                    communities[i] = community;
                    break;
                }
            }
        }



    }

    [CustomEditor(typeof(CommunityManager))]
    public class CommunityManagerEidtor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CommunityManager a = (CommunityManager)target;

            if (GUILayout.Button("Log"))
            {
                Debug.Log(a.communities);
            }
        }
    }
}
