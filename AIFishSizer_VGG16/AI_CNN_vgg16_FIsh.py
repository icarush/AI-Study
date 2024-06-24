import tensorflow as tf
from tensorflow.keras.models import load_model
from tensorflow.keras.preprocessing import image
import numpy as np
import matplotlib.pyplot as plt

class FishClassifier:
    def __init__(self, model_path, img_path):
        self.model_path = model_path
        self.img_path = img_path
        self.model = None
        self.img = None

    def configure_gpu(self):
        gpus = tf.config.list_physical_devices('GPU')
        if gpus:
            try:
                for gpu in gpus:
                    tf.config.experimental.set_memory_growth(gpu, True)
            except RuntimeError as e:
                print(e)

    def load_model(self):
        self.model = load_model(self.model_path)

    def load_image(self):
        self.img = image.load_img(self.img_path, target_size=(224, 224))
        self.img = np.asarray(self.img)
        plt.imshow(self.img)
        plt.axis('off')  # Hide axes for better display
        plt.show()
        self.img = np.expand_dims(self.img, axis=0)

    def predict(self):
        if self.model is None or self.img is None:
            print("Model or image not loaded.")
            return None
        output = self.model.predict(self.img)
        if output[0][0] > output[0][1]:
            return "광어"
        else:
            return '줄돔'

    def run(self):
        self.configure_gpu()
        self.load_model()
        self.load_image()
        return self.predict()

if __name__ == "__main__":
    model_path = "vgg16_1.keras"
    img_path = "j1.jpg"
    classifier = FishClassifier(model_path, img_path)
    prediction = classifier.run()
    print(f"Prediction: {prediction}")