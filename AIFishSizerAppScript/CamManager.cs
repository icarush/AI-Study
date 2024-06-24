using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using UnityEngine.Android;
using TMPro;

public class CamManager : MonoBehaviour
{
    WebCamTexture camTexture;
    public RawImage cameraViewImage; // ī�޶� �並 ǥ���� RawImage UI ���
    public RawImage savedImageViewChecker; // ����� �̹����� ǥ���� RawImage UI ���
    public RawImage savedImageViewResult; // ����� �̹����� ǥ���� RawImage UI ���
    public string fileName = "capturedImage.png"; // ������ ���� �̸�
    public GameObject sceneStart;
    public GameObject sceneCamTaker;
    public GameObject sceneCamChecker;
    public GameObject sceneCamReulst;

    public TMP_Text Title;
    public TMP_Text subTitle;
    public TMP_Text bottomName;
    public TMP_Text bottomSize;
    public TMP_Text bottomW;
    public TMP_Text bottomH;
    public TMP_Text bottomWeight;
    void Start()
    {

    }

    public void click_Start()
    {
        CameraON();
        sceneCamTaker.SetActive(true);
        sceneStart.SetActive(false);
    }

    public void click_camTake()
    {
        CaptureAndSaveImage();
        CameraOFF();
        sceneCamChecker.SetActive(true);
        sceneCamTaker.SetActive(false);
        // �̹��� �ҷ�����
    }

    public void click_NextResult(ResponseData responseData)
    {
        sceneCamReulst.SetActive(true);
        sceneCamChecker.SetActive(false);

        Title.text = responseData.Title;
        subTitle.text = responseData.subTitle;
        bottomName.text = responseData.bottomName;
        bottomSize.text = responseData.bottomSize;
        bottomW.text = responseData.bottomW;
        bottomH.text = responseData.bottomH;
        bottomWeight.text = responseData.bottomWeight;       
    }

    public void click_AgainTake()
    {
        CameraON();
        sceneCamTaker.SetActive(true);
        sceneCamChecker.SetActive(false);
    }

    public void CameraON()
    {
        // ���� üũ
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            return;
        }

        if (WebCamTexture.devices.Length == 0) // ��� ������ ī�޶� ���� ���
        {
            Debug.Log("No camera!");
            return;
        }

        WebCamDevice[] devices = WebCamTexture.devices; // ��� ������ ī�޶� ��ġ�� �迭�� ����
        int selectedCameraIndex = -1;

        // �ĸ� ī�޶� ����
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                selectedCameraIndex = i;
                break;
            }
        }

        // ī�޶� ON
        if (selectedCameraIndex >= 0)
        {
            // �ش� �ε����� ī�޶� ��ġ ���
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);
            camTexture.requestedFPS = 30; // ������ ����
            cameraViewImage.texture = camTexture; // WebCamTexture�� Raw Image�� Texture�� �Ҵ�
            camTexture.Play(); // ī�޶� ����
        }
    }

    public void CameraOFF()
    {
        if (camTexture != null)
        {
            camTexture.Stop(); // ī�޶� ����
            WebCamTexture.Destroy(camTexture); // ī�޶� �ؽ�ó ����
            camTexture = null; // ���� �ʱ�ȭ
        }

        sceneCamTaker.SetActive(false);
    }

    public void CaptureAndSaveImage()
    {
        if (camTexture != null)
        {
            Texture2D photo = new Texture2D(camTexture.width, camTexture.height);
            photo.SetPixels(camTexture.GetPixels());
            photo.Apply();

            // �̹��� �����͸� PNG�� ��ȯ
            byte[] bytes = photo.EncodeToPNG();
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            // ���Ϸ� ����
            File.WriteAllBytes(filePath, bytes);
            Debug.Log("Saved image to " + filePath);

            // ����� �̹����� UI�� ǥ��
            StartCoroutine(LoadAndDisplayImage(filePath));
        }
    }

    private IEnumerator LoadAndDisplayImage(string filePath)
    {
        if (File.Exists(filePath))
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            savedImageViewChecker.texture = texture;
            savedImageViewResult.texture = texture;
            yield return null;
        }
        else
        {
            Debug.Log("File not found at " + filePath);
        }
    }
}


//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Android;

//public class CamManager : MonoBehaviour
//{
//    WebCamTexture camTexture;

//    public RawImage cameraViewImage; // ī�޶� �ش��ϴ� RawImage UI ���

//    public void CameraON() // ī�޶� ON
//    {
//        // ���� üũ
//        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
//        {
//            Permission.RequestUserPermission(Permission.Camera);
//            return;
//        }

//        if (WebCamTexture.devices.Length == 0) // ��� ������ ī�޶� ���� ���
//        {
//            Debug.Log("No camera!");
//            return;
//        }

//        WebCamDevice[] devices = WebCamTexture.devices; // ��� ������ ī�޶� ��ġ�� �迭�� ����
//        int selectedCameraIndex = -1;

//        // ���� ī�޶� ����
//        for (int i = 0; i < devices.Length; i++)
//        {
//            if (devices[i].isFrontFacing == false)
//            {
//                selectedCameraIndex = i;
//                break;
//            }
//        }

//        // ī�޶� ON
//        if (selectedCameraIndex >= 0)
//        {
//            // �ش� �ε����� ī�޶� ��ġ ���
//            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);

//            camTexture.requestedFPS = 30; // ������ ����

//            cameraViewImage.texture = camTexture; // WebCamTexture�� Raw Image�� Texture�� �Ҵ�

//            camTexture.Play(); // ī�޶� ����
//        }
//    }

//    public void CameraOFF() // ī�޶� OFF
//    {
//        if (camTexture != null)
//        {
//            camTexture.Stop(); // ī�޶� ����
//            WebCamTexture.Destroy(camTexture); // ī�޶� �ؽ�ó ����
//            camTexture = null; // ���� �ʱ�ȭ
//        }
//    }
//}


