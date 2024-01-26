using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public Rigidbody2D bottomObject; // 底部的物体

    public float swingForce = 10000f; // 施加的摆动力量

    void Update()
    {
        // 如果按下右键
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ApplySwingForce(Vector2.right);
        }
        // 如果按下左键
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ApplySwingForce(Vector2.left);
        }
    }

    // 施加摆动力量
    void ApplySwingForce(Vector2 direction)
    {
        // 底部物体施加力量
        bottomObject.AddForce(direction * swingForce, ForceMode2D.Impulse);
        
    }
}


