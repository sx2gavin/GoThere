using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class JewelCount : MonoBehaviour
{
    public int jewelCount = 0;

    private TextMeshProUGUI jewelCountText;
    // Start is called before the first frame update
    void Start()
    {
        jewelCountText = GetComponent<TextMeshProUGUI>();
        jewelCountText.text = jewelCount.ToString();
    }

    public void CollectJewel()
    {
        jewelCount++;
        jewelCountText.text = jewelCount.ToString();
    }
}
