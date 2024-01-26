using UnityEngine;

public class DrawExample : MonoBehaviour
{
    public Texture2D texture; // 要涂鸦的图片
    public GameObject objectToControl; // 要控制的物体

    void Update()
    {
        // 当按下空格键时
        if (Input.GetKey(KeyCode.Space))
        {
            // 发射一条射线从物体向图片上方
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit))
            {
                Debug.Log("1111");
                // 获取射线击中的纹理坐标
                Vector2 textureCoord = hit.textureCoord;

                // 将纹理坐标转换为像素坐标
                int x = (int)(textureCoord.x * texture.width);
                int y = (int)(textureCoord.y * texture.height);

                // 对图片上的像素进行涂鸦操作
                texture.SetPixel(x, y, Color.red);
                texture.Apply();
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.mainTexture = texture;
                }
            }
        }
    }
}