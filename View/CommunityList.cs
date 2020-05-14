using System;
using UnityEngine;
using Model;
using Manager;

namespace View
{
    class CommunityList:ListTemplate<Community,CommunityIcon>
    {
        
        void Update()
        {
            source = CommunityManager.Instance.communities;
            Display();
        }

    }
}
