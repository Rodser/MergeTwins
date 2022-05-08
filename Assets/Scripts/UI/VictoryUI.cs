using Rodser.MergeTwins;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Button play;

    private void Start()
    {
        this.play.onClick.AddListener(PlayInfinityLevel);
    }

    private void PlayInfinityLevel()
    {
        Debug.Log("Infinity");
        Time.timeScale = 1;
        Game.GameManager.SceneUI.SetStartConfig();
        this.gameObject.SetActive(false);
        Game.OnClickButton(this);
        Game.IsPlaying = true;
    }
}
