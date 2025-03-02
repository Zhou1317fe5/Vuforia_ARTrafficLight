using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLight : MonoBehaviour
{
    public SelectText _selText;
    public TimeSetRed _go;
    public GameObject[] _hdCtro;
    private int currentLight=0;
    public void SetObj(int number)
    {
        if (!_hdCtro[number].activeSelf)
        {
            _go._go.SetActive(false);
        }
        else
        {
            _go._go.SetActive(true);
        }
        _go.currentLuKou = number;
        _go.gameObject.SetActive(!_go.gameObject.activeSelf);
      
    }
   public void LoadSceenNumber(int number)
    {
        if (!_hdCtro[number - 1].activeSelf)
        {
            _go._go.SetActive(true);
            currentLight++;
            _hdCtro[number - 1].SetActive(true);
 
            if (currentLight == 7)
            {

            }
        }
        else
        {
            return;
        }
      
    }
}
