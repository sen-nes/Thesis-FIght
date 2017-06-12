using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillGold : MonoBehaviour {

    public int goldValue;
    public Text goldText;
    public float displayDuration = 2f;

    private void Start()
    {
        goldText.text = "+ " + goldValue.ToString() + "g";
        Destroy(gameObject, displayDuration);
    }
}
