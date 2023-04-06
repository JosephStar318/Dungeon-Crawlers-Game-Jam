using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnActive : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerInputHelper.Instance.Eventsystem != null)
        {
            PlayerInputHelper.Instance.Eventsystem.SetSelectedGameObject(gameObject);
        }
    }
    void Start()
    {
        if (PlayerInputHelper.Instance.Eventsystem != null)
        {
            PlayerInputHelper.Instance.Eventsystem.SetSelectedGameObject(gameObject);
        }
    }

    
}
