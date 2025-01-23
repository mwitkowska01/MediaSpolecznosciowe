import "./styles.css";
import { useState, useEffect } from "react";
import AuthForm from "./components/AuthForm";
import { fetchPosts, createPost } from "./api/posts";
import { fetchPeople, createPerson } from "./api/people";

const App = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem("token"));
  const [posts, setPosts] = useState([]);
  const [people, setPeople] = useState([]);
  const [newPostTitle, setNewPostTitle] = useState("");
  const [newPostContent, setNewPostContent] = useState("");
  const [newPersonName, setNewPersonName] = useState("");
  const [newPersonEmail, setNewPersonEmail] = useState("");

  useEffect(() => {
    if (isAuthenticated) {
      fetchPosts().then(setPosts).catch(console.error);
      fetchPeople().then(setPeople).catch(console.error);
    }
  }, [isAuthenticated]);

  const handleAuthSuccess = () => {
    setIsAuthenticated(true);
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    setIsAuthenticated(false);
  };

  const handleAddPost = async () => {
    if (!newPostTitle || !newPostContent) return;
    const post = await createPost(newPostTitle, newPostContent);
    setPosts([...posts, post]);
    setNewPostTitle("");
    setNewPostContent("");
  };

  const handleAddPerson = async () => {
    if (!newPersonName || !newPersonEmail) return;
    const person = await createPerson(newPersonName, newPersonEmail);
    setPeople([...people, person]);
    setNewPersonName("");
    setNewPersonEmail("");
  };

  return (
    <div className="app-container">
      {!isAuthenticated ? (
        <AuthForm onAuthSuccess={handleAuthSuccess} />
      ) : (
        <>
          <button onClick={handleLogout} className="logout-btn">Wyloguj</button>

          <h1>Lista postów</h1>
          <ul>
            {posts.map((post) => (
              <li key={post.id}>
                <strong>{post.title}</strong>: {post.content}
              </li>
            ))}
          </ul>
          <input type="text" placeholder="Tytuł posta" value={newPostTitle} onChange={(e) => setNewPostTitle(e.target.value)} />
          <input type="text" placeholder="Treść posta" value={newPostContent} onChange={(e) => setNewPostContent(e.target.value)} />
          <button onClick={handleAddPost}>Dodaj post</button>

          <h1>Lista użytkowników</h1>
          <ul>
            {people.map((person) => (
              <li key={person.id}>
                <strong>{person.name}</strong>: {person.email}
              </li>
            ))}
          </ul>
          <input type="text" placeholder="Imię użytkownika" value={newPersonName} onChange={(e) => setNewPersonName(e.target.value)} />
          <input type="text" placeholder="Email użytkownika" value={newPersonEmail} onChange={(e) => setNewPersonEmail(e.target.value)} />
          <button onClick={handleAddPerson}>Dodaj użytkownika</button>
        </>
      )}
    </div>
  );
};

// 🚀 Usuń ten zbędny średnik `;` przed exportem!
export default App;

