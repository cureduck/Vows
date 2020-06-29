using System;
using UnityEngine;

namespace Model.Structure
{
    public class Fence:Stru
    {
        protected override Action<Animal>[] DesiredReactions { get; } = {null};
        
    }
}