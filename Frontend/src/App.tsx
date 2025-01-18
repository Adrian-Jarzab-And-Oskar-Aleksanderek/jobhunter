import {BrowserRouter as Router, Navigate, Route, Routes} from 'react-router-dom'
import HomePage from "./pages/HomePage.tsx";
import AboutPage from "./pages/AboutPage.tsx";
import ContactPage from "./pages/ContactPage.tsx";
import NoPage from "./pages/NoPage.tsx";
import DetailsPage from "./pages/DetailsPage.tsx";
function App() {

  return (
      <>
          <Router>
              <Routes>
                  <Route path="/" element={<Navigate to="/offers/page/0" replace />} />
                  <Route path="/offers/page/:pageNumber" element={<HomePage />} />
                  <Route path="/offers/:id" element={<DetailsPage />} />
                  <Route path="/about" element={<AboutPage />} />
                  <Route path="/contact" element={<ContactPage />} />
                  <Route path="*" element={<NoPage />} />
              </Routes>
          </Router>
    </>
  )
}

export default App
