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
    [Header("Animator")]
    [SerializeField] Animator anim;

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
        anim = GetComponent<Animator>();
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
                anim.SetTrigger("isMountain");
                break;

            case "tundra":
                anim.SetTrigger("isTundra");
                break;

            case "lac":
                anim.SetTrigger("isWater");
                break;

            case "desert":
                anim.SetTrigger("isDesert");
                break;

            case "prairie":
                anim.SetTrigger("isPlaine");
                break;

            case "foret":
                anim.SetTrigger("isForest");
                break;

            case "ocean":
                anim.SetTrigger("isWater");
                break;

            case "banquise":
                anim.SetTrigger("isBanquise");
                break;

            case "glacier":
                anim.SetTrigger("isGlacier");
                break;

            case "neutral":
                anim.SetTrigger("isWater");
                break;

            default:
                anim.SetTrigger("isWater");
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
