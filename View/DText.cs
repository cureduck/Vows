using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace View
{
    /// <summary>
    /// 最常用的信息栏模板，包含本体字符串和描述字符串，点击后将描数字符串打在描数框内
    /// </summary>
    public class DText:MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler
    {

        public TMP_Text descArea;
        [SerializeField]
        private TMP_Text thisArea;
        private string description;

        public void Set(Tuple<string,string> data)
        {
            thisArea.text = data.Item1;
            description = data.Item2;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            thisArea.fontSize -= 2;
            descArea.text = description;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            thisArea.fontSize += 2;
        }

        

        private void Awake()
        {
            //here = GetComponent<TMP_Text>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            thisArea.fontSize += 1;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            thisArea.fontSize -= 1;
        }


    }
}
