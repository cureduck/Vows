using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    class Bed : Stru
    {
        public override string StruName => "Bed";

        protected override Action<Entity>[] DisiredReactions => new Action<Entity>[1] { Sleep };

        protected  IEnumerator sleep(Entity user)
        {
            for (int i = 0; i < 10; i++)
            {
                if (user is Animal a)
                {
                    a.HealHp(3);
                    a.HealSp(1);
                }
                yield return new WaitForSeconds(1f);
            }


        }

        public void Sleep(Entity user)
        {
        }
    }
}
