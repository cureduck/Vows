using System;
using UnityEngine;
using Model;
using Manager;

namespace View
{
    class CommunitiesView:ListTemplate<Community[],Community,CommunityIcon>
    {

        public CommunityView communityView;

        public override Community[] source
        {
            get
            {
                return CommunityManager.Instance.communities;
            }
            set => base.source = value;
        }


        void Start()
        {
            template.communityView = communityView;
            CommunityManager.Instance.CommunityAdded += UpdateUI;
            UpdateUI();
        }
    }
}
