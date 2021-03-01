using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    private int choice = 1;
    public Transform Pos1;
    public Transform Pos2;
    private float WaitOneSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WaitOneSecond += Time.deltaTime;
        if (WaitOneSecond < 1)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            choice = 1;
            transform.position = Pos1.position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            choice = 2;
            transform.position = Pos2.position;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("choice", choice);
            SceneManager.LoadScene(1);
        }
    }
}
