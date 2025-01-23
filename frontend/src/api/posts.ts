import axios from "axios";

const API_URL = import.meta.env.VITE_POSTS_API || "http://localhost:5003/api/posts";

export const fetchPosts = async () => {
    try {
        const response = await axios.get(API_URL);
        return response.data;
    } catch (error) {
        console.error("Błąd podczas pobierania postów:", error);
        throw new Error("Nie udało się pobrać postów");
    }
};

export const createPost = async (title: string, content: string) => {
    try {
        const response = await axios.post(API_URL, { title, content, createdAt: new Date().toISOString() });
        return response.data;
    } catch (error) {
        console.error("Błąd podczas tworzenia posta:", error);
        throw new Error("Nie udało się dodać posta");
    }
};
