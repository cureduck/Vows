using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public class Portal : Structure
    {
        public GameObject destination;

        protected override Action<Entity>[] DisiredReactions => new Action<Entity>[1] { Teleport };

        public void Teleport(Entity user)
        {
            user.transform.position = destination.transform.position;
        }
    }
}
