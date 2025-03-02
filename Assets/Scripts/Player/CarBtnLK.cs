using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBtnLK : MonoBehaviour
{
    public SetLight _sl;
    public int number;
    private void OnMouseDown()
    {
        _sl.SetObj(number);
    }
}
