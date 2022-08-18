using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class WinUI : MonoBehaviour
{
    [Header("UI References:")]
    [SerializeField] private Canvas _uiCanvas;
    [SerializeField] private TMP_Text _uiWinnerText;
    [SerializeField] private Button _uiRestartButton;

    [Header("Board Reference:")]
    [SerializeField] private Board _board;  

    private void Start()
    {
        _uiRestartButton.onClick.AddListener(() => 
        {          
            StartCoroutine(Restart());
        });
        _board.OnWinAction += OnWinEvent;

        _uiCanvas.gameObject.SetActive(false);
    }
  
    private IEnumerator Restart()
    {      
        _board.AudioSource.PlayOneShot(_board._soundOfPressButton);
        yield return new WaitForSeconds(.3f);

        SceneManager.LoadScene(0);
    }

    private void OnWinEvent(Mark mark, Color color)
    {
        _uiWinnerText.text = (mark == Mark.None)? "Nobody Wins" : mark.ToString() + " Wins";
        _uiWinnerText.color = color;

        _uiCanvas.gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        _uiRestartButton.onClick.RemoveAllListeners();
        _board.OnWinAction -= OnWinEvent;
    }
}
