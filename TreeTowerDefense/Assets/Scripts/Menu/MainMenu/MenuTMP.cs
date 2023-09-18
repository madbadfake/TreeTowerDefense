using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MenuTMP : MonoBehaviour
{

    private TextMeshProUGUI m_MenuTMP;

    // Start is called before the first frame update
    void Awake()
    {
        m_MenuTMP = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        //transform.DOScale(1, 2f).From (0);
        //m_TextMeshPro.DOFade(1f, 0f);

        m_MenuTMP.DOFade(0f, 0f).SetDelay(0.5f); // Setze die Alpha auf 0 und verz�gere um 1 Sekunde
        m_MenuTMP.DOFade(1f, 3f).SetDelay(4f); // �ndere die Alpha auf 1 �ber einen Zeitraum von 1 Sekunde nach der Verz�gerung von 1 Sekunde
    }

    // Update is called once per frame
    void Update()
    {

    }
}
