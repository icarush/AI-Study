using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting; // JSON ��ȯ�� ���� ���̺귯�� (Json.NET)
using TMPro;

public class ImageSenderSocket : MonoBehaviour
{
    public string serverAddress = "127.0.0.1"; // ���� �ּ�
    public int serverPort = 12345; // ���� ��Ʈ
    public CamManager Mng; // �̹��� ��� (StreamingAssets ������ �̹����� �ְ� ��θ� ����)
    public int inputLen;

    public void SendImageAndReceiveData()
    {
        try
        {
            // �̹��� ������ �о byte �迭�� ��ȯ
            string filePath = Path.Combine(Application.persistentDataPath, Mng.fileName);
            byte[] imageBytes = File.ReadAllBytes(filePath);

            // �̹����� ���� �����͸� �ϳ��� JSON ��ü�� ���
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
                // JSON ������ ���̸� ��Ʈ��ũ ����Ʈ ������ ��ȯ�Ͽ� ����
                byte[] lengthBytes = BitConverter.GetBytes(jsonRequestBytes.Length);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(lengthBytes);
                }
                stream.Write(lengthBytes, 0, lengthBytes.Length);

                // ���� JSON ������ ����
                stream.Write(jsonRequestBytes, 0, jsonRequestBytes.Length);

                // �����κ��� �з� ����� �߰� �ؽ�Ʈ �����͸� ����
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string jsonResponse = reader.ReadToEnd(); // �����κ��� ��� �����͸� ����
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
    public byte[] ImageBytes { get; set; } // �̹��� ������
    public int Length { get; set; } // ���� ������ �߰�
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