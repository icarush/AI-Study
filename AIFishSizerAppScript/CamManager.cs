using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using UnityEngine.Android;
using TMPro;

public class CamManager : MonoBehaviour
{
    WebCamTexture camTexture;
    public RawImage cameraViewImage; // 카메라 뷰를 표시할 RawImage UI 요소
    public RawImage savedImageViewChecker; // 저장된 이미지를 표시할 RawImage UI 요소
    public RawImage savedImageViewResult; // 저장된 이미지를 표시할 RawImage UI 요소
    public string fileName = "capturedImage.png"; // 저장할 파일 이름
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
        // 이미지 불러오기
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
        // 권한 체크
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            return;
        }

        if (WebCamTexture.devices.Length == 0) // 사용 가능한 카메라가 없을 경우
        {
            Debug.Log("No camera!");
            return;
        }

        WebCamDevice[] devices = WebCamTexture.devices; // 사용 가능한 카메라 장치를 배열로 저장
        int selectedCameraIndex = -1;

        // 후면 카메라 선택
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                selectedCameraIndex = i;
                break;
            }
        }

        // 카메라 ON
        if (selectedCameraIndex >= 0)
        {
            // 해당 인덱스의 카메라 장치 사용
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);
            camTexture.requestedFPS = 30; // 프레임 설정
            cameraViewImage.texture = camTexture; // WebCamTexture를 Raw Image의 Texture에 할당
            camTexture.Play(); // 카메라 실행
        }
    }

    public void CameraOFF()
    {
        if (camTexture != null)
        {
            camTexture.Stop(); // 카메라 중지
            WebCamTexture.Destroy(camTexture); // 카메라 텍스처 제거
            camTexture = null; // 변수 초기화
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

            // 이미지 데이터를 PNG로 변환
            byte[] bytes = photo.EncodeToPNG();
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            // 파일로 저장
            File.WriteAllBytes(filePath, bytes);
            Debug.Log("Saved image to " + filePath);

            // 저장된 이미지를 UI에 표시
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

//    public RawImage cameraViewImage; // 카메라에 해당하는 RawImage UI 요소

//    public void CameraON() // 카메라 ON
//    {
//        // 권한 체크
//        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
//        {
//            Permission.RequestUserPermission(Permission.Camera);
//            return;
//        }

//        if (WebCamTexture.devices.Length == 0) // 사용 가능한 카메라가 없을 경우
//        {
//            Debug.Log("No camera!");
//            return;
//        }

//        WebCamDevice[] devices = WebCamTexture.devices; // 사용 가능한 카메라 장치를 배열로 저장
//        int selectedCameraIndex = -1;

//        // 전면 카메라 선택
//        for (int i = 0; i < devices.Length; i++)
//        {
//            if (devices[i].isFrontFacing == false)
//            {
//                selectedCameraIndex = i;
//                break;
//            }
//        }

//        // 카메라 ON
//        if (selectedCameraIndex >= 0)
//        {
//            // 해당 인덱스의 카메라 장치 사용
//            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);

//            camTexture.requestedFPS = 30; // 프레임 설정

//            cameraViewImage.texture = camTexture; // WebCamTexture를 Raw Image의 Texture에 할당

//            camTexture.Play(); // 카메라 실행
//        }
//    }

//    public void CameraOFF() // 카메라 OFF
//    {
//        if (camTexture != null)
//        {
//            camTexture.Stop(); // 카메라 중지
//            WebCamTexture.Destroy(camTexture); // 카메라 텍스처 제거
//            camTexture = null; // 변수 초기화
//        }
//    }
//}


