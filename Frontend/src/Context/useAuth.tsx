import {
  createContext,
  useState,
  useEffect,
  useContext,
  ReactNode,
} from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { toast } from "react-toastify";
import { loginAPI, registerAPI } from "../Services/AuthService";
import { UserProfile } from "../Models/User";

type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  registerUser: (email: string, username: string, password: string) => void;
  loginUser: (username: string, password: string) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
};

const UserContext = createContext<UserContextType | undefined>(undefined);

type Props = {
  children: ReactNode;
};

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserProfile | null>(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const storedToken = localStorage.getItem("token");
    const storedUser = localStorage.getItem("user");

    if (storedToken && storedUser) {
      setToken(storedToken);
      setUser(JSON.parse(storedUser));
      axios.defaults.headers.common["Authorization"] = "Bearer " + storedToken;
    }

    setIsReady(true);
  }, []);

  const registerUser = async (
    email: string,
    username: string,
    password: string
  ) => {
    try {
      const res = await registerAPI(email, username, password);
      if (res?.data) {
        // Poprawiona destrukturyzacja z mapowaniem wielkich liter na małe
        const {
          Token: token,
          UserName: userName,
          Email: emailFromResponse,
        } = res.data;

        const userObj: UserProfile = { userName, email: emailFromResponse };

        localStorage.setItem("token", token);
        localStorage.setItem("user", JSON.stringify(userObj));
        axios.defaults.headers.common["Authorization"] = "Bearer " + token;

        setToken(token);
        setUser(userObj);
        toast.success("Registration successful!");
        navigate("/offers/page/0");
      }
    } catch (error) {
      toast.error("Registration failed");
    }
  };

  const loginUser = async (username: string, password: string) => {
    try {
      const res = await loginAPI(username, password);
      if (res?.data) {
        // Poprawiona destrukturyzacja z mapowaniem wielkich liter na małe
        const {
          Token: token,
          UserName: userName,
          Email: emailFromResponse,
        } = res.data;

        const userObj: UserProfile = { userName, email: emailFromResponse };

        localStorage.setItem("token", token);
        localStorage.setItem("user", JSON.stringify(userObj));
        axios.defaults.headers.common["Authorization"] = "Bearer " + token;

        setToken(token);
        setUser(userObj);
        toast.success("Login successful!");
        navigate("/offers/page/0");
      }
    } catch (error) {
      toast.error("Login failed");
    }
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken(null);
    delete axios.defaults.headers.common["Authorization"];
    navigate("/");
  };

  const isLoggedIn = () => !!user;

  return (
    <UserContext.Provider
      value={{ user, token, registerUser, loginUser, logout, isLoggedIn }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(UserContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within a UserProvider");
  }
  return context;
};
