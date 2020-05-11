using System;
using UnityEngine;

namespace Model
{
    class Cookpot : Structure
    {
        protected override Action<Entity>[] DisiredReactions => throw new NotImplementedException();

        protected override void CompleteReact(Entity entity)
        {
            base.CompleteReact(entity);
        }

        protected override void StartReact(Coroutine coro, Entity trader)
        {
            base.StartReact(coro, trader);
        }
    }
}
