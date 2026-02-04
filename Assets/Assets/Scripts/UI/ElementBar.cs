using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementBar : MonoBehaviour {
    [SerializeField]
    private Slider slider;
    public static Dictionary<string, ElementBar> Registry = new();
    
    void Awake()
    {
        Registry[gameObject.name] = this;
    }

    void Start()
    {
        slider.maxValue = 1.0f;
        slider.value = 1.0f;
    }
    // public void SetMaxValue(float value){
    //     slider.maxValue = value;
    //     slider.value = value;
    // }
    public void SetValue(float value){
        slider.value = value;
    }
    void OnDestroy()
    {
        Registry.Remove(gameObject.name);
    }
}