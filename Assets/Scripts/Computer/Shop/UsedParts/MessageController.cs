using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageController : MonoBehaviour
{
    [SerializeField] private GameObject ShopPanels;
    [SerializeField] private string[] partDescription;
    [SerializeField] private string[] avatarNames;
    [SerializeField] private string[] myHelloMessage;
    [SerializeField] private string[] heHelloMessage;
    [SerializeField] private string[] sellMessage;
    [SerializeField] private Sprite[] AvatarImages;

    [Header("PanelParts")]
    [SerializeField] private TextMeshProUGUI avatarNameText;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private Image Avatar;
    [SerializeField] private Image PartImage;

    [Header("Dialogue")]
    [SerializeField] private TMP_InputField PriceInputField;
    [SerializeField] private Button SendBTN;
    [SerializeField] private TextMeshProUGUI firstPriceText;
    [SerializeField] private TextMeshProUGUI SecondPriceText;
    [SerializeField] private TextMeshProUGUI firstAnswerText;
    [SerializeField] private TextMeshProUGUI SecondAnswerText;
    [SerializeField] private TextMeshProUGUI myHelloMessageText;
    [SerializeField] private TextMeshProUGUI heHelloMessageText;
    [SerializeField] private GameObject PriceError;
    
    private int price;
    private int PostId;
    private int countMessage = 0;
    private Slot slot;
    private int firstPrice;

    private GameObject SenderPanel;

    BuyUsedPartsSystem BuyUsedPartsSystem;

    private void Start()
    {
        BuyUsedPartsSystem = GetComponent<BuyUsedPartsSystem>();
    }

    public void ChangeSenderPanel(GameObject Sender)
    {
        SenderPanel = Sender;
    }

    public void CloseMessagePanel()
    {
        ShopPanels.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ShowMessagePanel(int slotId)
    {
        gameObject.SetActive(true);
        ShopPanels.SetActive(false);

        countMessage = 0;
        firstAnswerText.gameObject.SetActive(false);
        firstPriceText.gameObject.SetActive(false);
        SecondAnswerText.gameObject.SetActive(false);
        SecondPriceText.gameObject.SetActive(false);
        PriceInputField.text = "";
        PriceInputField.interactable = true;
        SendBTN.interactable = true;

        myHelloMessageText.text = myHelloMessage[Random.Range(0,myHelloMessage.Length)];
        heHelloMessageText.text = heHelloMessage[Random.Range(0,heHelloMessage.Length)];

        slot = SenderPanel.GetComponent<UsedPartsController>().Slots[slotId];

        avatarNameText.text = avatarNames[Random.Range(0, avatarNames.Length)];
        NameText.text = slot.partName;
        PriceText.text = slot.partPrice.ToString() + " ������";
        DescriptionText.text = partDescription[Random.Range(0, partDescription.Length)];
        Avatar.sprite = AvatarImages[Random.Range(0,AvatarImages.Length)];
        PartImage.sprite = slot.PartImage;

        price = slot.partPrice;
        PostId = slotId;
    }

    

    public void AddPrice()
    {
        int inputPrice = int.Parse(PriceInputField.text);
        int minPrice = slot.partPrice - (slot.partPrice * 30 / 100);

        if (countMessage == 0)
        {
            firstPrice = inputPrice;

            if (inputPrice >= slot.partPrice)
            {
                firstPriceText.text = inputPrice.ToString();
                firstPriceText.gameObject.SetActive(true);
                firstAnswerText.gameObject.SetActive(true);
                firstAnswerText.text = sellMessage[Random.Range(0, sellMessage.Length)];
                BuyUsedPartsSystem.BuyPart(PostId,SenderPanel, inputPrice);
                PriceInputField.interactable = false;
                SendBTN.interactable = false;
                return;
            }
            else if (inputPrice <= minPrice)
            {
                StartCoroutine(ShowErrorPrice());
                return;
            }

            firstPriceText.text = inputPrice.ToString();
            firstPriceText.gameObject.SetActive(true);
            firstAnswerText.gameObject.SetActive(true);

            if (Random.Range(0, 2) == 0)
            {
                firstAnswerText.text = "����";
                countMessage++;
            }
            else
            {
                firstAnswerText.text = sellMessage[Random.Range(0, sellMessage.Length)];
                BuyUsedPartsSystem.BuyPart(PostId, SenderPanel, inputPrice);
                PriceInputField.interactable = false;
                SendBTN.interactable = false;
            }
        }
        else
        {
            if (inputPrice <= minPrice)
            {
                StartCoroutine(ShowErrorPrice());
                return;
            }

            SecondPriceText.text = inputPrice.ToString();
            SecondPriceText.gameObject.SetActive(true);
            SecondAnswerText.gameObject.SetActive(true);

            if (Random.Range(0, 2) == 0 || firstPrice >= inputPrice)
            {
                SecondAnswerText.text = "���, ����������";
                countMessage++;
                PriceInputField.interactable = false;
                SendBTN.interactable = false;
            }
            else
            {
                SecondAnswerText.text = sellMessage[Random.Range(0,sellMessage.Length)];
                BuyUsedPartsSystem.BuyPart(PostId, SenderPanel, inputPrice);
                PriceInputField.interactable = false;
                SendBTN.interactable = false;
            }

        }
    }

    IEnumerator ShowErrorPrice()
    {
        PriceError.SetActive(true);
        yield return new WaitForSeconds(1f);
        PriceError.SetActive(false);
    }
}
