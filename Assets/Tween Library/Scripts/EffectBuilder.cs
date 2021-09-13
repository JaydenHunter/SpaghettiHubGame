using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts
{
    public class EffectBuilder
    {
        public MonoBehaviour Owner { get; set; }
        private List<IUiEffect> _effects = new List<IUiEffect>();
        public EffectBuilder(MonoBehaviour owner)
        {
            Owner = owner;
        }

        public EffectBuilder AddEffect(IUiEffect effect)
        {
            _effects.Add(effect);
            return this;
        }
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
