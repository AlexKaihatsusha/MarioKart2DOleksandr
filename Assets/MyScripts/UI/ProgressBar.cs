using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public float maximum;
    public float current;
    public Image image;

    public PlayerBoostComponent boostComponentRef;
    // Start is called before the first frame update
    void Start()
    {
        maximum = boostComponentRef.boostAmount;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        current = boostComponentRef.GetCurrentBoostAmount();
    }

    public void GetCurrentFill()
    {
        float fillamount = current / maximum;
        image.fillAmount = fillamount;
    }
    
}
