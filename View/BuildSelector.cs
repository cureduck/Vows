using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using View;

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
