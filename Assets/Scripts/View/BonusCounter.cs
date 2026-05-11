using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusCounter : MonoBehaviour
{
    private int _bonuses = 0;
    [SerializeField] private TextMeshProUGUI _textBonusCnt;

    public void ChangeCount()
    {
        _bonuses += 1;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_textBonusCnt != null)
            _textBonusCnt.text = _bonuses.ToString();
    }

    void Awake()
    {
        UpdateUI();
    }
}
