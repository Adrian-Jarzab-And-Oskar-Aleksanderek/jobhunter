import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App";
import "./css/bootstrap.min.css";
import "./css/global.css";
import { BrowserRouter } from "react-router-dom";
import { UserProvider } from "./Context/useAuth";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <UserProvider>
        <App />
      </UserProvider>
    </BrowserRouter>
  </StrictMode>
);
