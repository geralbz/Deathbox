using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HpSlider : MonoBehaviour
{
    Slider slide;
    Player p;

    // Start is called before the first frame update
    void Start()
    {
        slide = gameObject.GetComponent<Slider>();
        p = GameObject.FindObjectOfType<Player>();
        slide.maxValue = p.maxHP;
        slide.value = p.hp;
        Player.onPlayerHpChanged += UpdateSlider;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSlider(int i) => slide.value = i;
}
