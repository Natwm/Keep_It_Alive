using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    public int GameSceneId;
    public MenuPage gamePage;
    public GameObject planet;
    public void OnClick(){
        Camera.main.GetComponent<FollowTarget>().aim.transform.position = planet.transform.position - Vector3.forward * 10;
        UIManager.instance.SwapToPage(gamePage);
        //SceneManager.LoadScene(GameSceneId);
    }
}
