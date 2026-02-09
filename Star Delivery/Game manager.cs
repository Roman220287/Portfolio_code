using UnityEngine;

public class Gamemanager : MonoBehaviour, ISetDestinationInfo
{
    [SerializeField] private GameObject[] planets;
    [SerializeField] private GameObject destinationMarker;
    [SerializeField] private GameObject package;
    [SerializeField] private GameObject packagePickupLocation;
    [SerializeField] private GrabAndDrop grabAndDrop;
    private GameObject LineGuide;
    private LineRenderer lineRenderer;
    public bool PickedUpPackage;
    public bool DeliveredPackage;
    public bool isLineActive;
    public void SetDestinationName(string DestinationName)
    {
        GetPlanet(DestinationName);

    }
    private void Start()
    {
        LineGuide = GameObject.FindGameObjectWithTag("LineRenderer");
        lineRenderer = GameObject.FindGameObjectWithTag("LineRenderer").GetComponent<LineRenderer>();
        LineGuide.SetActive(false);
        isLineActive = false;
    }

    void Update()
    {
        if (isLineActive)
            lineRenderer.SetPosition(0, GameObject.FindGameObjectWithTag("LineFollowTransform").transform.position);





        /* if (grabAndDrop.isCollected == true)
         {
             PickedUpPackage = true;
         }
         if (PickedUpPackage)
         {
             *//*SetMarker();*//*
         }

         if (DeliveredPackage)
         {
             DeliverdPackage();
         }*/
    }

    private void SetMarker(Transform PackageDestination)
    {
        if (PackageDestination == null)
        {
            isLineActive = false;
            LineGuide.SetActive(false);
        }
        else if (PackageDestination != null)
        {

            destinationMarker.transform.position = PackageDestination.position;
            LineGuide.SetActive(true);
            isLineActive = true;
            LineGuide.GetComponent<LineRenderer>().SetPosition(1, PackageDestination.transform.position);
            PickedUpPackage = false;
        }
    }

    private void GetPlanet(string PackageDestination)
    {

        if (PackageDestination == null)
        {
            SetMarker(null);
        }
        else if (PackageDestination != null)
        {
            foreach (GameObject planet in planets)
            {
                if (planet.name == PackageDestination)
                {
                    SetMarker(planet.transform);
                }
            }
        }

    }

    private void DeliverdPackage()
    {
        destinationMarker.transform.position = packagePickupLocation.transform.position;
        DeliveredPackage = false;
    }
}
