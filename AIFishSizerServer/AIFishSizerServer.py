import socket
import struct
import json
import base64
from WeightFish_load import PerchWeightPredictor
from AI_CNN_vgg16_FIsh import FishClassifier
import matplotlib.pyplot as plt
import numpy as np
from PIL import Image
from io import BytesIO

def handle_client(client_socket):
    try:
        # JSON 데이터 길이를 수신
        length_bytes = client_socket.recv(4)
        if len(length_bytes) < 4:
            print("Failed to receive JSON data length.")
            return
        json_length = struct.unpack('!I', length_bytes)[0]

        # JSON 데이터 수신
        json_data = b''
        while len(json_data) < json_length:
            packet = client_socket.recv(json_length - len(json_data))
            if not packet:
                break
            json_data += packet

        if len(json_data) != json_length:
            print("Failed to receive complete JSON data.")
            return

        # JSON 데이터를 디코딩하여 RequestData 객체로 변환
        json_request = json_data.decode('utf-8')
        request_data = json.loads(json_request)

        # 이미지 데이터와 정수 데이터 추출
        image_base64 = request_data['ImageBytes']
        length = request_data['Length']

        # base64로 인코딩된 이미지 데이터를 디코딩
        image_bytes = base64.b64decode(image_base64)

        # 이미지를 파일로 저장
        image = Image.open(BytesIO(image_bytes))
        if image.mode == 'RGBA':
            image = image.convert('RGB')
        image_path = 'received_image.jpg'
        image.save(image_path)

        # 이미지를 plt로 띄우기
        img = Image.open(image_path)
        plt.imshow(img)
        plt.axis('off')  # 축 숨기기
        plt.show()
        
        model_path = "vgg16_1.keras"       
        classifier = FishClassifier(model_path, image_path)
        fishName = classifier.run()
        print(f"물고기 종류 = {fishName}")
        
        # 여기에 이미지 분류 및 처리 코드를 추가합니다
        # 예를 들어, 이미지 데이터를 사용하여 무언가를 처리하고 결과를 result 변수에 저장합니다.
        result = "dummy_classification_result"  # 이 부분을 실제 처리 결과로 대체

        # 추가 텍스트 데이터
        additional_info = "This is some additional information from the server."

        # 어군 길이를 사용하여 무게 예측
        predictor = PerchWeightPredictor('linear_regression_model.pkl')    
        weight = predictor.run(length)

        print(f"Fish length {length}: Predicted weight {weight}")
        
        subtitle = "회쳐서 먹을만하다."
        size = "대박"
        if weight < 1000 :
            subtitle = "잡기에도 미안하다"
            size = "방생"

        # 분류 결과와 추가 텍스트 데이터를 JSON 형식으로 클라이언트로 전송
        response_data = {
            "Title": fishName,
            "subTitle": subtitle,
            "bottomName": fishName,
            "bottomSize": size,
            "bottomW": "20",
            "bottomH": length,
            "bottomWeight": str(weight) + "g"
        }
        response_json = json.dumps(response_data)
        client_socket.sendall(response_json.encode('utf-8'))

    finally:
        client_socket.close()

def main():
    server_address = '127.0.0.1'
    server_port = 12345

    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((server_address, server_port))
    server_socket.listen(5)
    print(f"Server listening on {server_address}:{server_port}")

    try:
        while True:
            client_socket, addr = server_socket.accept()
            print(f"Accepted connection from {addr}")
            handle_client(client_socket)
    finally:
        server_socket.close()

if __name__ == "__main__":
    main()