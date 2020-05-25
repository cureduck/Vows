using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.EventSystems;
using Manager;

namespace View
{

    public class PlayerController : Singleton<PlayerController>
    {
        public Animal body;
        //InfoView view;

        public enum Mode { Building,Common}
        [SerializeField] private Mode _mode;
        public Mode mode { get =>_mode; set
            {
                switch (_mode)
                {
                    case Mode.Building:
                        ExitBuildingMode();
                        break;
                    case Mode.Common:
                        ExitCommonMode();
                        break;
                    default:
                        break;
                }
                _mode = value;
                switch (value)
                {
                    case Mode.Building:
                        EnterBuildingMode();
                        break;
                    case Mode.Common:
                        EnterCommonMode();
                        break;
                    default:
                        break;
                }
            } }
        public Stru origin;

        public GameObject helper;

        public static Vector3 MouseToWorldPoint()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public static Vector2 FindNearestIntPoint(Vector3 pos)
        {
            return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));

        }

        void Start()
        {
            body = GameObject.Find("player").GetComponent<Animal>();
            //body = GetComponent<Animal>();
            //view = FindObjectOfType<InfoView>();
        }


        void Update()
        {
            Move2Mouse();
            SwitchMode();
            StopReact();
            helper.transform.position = FindNearestIntPoint(MouseToWorldPoint());
        }


        private void EnterBuildingMode()
        {
            helper.SetActive(true);
        }

        private void ExitBuildingMode()
        {
            helper.SetActive(false);
        }

        private void EnterCommonMode()
        {

        }

        private void ExitCommonMode()
        {

        }


        void Move2Mouse()
        {
            if ((Input.GetMouseButtonDown(1))&&(!EventSystem.current.IsPointerOverGameObject()))
            {
                body.SetDestination(MouseToWorldPoint());
            }
        }

        void StopReact()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                body.InterruptReact();
            }
        }

        public void SwitchMode(KeyCode buildCode=KeyCode.B,KeyCode commonCode=KeyCode.C)
        {
            switch (mode)
            {
                case Mode.Building:
                    if ((Input.GetMouseButton(0))&&(origin!=null))
                    {
                        Build(origin, MouseToWorldPoint());
                        mode = Mode.Common;
                    }
                    if (Input.GetKeyDown(commonCode))
                    {
                        mode = Mode.Common;
                    }
                    break;
                case Mode.Common:
                    if (Input.GetKeyDown(buildCode))
                    {
                        mode = Mode.Building;
                    }
                    break;
                default:
                    break;
            }
        }


        public void Build(Stru stru,Vector2 pos)
        {
            body.BuildStructure(stru, FindNearestIntPoint(pos));
        }

        public void FindStru(string name)
        {
            origin= StrucManager.Instance.struMap[name].GetComponent<Stru>();
        }

        public void FindStru(GameObject go)
        {
            origin = go.GetComponent<Stru>();
        }
    }
}

