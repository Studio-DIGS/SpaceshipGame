using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeSlider : MonoBehaviour
{
    [SerializeField] SliderManager sliderManager;
    void Start()
    {
        this.gameObject.GetComponent<Slider>().value = sliderManager.volume;
    }
}
