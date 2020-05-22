using System;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEditor;
using System.Linq;

namespace Manager
{
    public sealed class GroupManager:Singleton<GroupManager>
    {
        public Group[] communityList => groups.ToArray();
        public LinkedList<Group> groups;
        
        
        public event Action GroupChanged;

        private void Awake()
        {
            groups =new LinkedList<Group>();
        }

        public void AddNew(Group @group)
        {
            groups.AddLast(@group);
            GroupChanged?.Invoke();
        }

        private void OnCommunityChanged()
        {
            GroupChanged?.Invoke();
        }
    }

    [CustomEditor(typeof(GroupManager))]
    public class CommunityManagerEidtor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GroupManager a = (GroupManager)target;

            if (GUILayout.Button("Log"))
            {
                Debug.Log(a.communityList);
            }
        }
    }
}
