import { Container, Nav, Navbar } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { useAuth } from "../../Context/useAuth";
import "./Navigation.css";
import { Dropdown } from "react-bootstrap";

const Navigation = () => {
  const { user, isLoggedIn, logout } = useAuth();

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
              <Nav.Item>
                <Dropdown>
                  <Dropdown.Toggle
                    variant="link"
                    className="nav-link jh-navbar-text"
                    style={{ cursor: "pointer" }}
                  >
                    {user?.userName}
                  </Dropdown.Toggle>

                  <Dropdown.Menu>
                    <Dropdown.Item href="/profile">User Profile</Dropdown.Item>
                    <Dropdown.Item onClick={logout}>Logout</Dropdown.Item>
                  </Dropdown.Menu>
                </Dropdown>
              </Nav.Item>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default Navigation;
