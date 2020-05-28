﻿using System;
using Manager;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class ItemIcon : Icon<Item>,IDragHandler,IEndDragHandler,IBeginDragHandler,IDropHandler,IPointerClickHandler
    {
        [SerializeField]
        private Image _image;

        private GameObject copy;
        private void Start()
        {
            UpdateUI();
        }

        protected override void UpdateUI()
        {
            _image.sprite = value?.itemSprite;
        }
        
        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrop(PointerEventData eventData)
        {
            var that = eventData.pointerDrag.GetComponent<ItemIcon>();
            if (that!=null)
            {
                GameManager.Instance.player.SwapItem(that.index,this.index);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerId != -2) return;
            if (!(value is Consumpution c)) return;
            GameManager.Instance.player.Use(GameManager.Instance.player,index);
        }
    }
}