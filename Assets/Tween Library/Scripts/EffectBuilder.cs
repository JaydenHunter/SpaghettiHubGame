///Tomas Munro's Script
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts
{
    public class EffectBuilder
    {
        public MonoBehaviour Owner { get; set; }
        private List<IUiEffect> _effects = new List<IUiEffect>();
        //effect builder
        public EffectBuilder(MonoBehaviour owner)
        {
            Owner = owner;
        }
        /// <summary>
        /// add effect to list
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        public EffectBuilder AddEffect(IUiEffect effect)
        {
            _effects.Add(effect);
            return this;
        }
        /// <summary>
        /// execute all effects
        /// </summary>
        public void ExecuteAllEffects()
        {
            Owner.StopAllCoroutines();

            foreach (var effect in _effects)
            {

                Owner.StartCoroutine(effect.Execute());
            }

        }
       
    }
}
