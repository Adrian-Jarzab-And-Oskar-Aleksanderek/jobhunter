import { useEffect } from "react";
import { useAuth } from "../Context/useAuth";

const LogoutPage = () => {
  const { logout } = useAuth();

  useEffect(() => {
    logout();
  }, [logout]);

  return <div>Logging out...</div>;
};

export default LogoutPage;
