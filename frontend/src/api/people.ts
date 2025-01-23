import axios from "axios";

const API_URL = import.meta.env.VITE_PEOPLE_API || "http://localhost:5001/api/people";

export const fetchPeople = async () => {
    try {
        const response = await axios.get(API_URL);
        return response.data;
    } catch (error) {
        console.error("Błąd podczas pobierania użytkowników:", error);
        throw new Error("Nie udało się pobrać użytkowników");
    }
};

export const createPerson = async (name: string, email: string) => {
    try {
        const response = await axios.post(API_URL, { name, email });
        return response.data;
    } catch (error) {
        console.error("Błąd podczas dodawania użytkownika:", error);
        throw new Error("Nie udało się dodać użytkownika");
    }
};
