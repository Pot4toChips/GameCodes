using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject cam;

    private float h, v;
    public Rigidbody2D rb;
    public float speed;

    public GameObject gunTip;
    public GameObject bulletPrefab;
    private bool nextShoot = true;

    private int currentGun;
    private Gun gun = GunContainer.pistol;

    public static float hp;
    public ParticleSystem particle;
    private float ammo;
    private float magazine;
    private float reloadTime;
    private bool isReloading;

    public Text ammoText;
    public Text scoreText;
    public Slider hpBar;
    public static int score;

    public static bool gameOver;
    public SpriteRenderer sprite;
    public GameObject gameOverMenu;
    public Transform posIn, posOut;

    public Color color;
    private Color originalColor;
    public AudioSource shootAudioSource;

    private void Start()
    {
        originalColor = sprite.color;
        rb.bodyType = RigidbodyType2D.Dynamic;
        GunSetup();
        hp = 100;
    }
    private void FixedUpdate()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10), 0.3f);

        if (gameOver == false)
        {
            gameOverMenu.transform.position = Vector2.Lerp(gameOverMenu.transform.position, posOut.position, 0.3f);
            scoreText.text = score.ToString();

            Movement();
            Shooting();
            Die();
        }
        else
        {
            gameOverMenu.transform.position = Vector2.Lerp(gameOverMenu.transform.position, posIn.position, 0.3f);
            if (Input.GetKey(KeyCode.Space))
            {
                Restart();
            }
        }
        if (hp > 0)
        {
            hpBar.value = Mathf.Lerp(hpBar.value, hp, 0.2f);
        }
        else
        {
            hpBar.value = Mathf.Lerp(hpBar.value, 0, 0.2f);
        }
    }
    private void Movement()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(h, v) * speed;

        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    private void Shooting()
    {
        if (Input.GetButton("Fire1") && nextShoot == true && isReloading == false)
        {
            if (ammo > 0)
            {
                ammo--;
                StartCoroutine(autoShooter());
                nextShoot = false;
            }
            else
            {
                StartCoroutine(reload());
            }
        }
        if (Input.GetKey(KeyCode.R) && isReloading == false)
        {
            StartCoroutine(reload());
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            gun = GunContainer.pistol;
            GunSetup();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            gun = GunContainer.smg;
            GunSetup();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            gun = GunContainer.rifle;
            GunSetup();
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            gun = GunContainer.shotgun;
            GunSetup();
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            gun = GunContainer.sniper;
            GunSetup();
        }

        if (gun == GunContainer.sniper)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 12, 0.2f);
        }
        else if (Camera.main.orthographicSize != 7)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 7, 0.2f);
        }

        if (gun == GunContainer.pistol)
        {
            speed = Mathf.Lerp(speed, 10, 0.2f);
        }
        else if (speed != 7)
        {
            speed = Mathf.Lerp(speed, 7, 0.2f);
        }
        ammoText.text = ammo.ToString() + " / " + magazine.ToString();
    }
    private void GunSetup()
    {
        ammo = gun.GetMagazine();
        magazine = gun.GetMagazine();
        reloadTime = gun.GetReloadTime();
    }
    private void Die()
    {
        if (hp <= 0)
        {
            particle.Play();

            rb.bodyType = RigidbodyType2D.Static;
            gameOver = true;
            sprite.enabled = false;
        }
    }
    private void Restart()
    {
        gameOver = false;
        shootAudioSource.Stop();
        score = 0;
        scoreText.text = "0";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator autoShooter()
    {
        Bullet.bForce = gun.GetSpeed();
        Bullet.bDamage = gun.GetDamage();

        for (int i = 0; i < gun.GetBullets(); i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunTip.transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + Random.Range(-gun.GetSpread() / 2, gun.GetSpread() / 2), transform.rotation.w));
            bullet.GetComponent<Bullet>().parent = gameObject;
            bullet.GetComponent<SpriteRenderer>().color = originalColor;
        }
        shootAudioSource.Play();

        yield return new WaitForSeconds(gun.GetFireRate());
        nextShoot = true;
    }
    private IEnumerator reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammo = magazine;
        isReloading = false;
    }
    private IEnumerator loseHP()
    {
        originalColor = sprite.color;
        sprite.color = color;
        yield return new WaitForSeconds(0.05f);
        sprite.color = originalColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Bullet(Clone)" && collision.gameObject.GetComponent<Bullet>().parent != gameObject)
        {
            hp -= collision.gameObject.GetComponent<Bullet>().damage;
            if (sprite.color != color)
            {
                StartCoroutine(loseHP());
            }
            Destroy(collision.gameObject);
        }
    }
}
