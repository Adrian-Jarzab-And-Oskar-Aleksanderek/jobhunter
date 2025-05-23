import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import { Card, Col, Container, Row } from "react-bootstrap";

const AboutPage = () => {
  return (
    <>
      <Navigation />
      <Container className="mt-4 mb-5 vh-100">
        <Row className="justify-content-center">
          <Col md={8}>
            <Card className="shadow-sm">
              <Card.Body>
                <Card.Title className="mb-4">About JobHunter</Card.Title>
                <Card.Text className="fs-5">
                  <strong>JobHunter</strong> is a comprehensive platform
                  designed to aggregate various IT job offers from multiple
                  sources. Our mission is to simplify the job search process for
                  IT professionals by providing a centralized location where
                  they can find and review job opportunities.
                </Card.Text>
                <Card.Text className="fs-5">
                  Users can browse through a wide range of IT job listings,
                  filter them based on their preferences, and leave reviews
                  based on their experiences with different companies and roles.
                  These reviews help other job seekers make informed decisions
                  and ensure transparency in the job market.
                </Card.Text>
                <Card.Text className="fs-5">
                  Whether you're a seasoned IT professional or just starting
                  your career, <strong>JobHunter</strong> is here to support you
                  in finding the perfect job that matches your skills and
                  aspirations.
                </Card.Text>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
      <Footer />
    </>
  );
};

export default AboutPage;
