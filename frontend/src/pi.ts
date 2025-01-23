import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL;

export const fetchPosts = async () => {
    const response = await axios.get(`${API_URL}/posts`);
    return response.data;
};

export const createPost = async (post: { title: string; content: string }) => {
    const response = await axios.post(`${API_URL}/posts`, post);
    return response.data;
};
