using UnityEngine;

public class CannonBallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float timeToDestroy = 5f;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerSettings playerSettings;

    public void ShootGun()
    {
        GameObject Createdball = Instantiate(cannonBall, shootPoint.position, shootPoint.rotation, transform);
        Createdball.GetComponent<Rigidbody>().velocity = shootPoint.transform.up * playerSettings.ShootPower;
        Destroy(Createdball, timeToDestroy);
    }
}
