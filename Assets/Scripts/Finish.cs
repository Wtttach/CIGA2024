using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public List<GameObject> Levels;
    public List<float> LevelRotationKey;
    public Vector3 Level2PlatformTargetPos;
    public Transform Level2Platform;
    public float Level3key1;
    public float Level3key2;
    public float Level3key3;
    public Transform Level3Platform;
    public Vector3 Level3PlatformTargetPos1;
    public Vector3 Level3PlatformTargetPos2;
    private int _finalFlag = 0;
    private int _currentLevel;
    private float _timer=0;
    public AudioSource FinishAudio = null;
    private void Start()
    {
        Debug.Log(1|2|4);
        _currentLevel = 0;
        Levels[_currentLevel].SetActive(true);
        Debug.Log(Levels[_currentLevel].transform.eulerAngles);
    }

    private void FixedUpdate()
    {
        Jugde();
    }

    private void Jugde()
    {
        if (_currentLevel == 0)
        {
            if (Mathf.Abs(Levels[0].transform.eulerAngles.y - LevelRotationKey[0]) < 2f ||
                Mathf.Abs(Levels[0].transform.eulerAngles.y - LevelRotationKey[0]) > 358f)
            {
                _timer += Time.deltaTime;
                if (_timer >= 0.6)
                {
                    ChangeToNextLevel();
                    _timer = 0;
                }
            }
            else
            {
                _timer = 0;
            }
        }
        else if (_currentLevel == 1)
        {
            Debug.Log(Level2Platform.position);
            if (Vector3.Distance(Level2Platform.position, Level2PlatformTargetPos) < 0.5f&&
                (Mathf.Abs(Levels[1].transform.eulerAngles.y - LevelRotationKey[1]) < 2f ||
                 Mathf.Abs(Levels[1].transform.eulerAngles.y - LevelRotationKey[1]) > 358f))
            {
                _timer += Time.deltaTime;
                if (_timer >= 0.6)
                {
                    ChangeToNextLevel();
                    _timer = 0;
                }
            }
            else
            {
                _timer = 0;
            }
        }
        else if (_currentLevel == 2)
        {
            Debug.Log(Level3Platform.position);
            Debug.Log(Levels[2].transform.eulerAngles.y);
            if (Vector3.Distance(Level3Platform.position, Level3PlatformTargetPos1) < 0.5f &&
                (Mathf.Abs(Levels[2].transform.eulerAngles.y - Level3key1) < 2f ||
                 Mathf.Abs(Levels[2].transform.eulerAngles.y - Level3key1) > 358f))
            {
                _timer += Time.deltaTime;
                if (_timer >= 0.6)
                {
                    Debug.LogError("1!");
                    _finalFlag|=1;
                    _timer = 0;
                }
            }
            if (Vector3.Distance(Level3Platform.position, Level3PlatformTargetPos2) < 0.5f &&
                (Mathf.Abs(Levels[2].transform.eulerAngles.y - Level3key2) < 2f ||
                 Mathf.Abs(Levels[2].transform.eulerAngles.y - Level3key2) > 358f))
            {
                _timer += Time.deltaTime;
                if (_timer >= 0.6)
                {
                    Debug.LogError("2");
                    _finalFlag|=2;
                    _timer = 0;
                }
            }

            if (Mathf.Abs(Levels[2].transform.eulerAngles.y - Level3key3) < 2f ||
                Mathf.Abs(Levels[2].transform.eulerAngles.y - Level3key3) > 358f)
            {
                _timer += Time.deltaTime;
                if (_timer >= 0.6)
                {
                    Debug.LogError("3!");
                    _finalFlag|=4;
                    _timer = 0;
                }
            }
            if (_finalFlag==7)
            {
                Debug.LogError("finish!");
                Levels[2].SetActive(false);
            }
        }
    }

    private void ChangeToNextLevel()
    {
        //todo
        Levels[_currentLevel].GetComponent<Animator>().SetBool("End", true);
        FinishAudio.Play();
        
        if (++_currentLevel >= Levels.Count)
        {
            Debug.Log("Level Finished!");
            _currentLevel = 0;
        }
        
        //todo
        Levels[_currentLevel].SetActive(true);
        Levels[_currentLevel].GetComponent<Animator>().SetBool("Start", true);
    }
    
}

