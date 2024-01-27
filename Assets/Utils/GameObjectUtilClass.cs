using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameObjectUtilClass 
{
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color color = default(Color), TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 0)
    {
        if(color == default(Color))
        {
            color = Color.white;
        }
        return CreateWorldText(parent, text, localPosition, fontSize, color, textAnchor, textAlignment, sortingOrder);
    }

    public static TextMesh CreatePopUp()
    {
        return null;
    }
    
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
    
    public static Vector3 GetMouseWorldPosition()
    {
        //Vector3 vec3 = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        Vector2 vec2 = Mouse.current.position.ReadValue();
        Vector3 vec3 = GetMouseWorldPositionWithZ(new Vector3(vec2.x,vec2.y,0), Camera.main);
        
        Debug.Log("vec2"  + vec2);
        Debug.Log("vec3"  + vec3);

        vec3.z = 0f;
        return vec3;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        screenPosition.z = worldCamera.nearClipPlane;
        Vector3 vec3 = worldCamera.ScreenToWorldPoint(screenPosition);
        return vec3;
    }

    public static Vector3 GetMouseWorldPosition3D(LayerMask mask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
        
    }
}
