using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float targetSize = 4f;
    public float transitionDuration = 2f;
    public GameObject player;
    public GameObject timer;

    private float initialSize;
    private float elapsedTime = 0f;

    private void Start()
    {
        virtualCamera.m_Lens.OrthographicSize = 14;
        initialSize = virtualCamera.m_Lens.OrthographicSize;
        
        StartCoroutine(StartTransitionAfterDelay());
    }
    IEnumerator StartTransitionAfterDelay()
    {
        yield return new WaitForSeconds(2f); // 等待2秒

        StartCoroutine(TransitionCameraSize());
    }
    IEnumerator TransitionCameraSize()
    {
        while (elapsedTime < transitionDuration)
        {
            float newSize = Mathf.Lerp(initialSize, targetSize, elapsedTime / transitionDuration);
            virtualCamera.m_Lens.OrthographicSize = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终大小是目标大小
        virtualCamera.m_Lens.OrthographicSize = targetSize;
        if (player != null)
            player.SetActive(true);
        if (timer != null)
            timer.SetActive(true);
    }
}
