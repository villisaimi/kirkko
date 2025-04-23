using StarterAssets;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private float rayDistance = 2.0f;
    [SerializeField] private float raySphereRadius = 0.1f;
    [SerializeField] private LayerMask isInteractable = ~0;

    public GameObject interactPanel;

    public bool isInteracting;

    private FirstPersonController player;

    public Interactable interactable;

    private void Awake()
    {
        player = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractables();
        CheckForInput();
    }

    private void Interact()
    {
        interactable.OnInteract();
        ResetInteractable();
    }

    public void StartInput()
    {
        if (interactable == null)
            return;

        isInteracting = true;
    }

    private void CheckForInput()
    {
        if (isInteracting)
        {
            if (!interactable.IsInteractable)
                return;

            Interact();
            isInteracting = false;  
        }
      
    }

    private void ResetInteractable()
    {
        interactable = null;
        interactPanel.SetActive(false);
    }

    private void CheckForInteractables()
    {
        Ray t_ray = new(player.transform.position, player.transform.forward);
        bool t_hitSomething = Physics.SphereCast(t_ray, raySphereRadius, out RaycastHit t_hitInfo, rayDistance, isInteractable );

        if (t_hitSomething)
        {
            Interactable t_interactable = t_hitInfo.transform.GetComponent<Interactable>();

            if (t_interactable != null)
            {
                interactable = t_interactable;
                interactPanel.SetActive(true);
            }
        }
        else
        {
            ResetInteractable();
        }

        Debug.DrawRay(t_ray.origin, t_ray.direction * rayDistance, t_hitSomething ? Color.green : Color.red);  
    }
}
