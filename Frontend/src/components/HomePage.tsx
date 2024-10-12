import 'react-bootstrap';
import { Container, Nav, Navbar } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import Footer from './Footer';

const HomePage = () => {
    return (
            <><Navbar bg="dark" variant="dark" expand="lg">
            <Container>
                <Navbar.Brand href="/">JobHunter</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="ms-auto">
                        <LinkContainer to="/">
                            <Nav.Link>Home</Nav.Link>
                        </LinkContainer>
                        <LinkContainer to="/about">
                            <Nav.Link>About</Nav.Link>
                        </LinkContainer>
                        <LinkContainer to="/contact">
                            <Nav.Link>Contact</Nav.Link>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar><div><Footer></Footer></div></>
    );
}

export default HomePage;