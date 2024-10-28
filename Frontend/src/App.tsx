import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import HomePage from "./pages/HomePage.tsx";
import AboutPage from "./pages/AboutPage.tsx";
import ContactPage from "./pages/ContactPage.tsx";
import NoPage from "./pages/NoPage.tsx";
import "./css/global.css";
function App() {

  return (
      <>
          <Router>
              <Routes>
                  <Route path="/" element={<HomePage />} />
                  <Route path="/about" element={<AboutPage />} />
                  <Route path="/contact" element={<ContactPage />} />
                  <Route path="*" element={<NoPage />} />
              </Routes>
          </Router>
    </>
  )
}

export default App
