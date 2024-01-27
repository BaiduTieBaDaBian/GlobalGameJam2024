using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Tool5_UIFrameWork.Scripts
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private Dictionary<string, UIWindow> uiWindowDic;

        protected override void Init()
        {
            base.Init();
            uiWindowDic = new Dictionary<string, UIWindow>();
            UIWindow[] uiWindowArr = FindObjectsOfType<UIWindow>();
            for (int i = 0; i < uiWindowArr.Length; i++)
            {
                //隐藏窗口
                uiWindowArr[i].SetVisible(false);
                //记录窗口
                AddWindow(uiWindowArr[i]);
            }
        }

        public void AddWindow(UIWindow window)
        {
            uiWindowDic.Add(window.GetType().Name,window);

        }

        public T GetWindow<T>() where T : class
        {
            string key = typeof(T).Name;
            if (!uiWindowDic.ContainsKey(key)) return null;
            return uiWindowDic[key] as T;
        }
        
    }
}