using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArChi
{
    public class UICharacterComponent : MonoBehaviour, IUICharacter
    {
        [SerializeField] private Slider healthSlider;

        public void SetMaxHealth(float value)
        {
            healthSlider.maxValue = value;
            healthSlider.value = value;
        }
        public void UpdatedHealth(float health)
        {
            healthSlider.value = health;
        }
    }
}
