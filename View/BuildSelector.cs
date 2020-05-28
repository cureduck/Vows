using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// 加载建筑模板
    /// </summary>
    public class BuildSelector : MonoBehaviour
    {
        [SerializeField] private Button btn;
        private void Start()
        {
            foreach (var go in StrucManager.Instance.items)
            {
                var t= GameObject.Instantiate(btn, parent: transform);
                t.gameObject.SetActive(true);
                t.GetComponentInChildren<Text>().text = go.name;
                t.onClick.AddListener(
                    ()=>
                    {
                        PlayerController.Instance.FindStru(go);
                    }
                );
            }
        }
    }
}
