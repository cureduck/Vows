using System;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEditor;
using System.Linq;

namespace Manager
{
    public sealed class CommunityManager:Singleton<CommunityManager>
    {
        public Community[] communityList => communities.ToArray();
        public LinkedList<Community> communities;



        public event Action CommunityChanged;

        private void Awake()
        {
            communities =new LinkedList<Community>();
        }

        public void AddNew(Community community)
        {
            communities.AddLast(community);
            CommunityChanged?.Invoke();
        }

        private void OnCommunityChanged()
        {
            CommunityChanged?.Invoke();
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
                Debug.Log(a.communityList);
            }
        }
    }
}
