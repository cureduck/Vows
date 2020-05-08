using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

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

        public SkillExp skillExp;

        public new string name;
        public Race race;
        public Weapon weapon;
        public Armor armor;
        public ItemBase[] items = new ItemBase[3]{new BigPotion(), null, null};
        public Profession prof;
        public Skill[] skills;


        private TileAI agent;

        #endregion

        #region events
        public event Action Death;
        public event Action StatusUpdated;

        #endregion

        #region methods

        private void Start()
        {
            agent = GetComponent<TileAI>();
            GetAnimComponents();
        }

        private static bool test(Entity t)
        {
            return true;
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
                new Tuple<string, string>("HP:"+curHp.ToString()+"/"+maxHp.ToString(),"Health Point,Drop to 0 and character dies"),
                new Tuple<string, string>("SP:"+curSp.ToString()+"/"+maxSp.ToString(),"Sprit Point,Drop to 0 and charater faint out"),
                new Tuple<string, string>("ATK:"+minAtk.ToString()+"~"+maxAtk.ToString(),"Attack"),
            };

            return status;
        }

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

        internal void HealHp(int point)
        {
            curHp = Math.Min(maxHp, point + curHp);
        }

        internal void HealSp(int point)
        {
            curSp = Math.Min(maxSp, point + curSp);
        }

        internal void TakeHpDmg(int point)
        {
            curHp = Math.Max(0, curHp - point);
            if (curHp == 0)
                Die();
        }


        internal void TakeSpDmg(int point)
        {
            curSp = Math.Max(0, curSp - point);
        }

        internal void Die()
        {
            Death();
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

        #endregion

        #region Animation Handler

        public void UpdateFace()
        {
            if (Math.Abs( velocity.x) > Math. Abs(velocity.y))
            {
                if (velocity.x>0)
                {
                    eyes.SetCategoryAndLabel("eyes", "right");
                }
                else
                {
                    eyes.SetCategoryAndLabel("eyes", "left");
                }
            }
            else
            {
                if (velocity.y < 0)
                {
                    eyes.SetCategoryAndLabel("eyes", "front");
                }
                else
                {
                    eyes.SetCategoryAndLabel("eyes", "back");

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
}
