
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tween_Library.Scripts;
using Tween_Library.Scripts.Effects;
using UnityEngine.UI;

public class BasicTextEffect : MonoBehaviour
{
   
         [SerializeField]
    private Color uiOriginal;
    [SerializeField]
    private Color Flash;
    
    private Image flashImage;


    public EffectBuilder uiEffect;
    YieldInstruction _wait;
    public Vector3 scaleMax;
    public int scaleSpeed;
    public float rotationMax;
    public int speedShake;
    // Start is called before the first frame update
    /// <summary>
    ///simple scale and rotate script can be added to any obj
    /// </summary>
    void Start()
    {
        flashImage = GetComponent<Image>();
        _wait = new WaitForSeconds(0.03f);
        uiEffect = new EffectBuilder(this).AddEffect(new ScaleRectEffect(GetComponent<RectTransform>(), scaleMax, scaleSpeed, _wait))
            .AddEffect(new ShakeRectEffect(GetComponent<RectTransform>(), rotationMax, speedShake)).AddEffect(new FlashColorEffect(uiOriginal, Flash,flashImage, _wait));
    }

    // Update is called once per frame
    void Update()
    {

    }
}