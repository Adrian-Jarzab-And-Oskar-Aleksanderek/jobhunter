import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import { Container } from "react-bootstrap";


const DetailsPage = () => {
    return (
        <>
            <Navigation />
            <Container fluid
                       className="d-flex jh-container-fluid align-items-start justify-content-center py-2 my-2"
                       style={{minHeight: '90vh'}}>
                <h1>Details page</h1>
            </Container>
            <Footer/>
        </>
    )
}

export default DetailsPage;