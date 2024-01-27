using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI事件监听器
/// </summary>
public class UIEventListener : MonoBehaviour , IPointerDownHandler,IPointerClickHandler,IPointerUpHandler
{
   public Action<PointerEventData> PointerClick;
   public Action<PointerEventData> PointerDown;
   public Action<PointerEventData> PointerUp;

   /// <summary>
   /// 通过Tranform获取事件监听器
   /// </summary>
   /// <param name="tf"></param>
   /// <returns></returns>
   public static UIEventListener GetListener(Transform tf)
   {
      UIEventListener uiEvent = tf.GetComponent<UIEventListener>();
      if (uiEvent == null) uiEvent = tf.gameObject.AddComponent<UIEventListener>();
      return uiEvent;
   }
   
   
   //继承接口
   public void OnPointerDown(PointerEventData eventData)
   {
      //抽象类，接口（多类抽象行为），委托（一类抽象行为）
      PointerDown?.Invoke(eventData);
   }

   public void OnPointerClick(PointerEventData eventData)
   {
      PointerClick?.Invoke(eventData);
   }

   public void OnPointerUp(PointerEventData eventData)
   {
      PointerUp?.Invoke(eventData);
   }
}
