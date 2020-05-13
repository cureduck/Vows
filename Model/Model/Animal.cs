using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEditor;

namespace Model
{

    [RequireComponent(typeof(TileAI),typeof(BoxCollider2D),typeof(Rigidbody2D))]
    public partial class Animal : Entity
    {
        #region property
        public Vector2 velocity { get => agent.velocity; }
        private SpriteResolver body;
        private SpriteResolver eyes;
        private SpriteResolver head;
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

        public new string name;
        public Race race;
        public Profession prof;
        public Skill[] skills;


        private TileAI agent;
        private Animator animator;

        public override Status status
        {
            get => base.status;
            set
            {
                base.status = value;
                switch (value)
                {
                    case Status.Idle:
                        animator.SetTrigger("Idle");
                        break;
                    case Status.Reacting:
                        animator.SetTrigger("Work");
                        break;
                    case Status.Stun:
                        animator.SetTrigger("Stun");
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region events
        public event Action Death;
        public event Action StatusUpdated;

        #endregion

        #region methods

        private void Start()
        {
            agent = GetComponent<TileAI>();
            animator = GetComponent<Animator>();
            GetAnimComponents();
        }

        public void SetDestination(Vector2 destiantion)
        {
            if (status==Status.Idle)
            {
                agent.SetDestination(destiantion);
                UpdateFace();
            }
        }


        private Tuple<string,string>[] GetStatus()
        {
            Tuple<string, string>[] status = new Tuple<string, string>[6]
            {
                new Tuple<string, string>("Name:"+name,"Name"),
                new Tuple<string, string>("Race:Human","The Race"),
                new Tuple<string, string>("Bielf:Iron","The Blief"),
                new Tuple<string, string>("HP:"+combatAttr.curHp.ToString()+"/"+combatAttr.maxHp.ToString(),"Health Point,Drop to 0 and character dies"),
                new Tuple<string, string>("SP:"+combatAttr.curSp.ToString()+"/"+combatAttr.maxSp.ToString(),"Sprit Point,Drop to 0 and charater faint out"),
                new Tuple<string, string>("ATK:"+combatAttr.minAtk.ToString()+"~"+combatAttr.maxAtk.ToString(),"Attack"),
            };

            return status;
        }

        /*
        public void SwapItem(int idx1,int idx2)
        {
            var tmp = items[idx1];
            items[idx1] = items[idx2];
            items[idx2] = tmp;
            UpdateAttr();
        }
        /// <summary>
        /// 待改进
        /// </summary>
        /// <param name="consumpution"></param>
        public void Use(Animal owner,int index)
        {
            if (owner.items[index] is Consumpution c)
            {
                c.Use(this);
                if (c.count == 0)
                    owner.items[index] = null;
                UpdateAttr();
            }
        }
    */

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

        internal void Die()
        {
            Debug.Log("去世");
            Death?.Invoke();
            Destroy(gameObject);
        }

        public void Move2React(Entity target,int index=0)
        {
            StartCoroutine(Move2(target, index));
        }

        protected IEnumerator Move2(Entity target, int index)
        {
            SetDestination(target.transform.position);
            yield return new WaitUntil(()=> { return agent.hasReached; });
            target.GetReactions(this)[index](this);
        }

        public void BuildStructure(Structure origin, Vector2 pos)
        {
            Instantiate(original: origin, position: pos, rotation: Quaternion.identity);
        }
        #endregion

        #region Animation Handler

        public void UpdateFace()
        {
            if (Math.Abs( velocity.x) > Math. Abs(velocity.y))
            {
                if (velocity.x>0)
                {
                    eyes.SetCategoryAndLabel("eyes_right", "default");
                }
                else
                {
                    eyes.SetCategoryAndLabel("eyes_left", "default");
                }
            }
            else
            {
                if (velocity.y < 0)
                {
                    eyes.SetCategoryAndLabel("eyes_front", "default");
                }
                else
                {
                    eyes.SetCategoryAndLabel("eyes_back", "default");

                }

            }
        }


        public void GetAnimComponents()
        {
            body= transform.Find("body").GetComponent<SpriteResolver>();
            head = transform.Find("head").GetComponent<SpriteResolver>();
            eyes = head.transform.Find("eyes").GetComponent<SpriteResolver>();
        }

        public override Action<Entity>[] GetReactions(Entity sponser)
        {
            throw new NotImplementedException();
        }


        #endregion
    }

    [CustomEditor(typeof(Animal))]
    public class AnimalEditor:Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Animal a = (Animal)target;
            if (GUILayout.Button("Die"))
            {
                a.Die();
            }
        }
    }
}
