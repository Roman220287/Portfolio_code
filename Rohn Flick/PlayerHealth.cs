using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int armor;

    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject gunModel;

    [SerializeField] private int medkitHealthAdded;
    [SerializeField] private int shieldAdded;

    [SerializeField] private GameObject[] bloodVFX;
   [SerializeField] private VisualEffect[] vfx;
    private int secTilBloodGone = 3;

    public bool GoMainMenu;
    public Vector3 spawnLocation;

    public bool HasEnemyBeenHacked;

    [SerializeField] private AudioSource metalicHit;
    public GameObject hackedEnemy;

    GameObject[] enemiesLeft;
    public int killCount;

    private void Start()
    {
        spawnLocation = transform.position;
        playerModel = gameObject.transform.GetChild(1).gameObject;
        gunModel = gameObject.transform.GetChild(0).gameObject;
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        if (killCount >= enemiesLeft.Length)
        {
            SceneManager.LoadScene("New Level");
        }


        if (FindObjectOfType<MainMenu>().startGame)
        {
            transform.position = spawnLocation;
            health = 1;
            gunModel.SetActive(true);
        }

        if (bloodVFX[2] == null)
        {
            for (int i = 0; i < 3; i++)
            {
                bloodVFX[i] = gameObject.transform.GetChild(i + 2).gameObject;
            }
        }

        if (vfx[2] == null && bloodVFX[2] != null)
        {
            for (int i = 0; i < 3; i++)
            {
                vfx[i] = bloodVFX[i].GetComponent<VisualEffect>();
            }

            TurnnAllBloodOff();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            health = 99999999;
            armor = 9999999;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            StartCoroutine(BloodGoSpew());

            if (armor > 0)
            {
                armor--;
            }
            if (armor <= 0)
            {
                health--;
            }
            if (health <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator BloodGoSpew()
    {
        int whichBloodPos = Random.Range(0, 3);
        vfx[whichBloodPos].Play();

        yield return new WaitForSeconds(secTilBloodGone);

        vfx[whichBloodPos].Stop();
    }

    public void AddHealth()
    {
        health += medkitHealthAdded;
    }

    public void AddShield()
    {
        armor += shieldAdded;
    }

    private IEnumerator Death()
    {
        metalicHit.Play();
        secTilBloodGone = 0;
        playerModel.SetActive(false);
        gunModel.SetActive(false);

        // !FindObjectOfType<CameraMovementMenus>().hasSwappedPlayer
        if (HasEnemyBeenHacked && hackedEnemy != null)
        {
                StartCoroutine(FindAnyObjectByType<CameraMovementMenus>().SwapPlayerToHackedEnemy());

                yield return new WaitForSeconds(2.2f);

                transform.position = hackedEnemy.transform.position;
                transform.rotation = Quaternion.Euler(hackedEnemy.transform.rotation.eulerAngles);
                Physics.SyncTransforms();
                playerModel.SetActive(true);
                gunModel.SetActive(true);
                health = 1;
                if (hackedEnemy != null)
                {
                    Destroy(hackedEnemy.gameObject);
                }
        }
        else
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("New Level");
        }
        //GoMainMenu = true; This was for first beta playtest

    }

    private void TurnnAllBloodOff()
    {
        foreach (VisualEffect blood in vfx)
        {
            blood.Stop();
        }
    }
}
