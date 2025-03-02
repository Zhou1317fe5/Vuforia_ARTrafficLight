using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public SelectText _st;
    private void Awake()
    {
        instance = this;
    }
   
    public void SetFaile()
    {

        //_st.ShowInFoText(5);
    }
}
