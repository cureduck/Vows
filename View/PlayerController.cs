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

        void Start()
        {
            //body = GetComponent<Animal>();
            //view = FindObjectOfType<InfoView>();
        }


        void Update()
        {
            Move2Mouse();
            StopReact();
        }

        void Move2Mouse()
        {
            if ((Input.GetMouseButtonDown(1))&&(!EventSystem.current.IsPointerOverGameObject()))
            {
                var des = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
                body.SetDestination(des);
            }
        }

        /*
        void GetMouseDownObject()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (hit.collider != null)
                {
                    //Debug.Log(hit.collider.name);
                    var tmp = hit.collider.GetComponent<Entity>();
                    //Debug.Log(tmp);
                    //view.SetOwner(tmp);
                }
            }
        }
        */

        

        void StopReact()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                body.InterruptReact();
            }
        }
    }
}

