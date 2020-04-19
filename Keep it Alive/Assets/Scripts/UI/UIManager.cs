using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public MenuPage currentPage;
    private List<MenuPage> previousPages;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
            Debug.LogWarning("Multiple UI Manager");

        instance = this;
        previousPages = new List<MenuPage>();
    }

    // Update is called once per frame

    public void SwapToPage(MenuPage page){
        previousPages.Add(currentPage);
        currentPage.gameObject.SetActive(false);
        
        currentPage = page;
        currentPage.gameObject.SetActive(true);
    }

    public void Return(){
        if(previousPages.Count <= 0)
            return;
        currentPage.gameObject.SetActive(false);
        currentPage = previousPages[previousPages.Count -1];
        currentPage.gameObject.SetActive(true);
    }
}
