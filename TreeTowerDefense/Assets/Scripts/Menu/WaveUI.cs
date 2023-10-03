using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    private GameObject waveManager;

    // Start is called before the first frame update
    void Start()
    {
        waveManager = GameObject.Find("WaveManager");
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = waveManager.GetComponent<WaveManager>().waveCount.ToString();
    }
}
