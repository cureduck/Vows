using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.EventSystems;

namespace View
{

    public class PlayerController : MonoBehaviour
    {
        public Animal body;
        //InfoView view;

        public enum Mode { Building,Common}
        public Mode mode;
        public Structure origin;

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
                        helper.SetActive(false);
                    }
                    if (Input.GetKeyDown(commonCode))
                    {
                        mode = Mode.Common;
                        helper.SetActive(false);
                    }
                    break;
                case Mode.Common:
                    if (Input.GetKeyDown(buildCode))
                    {
                        mode = Mode.Building;
                        helper.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }


        public void Build(Structure structure,Vector2 pos)
        {
            body.BuildStructure(structure, FindNearestIntPoint(pos));
        }
    }
}

