using System;
using UnityEngine;
using Model;
using Manager;

namespace View
{
    internal class CommunitiesView:ListTemplate<Community[],Community,CommunityIcon>
    {

        public CommunityView communityView;

        protected override Community[] components
        {
            get => CommunityManager.Instance.communityList;
            set => base.components = value;
        }


        private void Start()
        {
            template.communityView = communityView;
            CommunityManager.Instance.CommunityChanged += UpdateUI;
            UpdateUI();
        }

        public override void AddNewItem()
        {
            var t= AddNewTemplate();
            t.value = new Community(new Class[1]{new Class() }) {status = Community.Status.Building};
        }
    }
    
    
    
    
}
