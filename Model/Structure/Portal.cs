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

        public override Action<Entity>[] actions => new Action<Entity>[1] { Teleport};

        public override Tuple<string, string>[] Status => new Tuple<string, string>[1] { new Tuple<string, string>("dsf","sdf")};

        public void Teleport(Entity user)
        {
            user.transform.position = destination.transform.position;
        }
    }
}
