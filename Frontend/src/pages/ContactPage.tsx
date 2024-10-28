import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import { Container } from "react-bootstrap";


const ContactPage = () => {
    return (
        <>
            <Navigation />
            <Container fluid className="d-flex flex-column jh-container-fluid" style={{minHeight: '100vh'}}>
                <h1 className="flex-grow-1">Contact page</h1>
            </Container>
            <Footer/>
        </>
    )
}

export default ContactPage;