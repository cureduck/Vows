using System;
using UnityEngine;
using Model;
using Manager;

namespace View
{
    class CommunitiesView:ListTemplate<Community[],Community,CommunityIcon>
    {

        public CommunityView communityView;

        public override Community[] components
        {
            get
            {
                return CommunityManager.Instance.communities;
            }
            set => base.components = value;
        }


        void Start()
        {
            template.communityView = communityView;
            CommunityManager.Instance.CommunityAdded += UpdateUI;
            UpdateUI();
        }
    }
}
