using System;
using System.Collections;
using System.Collections.Generic;
using Tool5_UIFrameWork.example;
using Tool5_UIFrameWork.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainUIWindow : UIWindow
{
    private void Start()
    {
        TryAddPointerClickEvent("StartGame", StartGamePointerClick);
        TryAddPointerClickEvent("Setting",SettingPointerClick);
    }

    private void StartGamePointerClick(PointerEventData obj)
    {
        UIManager.Instance.GetWindow<MainUIWindow>().SetVisible(false);
    }
    
    
    private void SettingPointerClick(PointerEventData obj)
    {
        UIManager.Instance.GetWindow<MainUIWindow>().SetVisible(false);
        UIManager.Instance.GetWindow<SettingUIWindow>().SetVisible(true);

    }
}
