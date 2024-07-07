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
    private int _currentLevel;
    private float _timer=0;
    private void Start()
    {
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
    }

    private void ChangeToNextLevel()
    {
        //todo
        Levels[_currentLevel].SetActive(false);
        
        if (++_currentLevel >= Levels.Count)
        {
            Debug.Log("Level Finished!");
            _currentLevel = 0;
        }
        
        //todo
        Levels[_currentLevel].SetActive(true);
    }
}
