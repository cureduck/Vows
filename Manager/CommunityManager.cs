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
        public Community[] communities => _communities.ToArray();
        public LinkedList<Community> _communities;



        public event Action CommunityChanged;

        private void Awake()
        {
            _communities =new LinkedList<Community>();
        }

        public void AddNew(Community community)
        {
            _communities.AddLast(community);
            CommunityChanged?.Invoke();
        }

        protected virtual void OnCommunityChanged()
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
                Debug.Log(a.communities);
            }
        }
    }
}
