import 'react-bootstrap';
import { Container, Nav, Navbar } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import "./Navigation.css";

const Navigation = () => {
    return (
        <Navbar variant="dark" expand="lg" className="bg-primary sticky-top border-bottom">
            <Container>
                <Navbar.Brand className="me-auto jh-navbar-text" href="/">JobHunter</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" className="custom-navbar-toggle"/>
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="ms-auto">
                        <LinkContainer to="/">
                            <Nav.Link className="jh-navbar-text">Home</Nav.Link>
                        </LinkContainer>
                        <LinkContainer to="/about">
                            <Nav.Link className="jh-navbar-text">About</Nav.Link>
                        </LinkContainer>
                        <LinkContainer to="/contact">
                            <Nav.Link className="jh-navbar-text">Contact</Nav.Link>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default Navigation;