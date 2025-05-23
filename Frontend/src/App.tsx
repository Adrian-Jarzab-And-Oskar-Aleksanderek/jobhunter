import { Navigate, Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage.tsx";
import AboutPage from "./pages/AboutPage.tsx";
import ContactPage from "./pages/ContactPage.tsx";
import NoPage from "./pages/NoPage.tsx";
import DetailsPage from "./pages/DetailsPage.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import RegisterPage from "./pages/RegisterPage.tsx";
import LogoutPage from "./pages/LogoutPage.tsx";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function App() {
  return (
    <>
      <ToastContainer />
      <Routes>
        <Route path="/" element={<Navigate to="/offers/page/0" replace />} />
        <Route path="/offers/page/:pageNumber" element={<HomePage />} />
        <Route path="/offers/:id" element={<DetailsPage />} />
        <Route path="/about" element={<AboutPage />} />
        <Route path="/contact" element={<ContactPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/logout" element={<LogoutPage />} />
        <Route path="*" element={<NoPage />} />
      </Routes>
    </>
  );
}

export default App;
