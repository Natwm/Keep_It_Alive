using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyImage 
{
    [SerializeField] string name;
    [SerializeField] Sprite image;

    #region GETTER && SETTER
    public Sprite Image { get => image; set => image = value; }
    public string Name { get => name; set => name = value; }
    #endregion
}
