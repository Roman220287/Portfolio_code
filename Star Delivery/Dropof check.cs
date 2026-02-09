using TMPro;
using UnityEngine;

public class Dropofcheck : DropOffObserver
{
    [SerializeField] private TMP_Text correctPlanetText;
    [SerializeField] private Gamemanager gamemanager;

    [SerializeField] private PackageQuestHandler packageQuestHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("package"))
        {
            GameObject.FindGameObjectWithTag("LineRenderer").SetActive(false);

            Destroy(other.gameObject);
            packageQuestHandler.AddPoint();

            /*correctPlanetText.text = "You deliverd the package to the right planet";*/
            gamemanager.DeliveredPackage = true;

            CheckDropOff();
        }
        else
        {
            correctPlanetText.text = "";
        }
    }
}
