import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import { Container } from "react-bootstrap";

const AboutPage = () => {
    return (
        <>
            <Navigation />
            <Container fluid className="d-flex flex-column jh-container-fluid" style={{minHeight: '90vh'}}>
                <h1 className="flex-grow-1">About page</h1>
            </Container>
            <Footer/>
        </>
    )
}

export default AboutPage;