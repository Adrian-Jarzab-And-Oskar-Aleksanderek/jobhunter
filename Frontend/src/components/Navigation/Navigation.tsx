import { Container, Nav, Navbar } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { useAuth } from "../../Context/useAuth";
import "./Navigation.css";

const Navigation = () => {
  const { isLoggedIn, logout } = useAuth();

  return (
    <Navbar
      variant="dark"
      expand="lg"
      className="bg-primary sticky-top border-bottom"
    >
      <Container>
        <Navbar.Brand className="me-auto jh-navbar-text" href="/">
          JobHunter
        </Navbar.Brand>
        <Navbar.Toggle
          aria-controls="basic-navbar-nav"
          className="custom-navbar-toggle"
        />
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

            {!isLoggedIn() && (
              <>
                <LinkContainer to="/login">
                  <Nav.Link className="jh-navbar-text">Login</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/register">
                  <Nav.Link className="jh-navbar-text">Register</Nav.Link>
                </LinkContainer>
              </>
            )}

            {isLoggedIn() && (
              <Nav.Link
                onClick={() => logout()}
                className="jh-navbar-text"
                style={{ cursor: "pointer" }}
              >
                Logout
              </Nav.Link>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default Navigation;
