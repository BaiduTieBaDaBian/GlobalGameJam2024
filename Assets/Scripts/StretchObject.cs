using UnityEngine;

public class StretchObject : MonoBehaviour
{
    public float stretchSpeed = 1f; // 拉伸/压缩速度
    public float maxHeight = 2f; // 最大高度
    public float minHeight = 0.5f; // 最小高度
    public Transform bottomPoint; // 底部点

    private Vector3 initialScale; // 初始尺寸
    private float initialY; // 初始 Y 坐标

    void Start()
    {
        // 记录初始尺寸和 Y 坐标
        initialScale = transform.localScale;
        initialY = bottomPoint.localPosition.y;
    }

    void Update()
    {
        // 获取垂直输入
        float verticalInput = -Input.GetAxis("Vertical");

        // 计算目标高度
        float targetHeight = Mathf.Clamp(transform.localScale.y + verticalInput * stretchSpeed * Time.deltaTime, minHeight, maxHeight);

        // 计算底部点的新 Y 坐标
        float newBottomY = initialY + (targetHeight - initialScale.y) / 2;

        // 更新底部点的局部坐标
        bottomPoint.localPosition = new Vector3(bottomPoint.localPosition.x, newBottomY, bottomPoint.localPosition.z);

        // 更新顶部点的局部坐标
        float topY = newBottomY + targetHeight - initialScale.y;
        transform.localPosition = new Vector3(transform.localPosition.x, topY, transform.localPosition.z);

        // 更新物体的缩放
        transform.localScale = new Vector3(initialScale.x, targetHeight, initialScale.z);
    }
}