import joblib
import numpy as np

class PerchWeightPredictor:
    def __init__(self, model_path):
        self.model = self.load_model(model_path)
        print("Model loaded successfully")

    def load_model(self, model_path):
        return joblib.load(model_path)

    def predict_weight(self, length):
        input_features = np.array([[length**2, length]])
        predicted_weight = self.model.predict(input_features)
        return predicted_weight[0]

    def run(self, length):
        return self.predict_weight(length)

if __name__ == "__main__":
    predictor = PerchWeightPredictor('linear_regression_model.pkl')    
    length = 30
    weight = predictor.run(length)
    
    print(f"Predicted weight for length {length} cm: {weight:.2f} g")
