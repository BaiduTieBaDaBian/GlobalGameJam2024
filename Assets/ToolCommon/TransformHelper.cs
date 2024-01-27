using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelper 
{
    /// <summary>
    /// 未知成绩，查找后代制定名称的Transform组件
    /// 核心思想，在子物体中查找，如果没有找到，则将人物交给子物体
    /// </summary>
    /// <param name="currentTF">当前Transform组件</param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Transform FindChildByName(this Transform currentTF, string name)
    {
        Transform childTF = currentTF.Find(name);
        if (childTF != null) return childTF;

        for (int i = 0; i < currentTF.childCount; i++)
        {
            childTF = FindChildByName(currentTF.GetChild(i), name);
            if (childTF != null) return childTF;
        }

        return null;
    }
}
