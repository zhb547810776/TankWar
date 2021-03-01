using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int PlayerOneHP = 3;
    public int PlayerTwoHP;
    public int PlayerScore = 0;
    public bool PlayerOneisDead;
    public bool PlayerTwoisDead;
    public bool isDefeat;

    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    public GameObject PlayerBorn;
    public Text playerScoreTex;
    public Text playerOneHPTex;
    public Text playerTwoHPTex;
    public GameObject isDefeatUI;

    private void Awake()
    {
        instance = this;
        InitPlayerTwoproperty();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            return;
        }
        if (PlayerOneisDead || PlayerTwoisDead)
        {
            Recover();
        }
        playerScoreTex.text = PlayerScore.ToString();

        playerOneHPTex.text = PlayerOneHP.ToString();

        playerTwoHPTex.text = PlayerTwoHP.ToString();
    }

    private void Recover()
    {
        if(PlayerOneHP < 1 && PlayerTwoHP < 1)
        {
            isDefeat = true;
            isDefeatUI.SetActive(true);
            Invoke("ReturnToMenu", 3);

        }
        if(PlayerOneisDead && PlayerOneHP >= 1)
        {
            PlayerOneHP--;
            GameObject born = Instantiate(PlayerBorn, new Vector3(-2, -8, 0), Quaternion.identity);
            born.GetComponent<Born>().isCreatePlayerOne = true;
            PlayerOneisDead = false;
        }

        if (PlayerTwoisDead && PlayerTwoHP >= 1)
        {
            PlayerTwoHP--;
            GameObject born = Instantiate(PlayerBorn, new Vector3(2, -8, 0), Quaternion.identity);
            born.GetComponent<Born>().isCreatePlayerTwo = true;
            PlayerTwoisDead = false;
        }
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void InitPlayerTwoproperty()
    {
        if (PlayerPrefs.GetInt("choice") == 2)
        {
            PlayerTwoHP = 3;
        }
        else
        {
            PlayerTwoHP = 0;
        }
    }
}
