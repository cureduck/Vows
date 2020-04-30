using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace View
{
    public class ItemIcon:Icon,IDragHandler,IDropHandler,IBeginDragHandler,IPointerClickHandler
    {
        public enum Slot { Weapon,Armor,Item}

        private Slot slot;
        public Animal owner;
        public int index;
        private ItemBase item => owner.items[index];
        [SerializeField]
        private TMP_Text count;
        private void Start()
        {
            if (item!=null)
            {
                image.sprite = item.sprite;
                if (item is Consumpution c)
                {
                    count.text = c.count.ToString();
                }
                else
                {
                    count.gameObject.SetActive(false);
                }
            }
            else
            {
                image.sprite = null;
                count.gameObject.SetActive(false);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var that= eventData.pointerDrag.GetComponent<ItemIcon>();
            if (this.owner==that.owner)
            {
                owner.SwapItem(this.index, that.index);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerId);
            switch (eventData.pointerId)
            {
                case -2:
                    if (item is Consumpution c)
                        owner.Use(owner,index);
                    break;
                default:

                    break;
            }
        }
    }
}
