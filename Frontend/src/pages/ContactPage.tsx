import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import {
  Card,
  Col,
  Container,
  ListGroup,
  ListGroupItem,
  Row,
} from "react-bootstrap";

const ContactPage = () => {
  return (
    <>
      <Navigation />
      <Container className="mt-4 mb-5 vh-100">
        <Row className="justify-content-center">
          <Col md={8} lg={6}>
            <Card className="shadow-sm border-primary">
              <Card.Body>
                <Card.Title className="mb-4 text-center">Contact Us</Card.Title>
                <ListGroup variant="flush">
                  <ListGroupItem className="d-flex align-items-center">
                    <i
                      className="bi bi-envelope-fill me-3"
                      style={{ fontSize: "1.5rem", color: "#0d6efd" }}
                    ></i>
                    <div>
                      <h6 className="mb-0">Email</h6>
                      <span>contact@jobhunter.com</span>
                    </div>
                  </ListGroupItem>
                  <ListGroupItem className="d-flex align-items-center">
                    <i
                      className="bi bi-telephone-fill me-3"
                      style={{ fontSize: "1.5rem", color: "#0d6efd" }}
                    ></i>
                    <div>
                      <h6 className="mb-0">Phone</h6>
                      <span>+48 123 456 789</span>
                    </div>
                  </ListGroupItem>
                  <ListGroupItem className="d-flex align-items-center">
                    <i
                      className="bi bi-geo-alt-fill me-3"
                      style={{ fontSize: "1.5rem", color: "#0d6efd" }}
                    ></i>
                    <div>
                      <h6 className="mb-0">Address</h6>
                      <span>1234 JobHunter St., Warsaw, Poland</span>
                    </div>
                  </ListGroupItem>
                  <ListGroupItem className="d-flex align-items-center">
                    <i
                      className="bi bi-linkedin me-3"
                      style={{ fontSize: "1.5rem", color: "#0d6efd" }}
                    ></i>
                    <div>
                      <h6 className="mb-0">LinkedIn</h6>
                      <span>
                        <a
                          href="https://www.linkedin.com/company/jobhunter"
                          target="_blank"
                          rel="noopener noreferrer"
                        >
                          JobHunter LinkedIn
                        </a>
                      </span>
                    </div>
                  </ListGroupItem>
                  <ListGroupItem className="d-flex align-items-center">
                    <i
                      className="bi bi-twitter me-3"
                      style={{ fontSize: "1.5rem", color: "#0d6efd" }}
                    ></i>
                    <div>
                      <h6 className="mb-0">Twitter</h6>
                      <span>
                        <a
                          href="https://twitter.com/jobhunter"
                          target="_blank"
                          rel="noopener noreferrer"
                        >
                          @JobHunter
                        </a>
                      </span>
                    </div>
                  </ListGroupItem>
                </ListGroup>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
      <Footer />
    </>
  );
};

export default ContactPage;
