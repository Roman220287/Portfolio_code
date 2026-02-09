using System.Collections;
using TMPro;
using UnityEngine;

public class Fueldepletion : MonoBehaviour, IButtonInput
{
    [SerializeField] private TMP_Text fuelText;
    [SerializeField] private GameObject sosCanvas;

    [SerializeField] private GameObject bigShip;
    [SerializeField] private GameObject sphere;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float depleationTimer;

    public float Fuel;

    void OnEnable()
    {
        GameObject.FindGameObjectWithTag("InputManagers").GetComponent<ButtonInputSubject>().SetListeners(this); //input
    }

    void OnDisable()
    {
        GameObject.FindGameObjectWithTag("InputManagers").GetComponent<ButtonInputSubject>().RemoveListeners(this); //input
    }

    void Update()
    {
        FuelDropping();
    }

    private void FuelDropping() //makes the fuel drop with 1% every second, and show it with tmp
    {
        if (Fuel >= 0)
        {
            Fuel -= depleationTimer * Time.deltaTime;
        }

        if (Fuel >= 100)
        {
            Fuel = 100;
        }
        fuelText.text = Fuel.ToString("F0") + "%";
        NoFuelLeft();
    }

    private void NoFuelLeft()
    {
        if (Fuel <= 0.01)
        {
            sosCanvas.SetActive(true);
            rb.isKinematic = true;
        }
    }

    private void PressedX(int button) //when button pressed the fade in and timer start
    {
        if (button == 0 && Fuel < 1)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(WaitASec());
        }
    }

    IEnumerator FadeIn() //makes the inside sphere black 
    {
        Renderer rend = sphere.transform.GetComponent<Renderer>();

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            rend.material.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }

    IEnumerator FadeOut() //makes the inside sphere transparant
    {
        Renderer rend = sphere.transform.GetComponent<Renderer>();

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            rend.material.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }

    IEnumerator WaitASec() //a small timer before the rest happens
    {
        yield return new WaitForSeconds(1.5f);
        transform.position = bigShip.transform.position;
        StartCoroutine(FadeOut());
        Fuel = 100;
        sosCanvas.SetActive(false);
        rb.isKinematic = false;
    }

    public void OnButton(int button, bool state) //input
    {
        PressedX(button);
    }
}
