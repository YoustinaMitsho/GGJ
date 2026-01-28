using UnityEngine;
using TMPro;


public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;


    //bools 
    bool shooting, readyToShoot, reloading;


    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;


    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public LineRenderer tracer;
    public AudioSource audioSource;

    public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        camShake = FindAnyObjectByType<CamShake>();
    }
    private void Update()
    {
        MyInput();


        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();


        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;


        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        //RayCast
        /*if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);


            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("Hit Enemy for " + damage);
        }*/

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
                Debug.Log("Hit Enemy for " + damage);
            }

            // Bullet hole always appears on what you hit
            Instantiate(bulletHoleGraphic,
                rayHit.point + rayHit.normal * 0.01f,
                Quaternion.LookRotation(rayHit.normal));
            audioSource.Play();
        }

        //ShakeCamera
        if (camShake != null)
        {
            StartCoroutine(camShake.Shake(camShakeDuration, camShakeMagnitude));
        }
        else
        {
            Debug.LogError("CamShake not assigned!");
        }

        //Graphics
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        GameObject flash = Instantiate(muzzleFlash, attackPoint.position,
                              attackPoint.rotation, attackPoint);

        Destroy(flash, 0.05f);

        LineRenderer line = Instantiate(tracer, attackPoint.position, Quaternion.identity);

        line.SetPosition(0, attackPoint.position);
        line.SetPosition(1, rayHit.point);

        Destroy(line.gameObject, 0.05f);




        bulletsLeft--;
        bulletsShot--;


        Invoke("ResetShot", timeBetweenShooting);


        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
