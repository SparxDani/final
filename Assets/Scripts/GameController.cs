using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] bulletPrefabs = new GameObject[5];
    private SimplyLinkList<GameObject> bullet;
    private bool canShoot = true;
    private InputAction fireAction;

    private void Awake()
    {
        bullet = new SimplyLinkList<GameObject>();
        for (int i = 0; i < bulletPrefabs.Length; i++)
        {
            bullet.AddNodeAtEnd(bulletPrefabs[i]);
        }
    }

    private void OnEnable()
    {
        // Obtener la referencia a la acción "Fire" del nuevo sistema de entrada
        fireAction = new InputAction("Fire", binding: "<Keyboard>/p");
        fireAction.Enable();
        fireAction.started += OnFireStarted;
    }

    private void OnDisable()
    {
        // Deshabilitar y liberar la acción "Fire" del nuevo sistema de entrada
        fireAction.Disable();
        fireAction.started -= OnFireStarted;
        fireAction.Dispose();
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        if (canShoot && bullet.GetCapacity() > 0)
        {
            GameObject bulletObject = bullet.RemoveFirst();
            // Disparar el proyectil
            // ...

            // Esperar 3 segundos para poder disparar nuevamente
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }
}
