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

    [RequireComponent(typeof(TileAI),typeof(CapsuleCollider2D),typeof(Rigidbody2D))]
    public partial class Animal : Entity
    {
        #region property

        private Vector2 velocity => _agent.velocity;
        private SpriteResolver body,eyes,head;
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

        public string Name;
        public Race race;
        public Profession prof;
        public Skill[] skills;


        private TileAI _agent;
        private Animator _animator;

        public override Status status
        {
            get => base.status;
            set
            {
                base.status = value;
                switch (value)
                {
                    case Status.Idle:
                        _animator.SetTrigger("Idle");
                        break;
                    case Status.Reacting:
                        _animator.SetTrigger("Work");
                        break;
                    case Status.Stun:
                        _animator.SetTrigger("Stun");
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region events
        public event Action<Animal> Death;
        public event Action StatusUpdated;

        #endregion

        #region methods

        private void Start()
        {
            _agent = GetComponent<TileAI>();
            _animator = GetComponent<Animator>();
            //GetAnimComponents();
        }

        public void SetDestination(Vector2 dest)
        {
            if (status==Status.Idle)
            {
                _agent.SetDestination(dest);
                //UpdateFace();
            }
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
                eyes.SetCategoryAndLabel(velocity.x > 0 ? "eyes_right" : "eyes_left", "default");
            }
            else
            {
                eyes.SetCategoryAndLabel(velocity.y < 0 ? "eyes_front" : "eyes_back", "default");
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

        public override string ToString()
        {
            return Name;
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
