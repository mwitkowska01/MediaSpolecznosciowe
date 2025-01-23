import axios from "axios";

const API_URL_POSTS = import.meta.env.VITE_API_URL_POSTS || "http://localhost:5003/api/posts";
const API_URL_USERS = import.meta.env.VITE_API_URL_USERS || "http://localhost:5001/api/people";

// Pobieranie postów
export const fetchPosts = async () => {
  const response = await axios.get(API_URL_POSTS);
  return response.data;
};

// Dodawanie postów
export const createPost = async (post) => {
  const response = await axios.post(API_URL_POSTS, post);
  return response.data;
};

// Pobieranie użytkowników
export const fetchUsers = async () => {
  const response = await axios.get(API_URL_USERS);
  return response.data;
};

// Dodawanie użytkowników
export const createUser = async (user) => {
  const response = await axios.post(API_URL_USERS, user);
  return response.data;
};
