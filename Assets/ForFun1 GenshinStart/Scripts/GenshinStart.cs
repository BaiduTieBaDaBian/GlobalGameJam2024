using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ForFun1_GenshinStart.Scripts
{
    public class GenshinStart : MonoBehaviour
    {
        private Canvas _canvas;
        
        public AudioClip bgmAudioClip;
        [Range(0,1)] public float audioVolume = 1;
        public float audioPitch = 1;
        public bool audioLoop = false;
        public float bgmCutTime = 34f;

        public GameObject _whiteBackGround;
        public GameObject _logo;
        public GameObject _text;
        
        private GenshinCanvasState canvasState = GenshinCanvasState.BeforeAppear;

        [SerializeField] private float bgmTime = 5f;
        
        // appear time
        [SerializeField] private float appearTime = 1f;
        
        // wait time
        [SerializeField] private float waitTime = 5f;

        // show text time
        [SerializeField] private float showTextTime = 1f;
        
        //disappear time
        [SerializeField] private float disappearTime = 15f;

        [SerializeField] private bool playOnAwake = true;

        
        //timer
        private float canvasTimer;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _whiteBackGround.GetComponent<Image>().color = Color.white;
            Color transColor = _whiteBackGround.GetComponent<Image>().color;
            transColor.a = 0;
            _whiteBackGround.GetComponent<Image>().color = transColor;

            canvasTimer = 0;
            _canvas.sortingOrder = -30000;

        }

        private bool isStart = false;

        private void Start()
        {
            if (playOnAwake)
            {
                StartGenshin(); 
            }
           
            DontDestroyOnLoad(this);
        }

        public void StartGenshin()
        {
            if(isStart) return;
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = bgmAudioClip;
            source.volume = audioVolume;
            source.pitch = audioPitch;
            source.loop = audioLoop;
            source.time = bgmCutTime;
            source.Play();
            isStart = true;
            canvasState = GenshinCanvasState.BGMStart;
        }
        
        private void Update()
        {
            switch (canvasState)
            {
                case GenshinCanvasState.BGMStart:
                    canvasTimer += Time.deltaTime;
                    if (canvasTimer >= bgmTime)
                    {
                        canvasState = GenshinCanvasState.Appear;
                        canvasTimer = 0;
                        _canvas.sortingOrder = 32767;
                    }
                    break;
                
                case GenshinCanvasState.Appear:
                    
                    canvasTimer += Time.deltaTime;
                    float whiteBGAlpha = canvasTimer / appearTime;
                    Color transColor = _whiteBackGround.GetComponent<Image>().color;
                    transColor.a = whiteBGAlpha;
                    _whiteBackGround.GetComponent<Image>().color = transColor;

                    if (canvasTimer >= appearTime)
                    {
                        canvasState = GenshinCanvasState.WaitTime;
                        canvasTimer = 0;
                    }
                    break;
                case GenshinCanvasState.WaitTime:
                    canvasTimer += Time.deltaTime;
                    if (canvasTimer >= waitTime)
                    {
                        canvasState = GenshinCanvasState.ShowLOGO;
                        canvasTimer = 0;
                    }
                    break;
                case GenshinCanvasState.ShowLOGO:
                    canvasTimer += Time.deltaTime;
                    whiteBGAlpha = canvasTimer / appearTime;
                    transColor = _logo.GetComponent<Image>().color;
                    transColor.a = whiteBGAlpha;
                    _logo.GetComponent<Image>().color = transColor;

                    if (canvasTimer >= showTextTime)
                    {
                        canvasState = GenshinCanvasState.ShowText;
                        canvasTimer = 0;
                    }
                    break;
                case GenshinCanvasState.ShowText:
                    canvasTimer += Time.deltaTime;
                    whiteBGAlpha = canvasTimer / appearTime;
                    transColor = _text.GetComponent<Image>().color;
                    transColor.a = whiteBGAlpha;
                    _text.GetComponent<Image>().color = transColor;

                    if (canvasTimer >= showTextTime)
                    {
                        canvasState = GenshinCanvasState.Disappear;
                        canvasTimer = 0;
                    }
                    break;
                case GenshinCanvasState.Disappear:
                    canvasTimer += Time.deltaTime;
                    if (canvasTimer >= disappearTime)
                    {
                        Destroy(gameObject);
                    }
                    break;
                    
                default:
                    return;
            }
        }
    }
    
    enum GenshinCanvasState
    {
        BeforeAppear,
        BGMStart,
        Appear,
        WaitTime,
        ShowLOGO,
        ShowText,
        Disappear,
    }
}

