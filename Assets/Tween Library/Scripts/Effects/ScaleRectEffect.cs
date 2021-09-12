using UnityEngine;
using System.Collections;

namespace Tween_Library.Scripts.Effects
{
    public class ScaleRectEffect : IUiEffect
    {
        private RectTransform RectTransform { get; }
        private Vector3 MaxSize { get; }
        private float ScaleSpeed { get;}
        private YieldInstruction Wait { get; }


        public ScaleRectEffect(RectTransform rectTransform,Vector3 maxSize, float scaleSpeed, YieldInstruction wait)
        {
            RectTransform = rectTransform;
            MaxSize = maxSize;
            ScaleSpeed = scaleSpeed;
            Wait = wait;
        }

       public IEnumerator Execute()
        {
            var time = 0f;
           var currentScale = RectTransform.localScale;
            while (RectTransform.localScale != MaxSize)
            {
                time += Time.deltaTime * ScaleSpeed;
                var scale = Vector3.Lerp(currentScale, MaxSize, time);
                RectTransform.localScale = scale;
                yield return null;
            }
            Debug.Log("Scaling Rect");
            //scale the rect using rect transform passed in constructor
            yield return Wait;

            currentScale = RectTransform.localScale;
            time = 0f;
            while (RectTransform.localScale != Vector3.one)
            {
                time += Time.deltaTime * ScaleSpeed;
                var scale = Vector3.Lerp(currentScale, Vector3.one, time);
                RectTransform.localScale = scale;
                yield return null;

            }


        }
    }
}
