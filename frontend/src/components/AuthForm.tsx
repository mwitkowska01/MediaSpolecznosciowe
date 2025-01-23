import { useState } from "react";
import { registerUser, loginUser } from "../api/auth";

interface AuthFormProps {
  onAuthSuccess: () => void;
}

const AuthForm = ({ onAuthSuccess }: AuthFormProps) => {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [isLogin, setIsLogin] = useState(true);
  const [error, setError] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    try {
      if (isLogin) {
        await loginUser(userName, password);
      } else {
        await registerUser(userName, password);
      }
      onAuthSuccess(); // Po sukcesie przechodzimy do listy
    } catch (err) {
      setError("Błąd podczas logowania/rejestracji.");
    }
  };

  return (
    <div>
      <h2>{isLogin ? "Logowanie" : "Rejestracja"}</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Nazwa użytkownika"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
        <input
          type="password"
          placeholder="Hasło"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit">{isLogin ? "Zaloguj" : "Zarejestruj"}</button>
      </form>
      <button onClick={() => setIsLogin(!isLogin)}>
        {isLogin ? "Nie masz konta? Zarejestruj się" : "Masz już konto? Zaloguj się"}
      </button>
    </div>
  );
};

export default AuthForm;

