using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject explosion;
    public Sprite deadMap;
    public AudioClip dieAudio;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DieThenGameOver()
    {
        sr.sprite = deadMap;
        Instantiate(explosion, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
        PlayerManager.Instance.isDefeatUI.SetActive(true);
        Invoke("ReturnToMenu", 3);

        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
