using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private protected GameObject panel;
    public virtual void Initialize() { }

    public virtual void ActivatePanel() 
    { 
        panel.SetActive(true);
    }

    public virtual void DeactivatePanel() 
    { 
        panel.SetActive(false);
    }

    public virtual void Dispose() { }
}
