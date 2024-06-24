using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting; // JSON 변환을 위한 라이브러리 (Json.NET)
using TMPro;

public class ImageSenderSocket : MonoBehaviour
{
    public string serverAddress = "127.0.0.1"; // 서버 주소
    public int serverPort = 12345; // 서버 포트
    public CamManager Mng; // 이미지 경로 (StreamingAssets 폴더에 이미지를 넣고 경로를 설정)
    public int inputLen;

    public void SendImageAndReceiveData()
    {
        try
        {
            // 이미지 파일을 읽어서 byte 배열로 변환
            string filePath = Path.Combine(Application.persistentDataPath, Mng.fileName);
            byte[] imageBytes = File.ReadAllBytes(filePath);

            // 이미지와 정수 데이터를 하나의 JSON 객체에 담기
            RequestData requestData = new RequestData
            {
                ImageBytes = imageBytes,
                Length = inputLen
            };
            string jsonRequest = JsonConvert.SerializeObject(requestData);
            byte[] jsonRequestBytes = Encoding.UTF8.GetBytes(jsonRequest);

            using (TcpClient client = new TcpClient(serverAddress, serverPort))
            using (NetworkStream stream = client.GetStream())
            {
                // JSON 데이터 길이를 네트워크 바이트 순서로 변환하여 전송
                byte[] lengthBytes = BitConverter.GetBytes(jsonRequestBytes.Length);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(lengthBytes);
                }
                stream.Write(lengthBytes, 0, lengthBytes.Length);

                // 실제 JSON 데이터 전송
                stream.Write(jsonRequestBytes, 0, jsonRequestBytes.Length);

                // 서버로부터 분류 결과와 추가 텍스트 데이터를 수신
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string jsonResponse = reader.ReadToEnd(); // 서버로부터 모든 데이터를 읽음
                    ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(jsonResponse);
                    Debug.Log($"Received response: {responseData.Title}, {responseData.bottomSize}");
                    Mng.click_NextResult(responseData);
                }
            }

            Debug.Log("Image sent and data received successfully!");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
        }
    }
}

[Serializable]
public class RequestData
{
    public byte[] ImageBytes { get; set; } // 이미지 데이터
    public int Length { get; set; } // 정수 데이터 추가
}

[Serializable]
public class ResponseData
{
    public string Title { get; set; }
    public string subTitle { get; set; }
    public string bottomName { get; set; }
    public string bottomSize { get; set; }
    public string bottomW { get; set; }
    public string bottomH { get; set; }
    public string bottomWeight { get; set; }
}