using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMode : BonusInfo
{
    [SerializeField] private float speedOfGameModificator = 0.5f;
    public override void StartBonus()
    {
        Time.timeScale = speedOfGameModificator; // ��������� �������� ���� � 2 ����
        StartCoroutine(TimeForBonus());
    }

    protected override void StopBonus()
    {
        Time.timeScale = 1f; // ���������� �������� ���� � �����
    }
}
