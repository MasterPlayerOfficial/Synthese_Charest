using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int _score = default;
    [SerializeField] private TMP_Text _txtScore = default;
    [SerializeField] private TMP_Text _txtTemps = default;
    [SerializeField] private GameObject _menuPause = default;
    [SerializeField] private float _upgradeSpeed = 2.0f;
    [SerializeField] private int _upgradeSpeedScore = 1000;
    [SerializeField] private float _vitesseEnnemi1 = 5.0f;
    [SerializeField] private float _vitesseEnnemi2 = 3.0f;
    [SerializeField] private TMP_Text _txtGameOver = default;

    private bool _paused = false;
    private bool _isGameOver = false;
    private float _temps = 0;
    private BackgroundMusicManager _backgroundMusicManager;

    void Start()
    {
        _score = 0;
        UpdateScore();
        Time.timeScale = 1;
        _backgroundMusicManager = gameObject.GetComponentInChildren<BackgroundMusicManager>();
    }

    private void Update()
    {
        _temps = Time.time;
        UpdateTime();
        GestionPause();
    }

    public int GetScore()
    {
        return _score;
    }

    public void ChangerScore(int pointage)
    {
        _score += pointage; ;
        UpdateScore();
    }

    private void UpdateScore()
    {
        _txtScore.text = "Score : " + _score.ToString();
    }

    private void GestionPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_paused)
        {
            _menuPause.SetActive(true);
            Time.timeScale = 0;
            _paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _paused)
        {
            RetourJeu();
        }
    }

    private void UpdateTime()
    {
        if (!_isGameOver)
        {
            _txtTemps.text = "Temps : " + _temps.ToString("f2");
        }
    }

    public void RetourJeu()
    {
        _menuPause.SetActive(false);
        Time.timeScale = 1;
        _paused = false;
    }

    public float GetUpgradeSpeed()
    {
        return _upgradeSpeed;
    }
    
    public int GetUpgradeSpeedScore()
    {
        return _upgradeSpeedScore;
    }

    public float GetVitesseEnnemi1()
    {
        return _vitesseEnnemi1;
    }

    public float GetVitesseEnnemi2()
    {
        return _vitesseEnnemi2;
    }

    public void GameOverSequence()
    {
        _isGameOver = true;
        PlayerPrefs.SetInt("Score", _score);
        PlayerPrefs.Save();
        _txtGameOver.gameObject.SetActive(true);
        _backgroundMusicManager.EndMusic();
        StartCoroutine("GameOverRoutine");
    }
    

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(3);
    }
}
