using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DeathTMP : MonoBehaviour
{

    private TextMeshProUGUI m_DeathTMP;

    // Start is called before the first frame update
    void Awake()
    {
        m_DeathTMP = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        //transform.DOScale(1, 2f).From (0);
        //m_TextMeshPro.DOFade(1f, 0f);

        m_DeathTMP.DOFade(0f, 0f).SetDelay(0.5f); // Setze die Alpha auf 0 und verzögere um 1 Sekunde
        m_DeathTMP.DOFade(1f, 5f).SetDelay(1f); // Ändere die Alpha auf 1 über einen Zeitraum von 1 Sekunde nach der Verzögerung von 1 Sekunde
    }

    // Update is called once per frame
    void Update()
    {

    }
}
