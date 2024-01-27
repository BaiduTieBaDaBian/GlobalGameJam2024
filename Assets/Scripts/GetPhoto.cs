using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GetPhoto : MonoBehaviour
{
    public RenderTexture renderTexture01;
    private Texture2D texture2D01;
    public RenderTexture renderTexture02;
    private Texture2D texture2D02;
    public RenderTexture renderTexture03;
    private Texture2D texture2D03;

    private void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeScreenshot01()
    {

        
        // 激活这个RenderTexture，以便后续读取数据
        RenderTexture.active = renderTexture01;
        texture2D01 = new Texture2D(renderTexture01.width, renderTexture01.height, TextureFormat.RGBA32, false);
        // 创建一个新的Texture2D来保存截图的像素数据
        
        // 从RenderTexture中读取像素数据到Texture2D中
        texture2D01.ReadPixels(new Rect(0, 0, renderTexture01.width, renderTexture01.height), 0, 0);
        var bytes = texture2D01.EncodeToPNG();
        var paht = Application.dataPath + "/Resources/Photo/level1.png";
        File.WriteAllBytes(paht,bytes);
        
    }

    public void TakeScreenshot02()
    {

        
        // 激活这个RenderTexture，以便后续读取数据
        RenderTexture.active = renderTexture02;
        texture2D02 = new Texture2D(renderTexture02.width, renderTexture02.height, TextureFormat.RGBA32, false);
        // 创建一个新的Texture2D来保存截图的像素数据
        
        // 从RenderTexture中读取像素数据到Texture2D中
        texture2D02.ReadPixels(new Rect(0, 0, renderTexture02.width, renderTexture02.height), 0, 0);
        var bytes = texture2D02.EncodeToPNG();
        var paht = Application.dataPath + "/Resources/Photo/level2.png";
        File.WriteAllBytes(paht,bytes);
        
    }
    public void TakeScreenshot03()
    {

        
        // 激活这个RenderTexture，以便后续读取数据
        RenderTexture.active = renderTexture03;
        texture2D03 = new Texture2D(renderTexture03.width, renderTexture03.height, TextureFormat.RGBA32, false);
        // 创建一个新的Texture2D来保存截图的像素数据
        
        // 从RenderTexture中读取像素数据到Texture2D中
        texture2D03.ReadPixels(new Rect(0, 0, renderTexture03.width, renderTexture03.height), 0, 0);
        var bytes = texture2D03.EncodeToPNG();
        var paht = Application.dataPath + "/Resources/Photo/level3.png";
        File.WriteAllBytes(paht,bytes);
        
    }
}