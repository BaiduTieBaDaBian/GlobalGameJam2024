using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;
    
    public CinemachineVirtualCamera virtualCamera;
    public Transform background;
    public float transitionDuration = 5.0f;
    public float endFieldOfView = 25.0f;

    private Transform currentFollow;
    private Transform currentLookAt;
    private float currentFieldOfView;
    private float transitionTimer = 0.0f;
    private bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
 
    void Update()
    {
        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float progress = transitionTimer / transitionDuration;

            if (virtualCamera != null)
            {
                virtualCamera.Follow = currentFollow;
                virtualCamera.LookAt = currentLookAt;
                virtualCamera.m_Lens.OrthographicSize= Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, endFieldOfView, progress);
            }

            if (progress >= 1.0f)
            {
                isTransitioning = false;
            }
        }
    }

    public void StartTransition()
    {
        if (isTransitioning)
            return;

        isTransitioning = true;
        transitionTimer = 0.0f;

        currentFollow = background;
        currentLookAt = background;
        currentFieldOfView = endFieldOfView;
    }
}
