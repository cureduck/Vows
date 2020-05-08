using System;
using UnityEngine;

namespace View
{
    [CreateAssetMenu(fileName ="sdfsf",menuName ="KL"),Serializable]
    public class ViewContext:ScriptableObject
    {
        public string context;
        public string description;
    }

    [CreateAssetMenu(fileName = "sdfsf", menuName = "KL"), Serializable]
    public class ViewContexts : ScriptableObject
    {

    }
}
