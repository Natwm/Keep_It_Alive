using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    public int GameSceneId;
    public void OnClick(){
        SceneManager.LoadScene(GameSceneId);
    }
}
