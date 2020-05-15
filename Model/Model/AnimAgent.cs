using System;
using UnityEngine;


namespace Model
{
    public class AnimAgent:MonoBehaviour
    {

        private Animator _animator;
        private Entity _model;



        protected virtual void Awake()
        {
            
            _animator = GetComponent<Animator>();
            _model = GetComponent<Entity>();

            /*
            System.Reflection.MethodInfo[] methods = model.GetType().GetMethods();
            foreach (var method in methods)
            {
                foreach (var attr in method.GetCustomAttributes(typeof(Reaction), true))
                {

                }
            }
            */
        }

        private void Start()
        {


        }
    }

    /// <summary>
    /// 被打上标记的方法，都会在被调用的时候，触发与方法名相同的的animationTrigger
    /// </summary>
    [AttributeUsage(validOn:AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class Reaction : System.Attribute
    {
        public Func<Entity,bool> Prior;

        public Reaction(string test)
        {
        }
    }
}


