using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool isInteractable = true;
    [SerializeField] private string tooltipMessage = "interact";

    public bool IsInteractable => isInteractable;
    public string TooltipMessage => tooltipMessage;

    public virtual void OnInteract()
    {
        Debug.Log("INTERACTED: " + gameObject.name);
    }
}
