import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import {Alert, Badge, Card, Col, Container, Row, Spinner} from "react-bootstrap";
import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";


interface Review {
    $id: string;
    comment: string;
    rating: number;
    createdAt: string;
    user: string | null;
}

interface EmploymentType {
    $id: string;
    id: number;
    gross: boolean;
    type: string;
    to_Pln: number;
    currency: string;
}

interface Details {
    id: number;
    companyName: string;
    title: string;
    description: string;
    requiredSkills: { $values: string[] };
    reviews: { $values: Review[] };
    city: string;
    workplaceType: string;
    experienceLevel: string;
    employmentTypes: { $values: EmploymentType[] };
}

const DetailsPage = () => {
    const { id } = useParams<{ id: string }>();
    const offerId = id ? parseInt(id, 10) : null;
    const [details, setDetails] = useState<Details | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const fetchDetails = async (id: number) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`http://localhost:5216/api/offer?id=${id}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
                mode: 'cors'
            });

            if (!response.ok) {
                throw new Error('Error when fetching details');
            }

            const data = await response.json();
            setDetails(data);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };
    
    useEffect(() => {
        if(offerId){
            fetchDetails(offerId);
        }
        console.log(details);
    }, [offerId]);

    const renderEmploymentTypes = () => {
        if (!details?.employmentTypes.$values.length) {
            return <p className="text-center">No employment types available.</p>;
        }

        return (
            <div className="mb-3">
                <h5>Employment Types</h5>
                {details.employmentTypes.$values.map((employment: EmploymentType) => (
                    <Badge key={employment.$id} bg="info" className="me-2 mb-2">
                        {employment.type.toUpperCase()} {employment.gross ? "(Gross)" : "(Net)"} - {employment.currency.toUpperCase()}
                    </Badge>
                ))}
            </div>
        );
    };
    
    const renderReviews = () => {
        if (!details?.reviews.$values.length) {
            return <p className="text-center">No reviews for this offer yet.</p>;
        }

        return (
            <Row>
                {details.reviews.$values.map((review: Review) => (
                    <Col md={6} lg={4} key={review.$id} className="mb-4">
                        <Card className="h-100 shadow-sm">
                            <Card.Body>
                                <Card.Title>{review.user || 'Anonymous'}</Card.Title>
                                <Card.Subtitle className="mb-2 text-muted">{new Date(review.createdAt).toLocaleDateString()}</Card.Subtitle>
                                <Card.Text>{review.comment}</Card.Text>
                                <Badge bg="warning" text="dark">
                                    Rating: {review.rating}/5
                                </Badge>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        );
    };

    const renderDetailsCard = () => {
        if (!details) return null;

        return (
            <Card className="mb-4 shadow-sm border-primary">
                <Card.Body>
                    <Card.Title className="fs-3 fw-bold">{details.title}</Card.Title>
                    <Card.Subtitle className="mb-3 text-muted fs-5">{details.companyName}</Card.Subtitle>

                    {renderEmploymentTypes()}

                    <div className="mb-3">
                        <h5>Required Skills</h5>
                        {details.requiredSkills.$values.length > 0 ? (
                            details.requiredSkills.$values.map((skill, index) => (
                                <Badge key={index} bg="secondary" className="me-1 mb-1">
                                    {skill}
                                </Badge>
                            ))
                        ) : (
                            <Badge bg="light" text="dark">
                                No required skills
                            </Badge>
                        )}
                    </div>
                    
                    <Card.Text className="fs-6">
                        City: {details.city}
                    </Card.Text>
                    <Card.Text className="fs-6">
                        Workplace type: {details.workplaceType}
                    </Card.Text>
                    <Card.Text className="fs-6">
                        Experience level: {details.experienceLevel}
                    </Card.Text>
                </Card.Body>
            </Card>
        );
    };

    return (
        <>
            <Navigation />
            <Container className="mt-4 mb-5 vh-100">
                {loading && (
                    <div className="d-flex justify-content-center my-5">
                        <Spinner animation="border" variant="primary" />
                    </div>
                )}
                {error && (
                    <Alert variant="danger" className="text-center">
                        {error}
                    </Alert>
                )}
                {!loading && !error && details && (
                    <>
                        {renderDetailsCard()}

                        <h3 className="mb-3">Reviews</h3>
                        {renderReviews()}
                    </>
                )}
            </Container>
            <Footer />
        </>
    );
}

export default DetailsPage;