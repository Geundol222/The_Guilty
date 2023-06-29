using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject UIObject; 

    ParticleSystem particle;
    Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    public void Interact()
    {
        GameManager.Resource.Destroy(particle.gameObject);
        col.enabled = false;
        ItemPopUpUI itemPopUpUI = GameManager.UI.ShowPopUpUI<ItemPopUpUI>("UI/PopUpUI/ItemPopUpUI");
        itemPopUpUI.ShowItem(UIObject);
    }
}
