using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private Transform bulletPos;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject bulletDirDebug;
    [SerializeField] private TrailRenderer bulletTrail;
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
        // if (Physics.Raycast(bulletPos.position, bulletPos.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.red, 1);
        //     Debug.Log("Did Hit");
        // }
        // else
        // {
        //     Debug.DrawRay(bulletPos.position, bulletPos.TransformDirection(Vector3.back) * 1000, Color.white, 1);
        //     Debug.Log("Did not Hit");
        // }

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            TrailRenderer trail = Instantiate(bulletTrail, bulletPos.position, Quaternion.identity);
            StartCoroutine(SpawnBulletTrail(trail, hit));
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue, 1);
            hit.collider.gameObject.GetComponent<IDamageable>().OnDamage();
        }
        else
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * 1000, Color.white, 1);
            Debug.Log("Did not Hit");
        }
        bulletDirDebug.transform.position = hit.point;
    }

    private IEnumerator SpawnBulletTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPoint = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, hit.point, time); 
            time += Time.deltaTime / trail.time;
            yield return null;
        }
    }   
}
