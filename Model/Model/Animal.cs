using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Community;
using Model.Plants;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEditor;

namespace Model
{

    [RequireComponent(typeof(TileAI),typeof(CapsuleCollider2D),typeof(Rigidbody2D)),Serializable]
    public partial class Animal : Entity, ISubject
    {
        #region property
        
        [ShowInInspector]
        public string name { get; set; }
        private float velocity => _agent.velocity;
        private SpriteResolver body,eyes,head;
        public bool hasReached => _agent.hasReached;

        public Properties properties;
        
        #endregion

        #region variable

        [Serializable]
        public struct CombatAttr
        {
            public int curHp;
            public int maxHp;
            public int curSp;
            public int maxSp;
            public int minAtk;
            public int maxAtk;
            public float dodge;
            public float crit;
            public float def;
            public int curMood;
            public int maxMood;
        }

        [Serializable]
        public struct BaseAttr
        {
            public int Con;
            public int Wis;
            public int Str;
            public int Int;
        }
        
        public CombatAttr combatAttr;
        public BaseAttr baseAttr;
        public SkillExp skillExp;

        [ShowInInspector]
        public Group mainGroup;
        
        private TileAI _agent;
        private Animator _animator;

        public Item[] backpack;
        #endregion

        #region events
        public event Action<Animal> Death;
        public event Action StatusUpdated;
        public event Action BackpackChanged;

        #endregion

        #region methods

        private void Awake()
        {
            _agent = GetComponent<TileAI>();
            _animator = GetComponent<Animator>();
            //GetAnimComponents();
        }

        public void SetDestination(Vector2 dest)
        {
            
            if (status==Status.Idle)
            {
                var t= new Walk(this, dest);
                task = t;
                t.TakeOn();
            }
            //_agent.SetDestination(dest);
            //UpdateFace();
        }
        
        [Button]
        public void ChangeBackpack()
        {
            BackpackChanged?.Invoke();
        }

        
        public void SwapItem(int idx1,int idx2)
        {
            var tmp = backpack[idx1];
            backpack[idx1] = backpack[idx2];
            backpack[idx2] = tmp;
            BackpackChanged?.Invoke();
        }
        
        /// <summary>
        /// 待改进
        /// </summary>
        /// <param name="consumpution"></param>
        public void Use(Animal owner,int index)
        {
            if (!(owner.backpack[index] is Consumpution c)) return;
            c.OnUse(this);
            if (c.Qty == 0)
                owner.backpack[index] = null;
            BackpackChanged?.Invoke();
        }
        
        
        public bool PickUp(Item item)
        {
            for (var i = 0; i < backpack.Length; i++)
            {
                if (backpack[i] != null) continue;
                backpack[i] = item;
                BackpackChanged?.Invoke();
                return true;
            }
            return false;
        }
        
        
        internal void HealHp(int point)
        {
            combatAttr.curHp = Math.Min(combatAttr.maxHp, point + combatAttr.curHp);
            StatusUpdated?.Invoke();
        }

        internal void HealSp(int point)
        {
            combatAttr.curSp = Math.Min(combatAttr.maxSp, point + combatAttr.curSp);
            StatusUpdated?.Invoke();
        }

        internal void TakeHpDmg(int point)
        {
            combatAttr.curHp = Math.Max(0, combatAttr.curHp - point);
            if (combatAttr.curHp == 0)
                Die();
            StatusUpdated?.Invoke();
        }


        internal void TakeSpDmg(int point)
        {
            combatAttr.curSp = Math.Max(0, combatAttr.curSp - point);
            StatusUpdated?.Invoke();
        }
        
        [Button]
        internal void Die()
        {
            Destroy(gameObject);
            Death?.Invoke(this);
        }

        public void Move2React(Entity target,int index=0)
        {
            StartCoroutine(Move2(target, index));
        }

        private IEnumerator Move2(Entity target, int index)
        {
            SetDestination(target.transform.position);
            yield return new WaitUntil(()=> _agent.hasReached);
            target.GetReactions(this)[index](this);
        }

        public void React(Entity target,int index=0)
        {
            target.GetReactions(this)[index](this);
        }

        public void BuildStructure(Stru origin, Vector2 pos)
        {
            var building= Instantiate(original: origin, position: pos, rotation: Quaternion.identity);
            building.owner = this;
        }
        #endregion
        
        #region Animation Handler

        /*
        public void UpdateFace()
        {
            if (Math.Abs( velocity.x) > Math. Abs(velocity.y))
            {
                eyes.SetCategoryAndLabel(velocity.x > 0 ? "eyes_right" : "eyes_left", "default");
            }
            else
            {
                eyes.SetCategoryAndLabel(velocity.y < 0 ? "eyes_front" : "eyes_back", "default");
            }
        }
        */


        public void GetAnimComponents()
        {
            body= transform.Find("body").GetComponent<SpriteResolver>();
            head = transform.Find("head").GetComponent<SpriteResolver>();
            eyes = head.transform.Find("eyes").GetComponent<SpriteResolver>();
        }

        public override Action<Animal>[] GetReactions(Animal sponser)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return name;
        }


        #endregion
    }


    public struct Properties
    {
        public List<Plant> Plants;
        public List<Bed> Beds;
        
    }
}
