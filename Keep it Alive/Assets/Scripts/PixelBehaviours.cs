using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBehaviours : MonoBehaviour
{

    #region param
    [SerializeField] string startState = "";

    [Header ("Les éléments affectant la cellule")]
    [SerializeField] private List<string> affect;

    [Space]
    [Header ("Incrementation")]
    [SerializeField] private float sun, moon, wind, rain = 0;

    [Space]
    [Header("limite pour changer la couleur")]
    [SerializeField] private int valForChangeColor = 4;

    [Space]
    [Header("retour à 0")]
    public AnimationCurve reduce ;

    [Space]
    [Header("Les couleurs")]
    [SerializeField] MyColor[] myColor;
    [SerializeField] MyImage[] myImg;

    //Variable pour modifier les couleurs et les sprites
    SpriteRenderer mySprite;
    Renderer spotMat;

    #endregion


    #region Update ||Start
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        spotMat = GetComponent<Renderer>();
        UpdateLand(startState);
        //mySprite.color = Color.red;
    }
    private void Update()
    {
        if(affect.Count <= 0)
        {
            Debug.Log("ok");
            GoBackToNormal();
        }
        
    }
    #endregion

    public void UpdateValues(string objectTag)
    {
        Debug.Log(objectTag);
        
            switch (objectTag)
            {
                case "SUN":
                    sun++;
                    //affect.Add("SUN");
                    break;

                case "MOON":
                    moon++;
                    //affect.Add("MOON");
                    break;

                case "WIND":
                    wind++;
                    //affect.Add("WIND");
                    break;

                case "RAIN":
                    rain++;
                    //affect.Add("RAIN");
                    break;

                default:
                    Debug.Log("stop");
                    break;
            }        
        VerifColor();
    }

    void VerifColor()
    {
        if(sun >= valForChangeColor && sun > rain && sun > wind && sun > moon)
        {
            UpdateLand("desert");
        }
        else if (moon >= valForChangeColor && moon >= rain && moon > wind && moon > sun)
        {
            UpdateLand("montagne");
        }
        else if (rain >= valForChangeColor && rain >= moon && rain > wind && rain > sun)
        {
            UpdateLand("lac");
        }
        else if (wind >= valForChangeColor && wind >= moon && wind > rain && wind > sun)
        {
            UpdateLand("tundra");
        }
        else if ((sun >= valForChangeColor && moon >= valForChangeColor) && (sun > rain && moon > rain) && (sun > wind && moon > wind))
        {
            UpdateLand("neutral");
        }
        else if ((sun >= valForChangeColor && wind >= valForChangeColor) && (sun > rain && wind > rain) && (sun > moon && wind > moon))
        {
            UpdateLand("prairie");
        }
        else if ((sun >= valForChangeColor && rain >= valForChangeColor) && (sun > wind && rain > wind) && (sun > moon && rain > moon))
        {
            UpdateLand("foret");
        }
        else if ((moon >= valForChangeColor && wind >= valForChangeColor) && (moon > sun && wind > sun) && (moon > rain && wind > rain))
        {
            UpdateLand("glacier");
        }
        else if ((moon >= valForChangeColor && rain >= valForChangeColor) && (moon > sun && rain > sun) && (moon > wind && rain > wind ))
        {
            UpdateLand("ocean");
        }
        else if ((rain >= valForChangeColor && wind >= valForChangeColor) && (rain > sun && wind > sun) && (rain > moon && wind > moon))
        {
            UpdateLand("banquise");
        }
    }

    private void UpdateLand(string newLand)
    {
        switch (newLand) {
            case "montagne":
                mySprite.sprite = myImg[0].Image;
                spotMat.material.color = myColor[0].Color;

                break;

            case "tundra":
                mySprite.sprite = myImg[1].Image;
                spotMat.material.color = myColor[1].Color;
                break;

            case "lac":
                mySprite.sprite = myImg[2].Image;
                spotMat.material.color = myColor[2].Color;
                break;

            case "desert":
                mySprite.sprite = myImg[3].Image;
                spotMat.material.color = myColor[3].Color;
                break;

            case "prairie":
                mySprite.sprite = myImg[4].Image;
                spotMat.material.color = myColor[4].Color;
                break;

            case "foret":
                mySprite.sprite = myImg[5].Image;
                spotMat.material.color = myColor[5].Color;
                break;

            case "ocean":
                mySprite.sprite = myImg[6].Image;
                spotMat.material.color = myColor[6].Color;
                break;

            case "banquise":
                mySprite.sprite = myImg[7].Image;
                spotMat.material.color = myColor[7].Color;
                break;

            case "glacier":
                mySprite.sprite = myImg[8].Image;
                spotMat.material.color = myColor[8].Color;
                break;

            case "neutral":
                mySprite.sprite = myImg[9].Image;
                spotMat.material.color = myColor[9].Color;
                break;

            default:
                mySprite.sprite = myImg[9].Image;
                //spotMat.material.color = myColor[9].Color;
                
                break;
        }
    }

    void GoBackToNormal()
    {
        //Debug.Log(Time.deltaTime);
        if(sun >0)
            sun -= reduce.Evaluate(Time.deltaTime);

        if (moon > 0)
            moon -= reduce.Evaluate(Time.deltaTime);

        if (rain > 0)
            rain -= reduce.Evaluate(Time.deltaTime);

        if (wind > 0)
            wind -= reduce.Evaluate(Time.deltaTime); 
    }


    #region Ontrigger
    private void OnTriggerEnter(Collider other)
    {
        //ajoute l'élément dans la liste affect
        if(other.tag != null)
        {
            affect.Add(other.gameObject.name);
            UpdateValues(other.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //retire l'élément dans la liste affect
        if (other.tag != null)
        {
            affect.Remove(other.gameObject.name);
        }
    }
    #endregion
}
