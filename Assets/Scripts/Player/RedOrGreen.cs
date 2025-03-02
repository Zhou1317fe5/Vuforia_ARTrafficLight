using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrGreen : MonoBehaviour
{
    public Material _material_Red;
    public Material _material_Green;
    public Material _material_bigRed;
    public Material _material_bigGreen;
  
    private MeshRenderer _mR;
    private void Awake()
    {
        _mR = GetComponent<MeshRenderer>();
    }
  
  
    public void SetRedLight(int state)
    {
        if (_mR == null)
        {
            _mR = GetComponent<MeshRenderer>();
        }
        switch (state)
        {
            case 0:
                _mR.material = _material_Red;
                break;
            case 1:
                _mR.material = _material_Green;
                break;
        }
    }

}
