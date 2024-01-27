using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public float totalTime = 60f; // 倒计时总时长（秒）
    private float currentTime; // 当前倒计时时间
    public TextMeshProUGUI timerText;// 用于显示倒计时的UI Text组件
    public Animator loadAnim;
    
    
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
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int index)
    {
        loadAnim.SetBool("isEnd",true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}