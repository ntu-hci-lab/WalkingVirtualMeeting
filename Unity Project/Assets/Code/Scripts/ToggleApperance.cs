using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleApperance : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject visualEffect;
    [SerializeField] private float cd;
    [SerializeField] private CloudDanger cloudDanger;
    private bool toggle = true;
    private bool cooldown = false;
    public void toggleElement()
    {
        if (!cooldown)
        {
            if (toggle)
            {
                target.SetActive(false);
                cloudDanger.toggle(false);
                visualEffect.SetActive(true);
            }
            else
            {
                target.SetActive(true);
                cloudDanger.toggle(true);
                visualEffect.SetActive(false);
            }
            toggle = !toggle;
            //cooldown = true;
            //StartCoroutine(cool());
        }
    }
    IEnumerator cool()
    {
        print("cool start");
        yield return new WaitForSeconds(cd);
        cooldown = false;
        print("cool end");
    }
}
