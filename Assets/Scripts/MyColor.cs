using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyColor
{
    public int price = 0;
    public Color color = Color.white;
    public bool bought = false;

    public MyColor(int price, Color color)
    {

        this.price = price;
        this.color = color;
        if (price == -1)
        {
            bought = true;
        }
        else
        {
            bought = false;
        }

    }
}

public enum colors
{
    white,
    red,
    green,
    blue,
};






