using System;
using UnityEngine;
using Model;
using Manager;

namespace View
{
    internal class CommunitiesView:ListTemplate<Community[],Community,CommunityIcon>
    {

        public CommunityView communityView;

        public override Community[] components
        {
            get => CommunityManager.Instance.communities;
            set => base.components = value;
        }


        private void Start()
        {
            template.communityView = communityView;
            CommunityManager.Instance.CommunityAdded += UpdateUI;
            UpdateUI();
        }
    }
}
