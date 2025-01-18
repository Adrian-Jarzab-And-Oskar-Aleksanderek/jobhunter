import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import {Card, Container} from "react-bootstrap";
import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";


const DetailsPage = () => {
    const { id } = useParams<{ id: string }>();
    const offerId = id ? parseInt(id, 10) : null;
    const [details, setDetails] = useState({});
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    /*const fetchDetails = async (id: number) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`http://localhost:5216/api/offer?id=${id}`, {mode: 'cors'});
            if (!response.ok) {
                throw new Error('Error when fetching details');
            }
            const data = await response.json();
            setDetails(data);
        }catch(err: any) {
            setError(err.message);
        }finally {
            setLoading(false);
        }
    };*/

    const fetchDetails = async (id: number) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`http://localhost:5216/api/offer?id=${id}`);
            if (!response.ok) {
                throw new Error('Error when fetching offers');
            }
            const data = await response.json();
            setDetails(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };
    
    useEffect(() => {
        fetchDetails(offerId);
    }, [offerId]);
    
    const renderReviews = () => {
        if (details.reviews.$values.length === 0) {
            return <p>Brak opinii dla tej oferty.</p>;
        }
        return details.reviews.$values.forEach((review: any) => (
            <Card className="mb-3" key={review.$id}>
                <Card.Body>
                    <Card.Title>{review.user}</Card.Title>
                    <Card.Subtitle className="mb-2 text-muted">{review.createdAt}</Card.Subtitle>
                    <Card.Text>{review.comment}</Card.Text>
                </Card.Body>
            </Card>
        ));
    };
    
    return (
        <>
            <Navigation />
            <Container fluid
                       className="d-flex jh-container-fluid align-items-start justify-content-center py-2 my-2"
                       style={{minHeight: '90vh'}}>
                <h1>Details page</h1>
                <p>{details.id}</p>
            </Container>
            <Footer/>
        </>
    )
}

export default DetailsPage;