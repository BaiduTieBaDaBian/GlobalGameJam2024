using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tool5_UIFrameWork.Scripts
{
    
    
    public class UIWindow : MonoBehaviour
    {
        private Dictionary<string, UIEventListener> uiEventDic;

        private CanvasGroup cgroup;
        private void Awake()
        {
            uiEventDic = new Dictionary<string, UIEventListener>();
            cgroup = GetComponent<CanvasGroup>();
        }

        public void SetVisible(bool state, float delay = 0)
        {
            StartCoroutine(SetVisibleDelay(state, delay));

        }

        private IEnumerator SetVisibleDelay(bool state, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (state)
            {
                cgroup.alpha = 1;
            }
            else
            {
                cgroup.alpha = 0;
            }
        }

        /// <summary>
        /// 根据子物体名称获取事件监听器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UIEventListener GetUIEventListener(string name)
        {
            if (!uiEventDic.ContainsKey(name))
            {
                Transform tf = transform.FindChildByName(name);
                UIEventListener uiEvent = UIEventListener.GetListener(tf);
                uiEventDic.Add(name,uiEvent);
            }

            return uiEventDic[name];
        }
        
        public bool TryGetUIEventListener(string name , out UIEventListener listener)
        {
            if (!uiEventDic.ContainsKey(name))
            {
                Transform tf = transform.FindChildByName(name);
                if (tf == null)
                {
                    listener = null;
                    return false;
                }
                UIEventListener uiEvent = UIEventListener.GetListener(tf);
                uiEventDic.Add(name,uiEvent);
            }

            listener = uiEventDic[name];
            return true;
        }
        
        
        public UIEventListener TryGetUIEventListener(string name)
        {
            if (!uiEventDic.ContainsKey(name))
            {
                Transform tf = transform.FindChildByName(name);
                if (tf == null)
                {
                    return null;
                }
                UIEventListener uiEvent = UIEventListener.GetListener(tf);
                uiEventDic.Add(name,uiEvent);
            }
            return uiEventDic[name];
        }
        
        public void TryAddPointerClickEvent(string name, Action<PointerEventData> action)
        {
            UIEventListener listener = TryGetUIEventListener(name);
            if (listener != null)
            {
                listener.PointerClick += action;
            }
        }
    }
}