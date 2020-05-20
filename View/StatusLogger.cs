using System;
using UnityEngine;
using UnityEngine.UI;
using Model;
using TMPro;

namespace View
{
    class StatusLogger : MonoBehaviour
    {
        private Animal _animal;
        private Slider _hpBar, _spBar, _proBar;
        private TMP_Text _nameText;

        private void Start()
        {
            _animal = GetComponentInParent<Animal>();
            var bars = GetComponentsInChildren<Slider>();
            _hpBar = bars[0];
            _spBar = bars[1];
            _proBar = bars[2];
            _nameText = GetComponentInChildren<TMP_Text>();

            _nameText.text = _animal.name;

            UpdateView();
            _animal.StatusUpdated += UpdateView;
            _animal.ProgressUpdated += UpdateProgress;
        }

        private void UpdateView()
        {
            var tmp = _animal.combatAttr;
            _hpBar.value =(float) tmp.curHp / tmp.maxHp;
            _spBar.value = (float) tmp.curSp / tmp.maxSp;
        }

        private void UpdateProgress(float progress)
        {
            switch (progress)
            {
                case 0:
                    _proBar.gameObject.SetActive(false);
                    break;
                case 1:
                    _proBar.gameObject.SetActive(false);
                    break;
                default:
                    _proBar.gameObject.SetActive(true);
                    _proBar.value = progress;
                    break;
            }
        }
    }
}
