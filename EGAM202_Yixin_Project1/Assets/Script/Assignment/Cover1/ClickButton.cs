using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public ChooseCharacter targetScript;
    void Start()
    {
        targetScript = GameObject.Find("CharacterManager").GetComponent<ChooseCharacter>();
    }
    public void ClickFire()
    {
        targetScript.ClickFire();
    }

    public void ClickWater()
    {
        targetScript.ClickWater();
    }

    public void ClickElectric()
    {
        targetScript.ClickElectric();
    }
}
