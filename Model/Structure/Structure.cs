using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public abstract class Structure:Entity
    {

        public void Log(string s)
        {
            Debug.Log(s);
        }


    }
}
