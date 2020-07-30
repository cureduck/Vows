using System;
using UnityEngine;
using Model;
using Manager;
using Model.Community;

namespace View
{
    internal class CommunitiesView:ListTemplate<Group[],Group,CommunityIcon>
    {

        public CommunityView communityView;

        protected override Group[] components
        {
            get => GroupManager.Instance.communityList;
            set => base.components = value;
        }


        private void Start()
        {
            template.communityView = communityView;
            GroupManager.Instance.GroupChanged += UpdateUI;
            UpdateUI();
        }

        public override void AddNewItem()
        {
            var t= AddNewTemplate();
            t.value = new Group(new Class[1]{new Class() }) {status = Group.Status.Building};
        }
    }
    
    
    
    
}
