using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private Transform bulletPos;
    [SerializeField] private LayerMask layerMask;
    private InputControls inputControls;
    void Start()
    {
        inputControls = GameManager.instance.inputManager.inputControls;

        inputControls.Shoot.Fire.performed += ShootHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShootHandler(InputAction.CallbackContext obj)
    {
        print("Fire!");
        RaycastHit hit;
        if (Physics.Raycast(bulletPos.position, bulletPos.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.red, 1);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(bulletPos.position, bulletPos.TransformDirection(Vector3.back) * 1000, Color.white, 1);
            Debug.Log("Did not Hit");
        }
    }

}
