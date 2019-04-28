using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NiceCatchBlinker : MonoBehaviour
{
    public Text text;

   public IEnumerator Activated()
    {
        text.enabled = true;
        yield return new WaitForSeconds(0.3f);
        text.enabled = false;
        yield return new WaitForSeconds(0.1f);
        text.enabled = true;
        yield return new WaitForSeconds(0.4f);
        text.enabled = false;
        yield return new WaitForSeconds(0.1f);
        text.enabled = true;
        yield return new WaitForSeconds(0.3f);
        text.enabled = false;
      
    }
   
}
