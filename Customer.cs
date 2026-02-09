using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public enum PotionRequestType { Single, Double }
    public PotionRequestType Request_Type = PotionRequestType.Single;

    public string Requested_Potion1;
    public string Requested_Potion2;


    private bool Received_Potion1 = false;
    private bool Received_Potion2 = false;
    private bool Has_Received_All = false;
    private bool Is_Front = false;

    public float Patience_Time = 10f;
    public float Timer = 0f;
   

    [SerializeField] private Customer_Line_Advance _Advance;
    [SerializeField] private Transform Request_Display_Parent;
    [SerializeField] private GameObject Speech_Bubble;
    [SerializeField] private GameObject Love_Potion;
    [SerializeField] private GameObject Health_Potion;
    [SerializeField] private GameObject Growth_Potion;
    [SerializeField] private GameObject Poison_Potion;

    [SerializeField] private Slider PatienceSlider;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip thanks;
    [SerializeField] private AudioClip no;

    private void Awake()
    {
        PatienceSlider = GetComponentInChildren<Slider>();
        if (PatienceSlider == null)
        {
            Debug.LogWarning("PatienceSlider not found in prefab!");
        }
    }

    public void SetLineManager(Customer_Line_Advance advance)
    {
        _Advance = advance;
    }

    public void SetAsFrontCustomer(bool value)
    {
        Is_Front = value;
        if (Is_Front)
        {
            Timer = Patience_Time;
            Speech_Bubble.SetActive(true);
            PatienceSlider.gameObject.SetActive(true);
            ShowPotionRequests();
        }

        else
        {
            Speech_Bubble.SetActive(false);
            PatienceSlider.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (!Is_Front)
        {
            return;
        }

        if (Timer > 0f)
        {
            Timer -= Time.deltaTime;

            if (PatienceSlider != null)
            {
                PatienceSlider.value = Timer;
            }
        }
      

        if (Timer <= 0f)
        {
            Debug.Log("Customer: I'm tired of waiting!");
            dayfailure.Instance.CustomerFailed();
            _Advance.ServeFrontCustomer();
        }
    }
    private void ShowPotionRequests()
    {
        if (Request_Display_Parent == null) return;

        GameObject icon1 = GetIconForPotion(Requested_Potion1);
        if (icon1 != null)
        {
            Instantiate(icon1, Request_Display_Parent.position + Vector3.left * 0.2f, icon1.transform.rotation, Request_Display_Parent);
        }

        if (Request_Type == PotionRequestType.Double)
        {
            GameObject icon2 = GetIconForPotion(Requested_Potion2);
            if (icon2 != null)
            {
                Instantiate(icon2, Request_Display_Parent.position + Vector3.right * 0.2f, icon2.transform.rotation, Request_Display_Parent);
            }
        }
    }

    private GameObject GetIconForPotion(string potionTag)
    {
        switch (potionTag)
        {
            case "Health potion": return Health_Potion;
            case "Mana potion": return Love_Potion;
            case "Growth potion": return Growth_Potion;
            case "Poison potion": return Poison_Potion;
            default: return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hand"))
        {
            return;
        }

        if (Has_Received_All)
        {
            Debug.Log("Customer: I've already got what I need.");
            return;
        }

        bool potionMatched = false;

        if (Request_Type == PotionRequestType.Single)
        {
            if (other.CompareTag(Requested_Potion1))
            {
                potionMatched = true;
                Has_Received_All = true;
                Debug.Log("Customer: That's just what I needed!");
                audioSource.clip = thanks;
                audioSource.Play();
                _Advance.ServeFrontCustomer();

            }
        }
        else if (Request_Type == PotionRequestType.Double)
        {
            if (!Received_Potion1 && other.CompareTag(Requested_Potion1))
            {
                Received_Potion1 = true;
                potionMatched = true;
                audioSource.clip = thanks;
                audioSource.Play();
                Debug.Log("Customer: Thanks! One down.");
            }
            else if (!Received_Potion2 && other.CompareTag(Requested_Potion2))
            {
                Received_Potion2 = true;
                potionMatched = true;
                audioSource.clip = thanks;
                audioSource.Play();
                Debug.Log("Customer: Great! Thatfs the second one.");
            }

            if (Received_Potion1 && Received_Potion2 && !Has_Received_All)
            {
                Has_Received_All = true;
                Debug.Log("Customer: Both potions done!");                
                _Advance.ServeFrontCustomer();
            }
        }

        if (potionMatched)
        {
            Destroy(other.gameObject);
        }
        else
        {
            audioSource.clip = no;
            audioSource.Play();
            Debug.Log("Customer: That's not the potion I asked for.");
        }
    }

   
} 
    
        
    

