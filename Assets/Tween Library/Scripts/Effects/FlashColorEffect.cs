///Tomas Munro's Script
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Tween_Library.Scripts;

namespace Tween_Library.Scripts.Effects
{
    public class FlashColorEffect : IUiEffect
    {
    private YieldInstruction Wait { get; }
    private Color DefaultColor { get; }
    private Color TakeDammageColor { get; }

    private Image SliderFill { get; }
        /// <summary>
        /// flash color effect
        /// </summary>
        /// <param name="defaultColor"></param>
        /// <param name="takeDamageColor"></param>
        /// <param name="sliderFill"></param>
        /// <param name="wait"></param>
        public FlashColorEffect(Color defaultColor, Color takeDamageColor, Image sliderFill, YieldInstruction wait)
        {
            DefaultColor = defaultColor;
            TakeDammageColor = takeDamageColor;
            SliderFill = sliderFill;
            Wait = wait;
        }
        /// <summary>
        /// execute flash effect
        /// </summary>
        /// <returns></returns>
        public IEnumerator Execute()
        {

            float time = 0;
            while (SliderFill.color != TakeDammageColor)
            {
                time += Time.deltaTime*20;
                var color = Color.Lerp(DefaultColor, TakeDammageColor, time);
                SliderFill.color = color;
            yield return null;
            }

            yield return Wait;

            time = 0f;
            while (SliderFill.color != DefaultColor)
            {
                time += Time.deltaTime * 10;
                var color = Color.Lerp(TakeDammageColor, DefaultColor, time);
                SliderFill.color = color;
                yield return null;
            }
        }


    }
}
 
