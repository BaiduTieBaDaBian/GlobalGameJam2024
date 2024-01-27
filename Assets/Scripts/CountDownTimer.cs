using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public float totalTime = 30f; // 倒计时总时长（秒）
    private float currentTime; // 当前倒计时时间
    public TextMeshProUGUI timerText;// 用于显示倒计时的UI Text组件
    public Animator loadAnim;
    public GetPhoto getPhoto;
    public Image image1;
    public Image image2;
    public Image image3;
    public string image1Path = "Assets/Resources/Photo/level1.png";
    public string image2Path = "Assets/Resources/Photo/level2.png";
    public string image3Path = "Assets/Resources/Photo/level3.png";
    public GameObject player;
    void Start()
    {
        currentTime = totalTime;
        
    }

    void Update()
    {
        // 更新当前倒计时时间
        currentTime -= Time.deltaTime;

        // 将时间转换为整数
        int timeInt = Mathf.RoundToInt(currentTime);

        // 更新UI Text显示
        timerText.text = timeInt.ToString();

        // 倒计时结束时执行操作
        if (currentTime <= 0)
        {
            
            // 在此处添加您想要执行的操作，例如停止倒计时，触发事件等
            currentTime = 0; // 确保显示为0
            
            LoadNextScreen();
            
        }
    }



    public void LoadNextScreen()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
        if (SceneManager.GetActiveScene().buildIndex < 4)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));  
        }
        else
        {
            player.SetActive(false);
            Sprite sprite1 = LoadSpriteFromFile(image1Path);
            Sprite sprite2 = LoadSpriteFromFile(image2Path);
            Sprite sprite3 = LoadSpriteFromFile(image3Path);

            // 设置Image组件的Sprite
            if (sprite1 != null && image1 != null)
            {
                image1.sprite = sprite1;
            }
            if (sprite2 != null && image2 != null)
            {
                image2.sprite = sprite2;
            }
            if (sprite3 != null && image3 != null)
            {
                image3.sprite = sprite3;
            }
            CameraControl.Instance.StartTransition();
            
        }
        
    }

    Sprite LoadSpriteFromFile(string path)
    {
        // 使用Texture2D来加载图片
        Texture2D texture = LoadTextureFromFile(path);

        if (texture != null)
        {
            // 创建Sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            return sprite;
        }
        else
        {
            Debug.LogError("Failed to load texture from file: " + path);
            return null;
        }
    }


    IEnumerator LoadLevel(int index)
    {
        player.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex + 1 == 2)
        {
            getPhoto.TakeScreenshot01();
        }

        if (SceneManager.GetActiveScene().buildIndex + 1 == 3)
        {
            getPhoto.TakeScreenshot02();
        }
        if (SceneManager.GetActiveScene().buildIndex + 1 == 4)
        {
            getPhoto.TakeScreenshot03();
        }
        
        loadAnim.SetBool("isEnd",true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
    
    Texture2D LoadTextureFromFile(string path)
    {
        // 从文件加载图片
        byte[] fileData = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // 将文件数据加载到Texture2D中
        return texture;
    }

}