using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    class Bed : Structure
    {
        public override Action<Entity>[] actions => new Action<Entity>[1] { Sleep};

        protected  IEnumerator sleep(Entity user)
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("sleeping!");
                yield return new WaitForSeconds(1f);
            }
        }

        public void Sleep(Entity user)
        {
            BeginReact(sleep(user), user);
        }
    }
}
