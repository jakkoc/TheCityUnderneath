using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar: MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();    
    }

    //Health from 0 to 100
    public void SetHealth(int health)
    {
        image.fillAmount = health / 100.0f;
    }
}
