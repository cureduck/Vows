using System;
using UnityEngine;

namespace Model
{
    class Village : Community
    {
        public override bool Collapse()
        {
            throw new NotImplementedException();

        }
    }


    class Chief : Roles
    {
        public override string RoleName => "Chief";
        public override RangeInt Capacity => new RangeInt(1, 1);
    }

    class DeputyChief : Roles
    {
        public override string RoleName => "Deputy Chief";
        public override RangeInt Capacity => new RangeInt(1, 1);
    }

    class Villager : Roles
    {

        public override string RoleName => "Villager";

        public override RangeInt Capacity => new RangeInt(1,100);
    }
}
