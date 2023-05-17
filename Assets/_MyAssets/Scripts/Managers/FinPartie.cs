using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinPartie : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private GameObject _txtSaisie = default;

    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = PlayerPrefs.GetInt("Score");
        _txtScore.text = "Your Score : " + _score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Permet de redémarrer la partie ou se déplacer au menu de départ à la fin de la partie
        if (Input.GetKeyDown(KeyCode.R) && !_txtSaisie.activeSelf)
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !_txtSaisie.activeSelf)
        {
            SceneManager.LoadScene(0);
        }
    }
}
