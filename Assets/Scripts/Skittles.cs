using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skittles : MonoBehaviour
{
    [SerializeField]
    private GameObject _skittlePrefab;
    [SerializeField]
    private Transform _skittleParent;
    [SerializeField]
    private Collider _finishCollider;

    [SerializeField]
    private TextMeshProUGUI _score;
    [SerializeField]
    private TextMeshProUGUI _TextStrike;

    [SerializeField]
    private Vector3[] _spawnSkittle;

    private List<GameObject> _Skittles = new List<GameObject>();

    [SerializeField]
    private float _respawnDelay = 3f;

    [SerializeField]
    private int _bonus = 10;

    private int cnt = 0;
    [SerializeField]
    private int skittlesInField = 10;
    private int bonusPointThrows = 0;
    private int prevSkt = 0;
    private int tmpcnt = 0;

    private GameObject _skittle;

    void Start()
    {
        Spawn();
        _score.text = cnt.ToString();
    }

    void Update()
    {

       if (skittlesInField == 0)
       {
            _Skittles.Clear();
            StartCoroutine(Reloader());
            skittlesInField = 10;
       }

        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            Debug.Log("чтото1");
            StartCoroutine(CheckPinsAfterDelay());
        }
    }

    private IEnumerator Reloader()
    {
        yield return new WaitForSeconds(_respawnDelay);
        Spawn();
    }

    private IEnumerator CheckPinsAfterDelay()
    {
        yield return new WaitForSeconds(5f); 
        SkittlesIsFallen();
    }

    private IEnumerator StrikeOrSpare(String text)
    {
        _TextStrike.text = text;
        yield return new WaitForSeconds(4);
        _TextStrike.text = "";
    }

    private void SkittlesIsFallen()
    {

        tmpcnt = 0;
        for (int i = 0; i < _Skittles.Count; i++)
        {
            if (_Skittles[i] != null && (Vector3.Dot(_Skittles[i].transform.up, Vector3.up) < 0.9f || _Skittles[i].transform.position.y < 0))
            {
                Destroy(_Skittles[i]);
                skittlesInField -= 1;
                cnt += 1;
                tmpcnt += 1;
                _score.text = cnt.ToString();
            }
        }
        
        if (bonusPointThrows > 0)
        {
            cnt += _bonus;
            _score.text = cnt.ToString();
            bonusPointThrows -= 1;
        }


        if (tmpcnt == 10)
        {
            bonusPointThrows = 2;
            cnt += 10;
            _score.text = cnt.ToString();
            StartCoroutine(StrikeOrSpare("Strike!!!"));
        } 
        else if (tmpcnt + prevSkt == 10)
        {
            bonusPointThrows = 1;
            cnt += 10;
            _score.text = cnt.ToString();
            StartCoroutine(StrikeOrSpare("Spare!"));
        }
        prevSkt = tmpcnt;

        
        //Debug.Log(bonusPointThrows);
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnSkittle.Length; i++)
        {
            _skittle = Instantiate(_skittlePrefab, _skittleParent);
            _skittle.transform.position = _skittleParent.position;
            _skittle.transform.position += _spawnSkittle[i];
            _Skittles.Add(_skittle);
        }
    }

}