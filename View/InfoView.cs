using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace View
{
    public class InfoView:MonoBehaviour
    {
        public Color backgroundColor;
        public Color highlightColor;

        public GameObject statusView;
        public GameObject RuneView;
        public GameObject skillsView;
        public GameObject itemView;
        public GameObject infoView;
        public TMP_Text descriptionArea;

        public GameObject statusToggle;
        public GameObject runeToggle;
        public GameObject skillsToggle;
        public GameObject ItemsToggle;
        public GameObject InfoToggle;

        public Entity owner;

        public DText TextTemp;
        public ItemIcon itemIcon;

        public void SetOwner(Entity owner)
        {
            if (owner is Animal a)
            {
                skillsToggle.SetActive(a.skills != null);
                ItemsToggle.SetActive(a.items != null);
            }

            this.owner = owner;

            statusView.SetActive(true);
            skillsView.SetActive(false);
            itemView.SetActive(false);
            infoView.SetActive(false);

            UpdateUI();

            owner.AttrUpdated += UpdateUI;
        }

        private void UpdateUI()
        {
            UpdateStatus();
            UpdateInfo();
            if (owner is Animal a)
            {
                if (a.skills != null)
                    UpdateSkills();
                if (a.items!=null)
                    UpdateItems();
            }

        }

        private void UpdateStatus() 
        {
            foreach (Transform child in statusView.transform)
                Destroy(child.gameObject);

            /*
            foreach (var item in owner.Status)
            {
                var t= Instantiate(TextTemp, parent:statusView.transform);
                t.Set(item);
            }
            */
        }

        private void UpdateSkills()
        {

        }

        private void UpdateItems()
        {
            foreach (Transform child in itemView.transform)
                Destroy(child.gameObject);

            itemIcon.owner = owner as Animal;
            for (int i = 0; i < (owner as Animal).items.Length; i++)
            {
                itemIcon.index = i;
                Instantiate(itemIcon, parent: itemView.transform);
            }
        }

        private void UpdateInfo()
        {
            var tmp= infoView.GetComponentInChildren<TMP_Text>();
            tmp.text = owner.Info;
        }

        private void Start()
        {
            TextTemp.descArea = descriptionArea;
            SetOwner(owner);
        }
    }

}
