import axios from "axios";

const API_URL = import.meta.env.VITE_IDENTITY_API || "http://localhost:5002/api/identity";

export const registerUser = async (userName: string, password: string) => {
    try {
        const response = await axios.post(`${API_URL}/register`, { userName, password });
        return response.data;
    } catch (error) {
        throw new Error("Błąd podczas rejestracji");
    }
};

export const loginUser = async (userName: string, password: string) => {
    try {
        const response = await axios.post(`${API_URL}/login`, { userName, password });
        return response.data; // Powinno zwracać token JWT
    } catch (error) {
        throw new Error("Błąd podczas logowania");
    }
};
