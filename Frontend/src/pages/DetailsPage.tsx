import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import {Alert, Card, Container, Spinner} from "react-bootstrap";
import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";


interface Review {
    $id: string;
    comment: string;
    rating: number;
    createdAt: string;
    user: string | null;
}

interface Details {
    id: number;
    reviews: { $values: Review[] };
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

    const renderReviews = () => {
        if (!details?.reviews.$values.length) {
            return <p>Brak opinii dla tej oferty.</p>;
        }

        return details.reviews.$values.map((review) => (
            <Card className="mb-3" key={review.$id}>
                <Card.Body>
                    <Card.Title>{review.user || 'Anonymous'}</Card.Title>
                    <Card.Subtitle className="mb-2 text-muted">{review.createdAt}</Card.Subtitle>
                    <Card.Text>{review.comment}</Card.Text>
                </Card.Body>
            </Card>
        ));
    };

    return (
        <>
            <Navigation />
            <Container fluid className="d-flex jh-container-fluid align-items-start justify-content-center py-2 my-2" style={{ minHeight: '90vh' }}>
                {loading && <Spinner animation="border" variant="primary" />}
                {error && <Alert variant="danger">{error}</Alert>}
                {details && (
                    <>
                        {renderReviews()}
                    </>
                )}
            </Container>
            <Footer />
        </>
    );
}

export default DetailsPage;