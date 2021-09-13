using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tween_Library.Scripts;
using Tween_Library.Scripts.Effects;

public class BasicUIEffect : MonoBehaviour
{
    public EffectBuilder uiEffect;
    YieldInstruction _wait;
    public Vector3 scaleMax;
    public int scaleSpeed;
    public float rotationMax;
    public int speedShake;
    // Start is called before the first frame update
    void Start()
    {
        _wait = new WaitForSeconds(0.0f);
        uiEffect = new EffectBuilder(this).AddEffect(new ScaleRectEffect(GetComponent<RectTransform>(), scaleMax, scaleSpeed, _wait))
            .AddEffect(new ShakeRectEffect(GetComponent<RectTransform>(), rotationMax, speedShake));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
