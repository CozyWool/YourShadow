using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public TextMeshProUGUI LoadingPercentage;
    public Image LoadingProgressBar;


    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnim = false;

    private AsyncOperation loadSceneOperation;
    private Animator componentAnim;
    public static void SwitchToScene(string sceneName)
    { 
        instance.componentAnim.SetTrigger("Close");

        instance.loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadSceneOperation.allowSceneActivation = false;
    }

    void Start()
    {
        instance = this;
        componentAnim = GetComponent<Animator>();
        
        if (shouldPlayOpeningAnim) componentAnim.SetTrigger("Open");
      
    }

    
    void Update()
    {
        if (loadSceneOperation != null) 
        {
            LoadingPercentage.text = Mathf.RoundToInt(loadSceneOperation.progress * 100) + "%";
            LoadingProgressBar.fillAmount = loadSceneOperation.progress;
        }
       
    }
    
    public void OnAnimationOver()
    {
        shouldPlayOpeningAnim = true; 
        loadSceneOperation.allowSceneActivation = true;
    }
}
