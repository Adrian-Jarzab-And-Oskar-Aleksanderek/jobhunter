// HomePage.jsx
import 'react-bootstrap';
import { Container, Pagination, Spinner, Alert } from 'react-bootstrap';
import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import JobCard from "../components/JobCard/JobCard.tsx";
import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

const HomePage = () => {
    const [totalPages, setTotalPages] = useState(3);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [offers, setOffers] = useState([]);

    const { pageNumber } = useParams<{ pageNumber: string }>();
    const navigate = useNavigate();
    const parsedPageNumber = pageNumber ? parseInt(pageNumber) : 0;

    const fetchOffers = async (page: number) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`http://localhost:5216/api/offers/page/${page}`);
            if (!response.ok) {
                throw new Error('Błąd podczas pobierania ofert');
            }
            const data = await response.json();
            setTotalPages(10);
            setOffers(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchOffers(parsedPageNumber);
    }, [parsedPageNumber]);

    const handlePageChange = (page: number) => {
        navigate(`/offers/page/${page}`);
    };

    const renderJobCards = () => {
        return offers.map(offer => (
            <JobCard
                key={offer.id}
                title={offer.title}
                companyName={offer.companyName}
                requiredSkills={offer.requiredSkills}
            />
        ));
    };
    
    const renderPagination = () => {
        let items = [];

        const maxPagesToShow = 5;
        let startPage = Math.max(parsedPageNumber - Math.floor(maxPagesToShow / 2), 0);
        let endPage = startPage + maxPagesToShow;

        if (endPage > totalPages) {
            endPage = totalPages;
            startPage = Math.max(endPage - maxPagesToShow, 0);
        }

        for (let number = startPage; number < endPage; number++) {
            items.push(
                <Pagination.Item
                    key={number}
                    active={number === parsedPageNumber}
                    onClick={() => handlePageChange(number)}
                >
                    {number + 1}
                </Pagination.Item>,
            );
        }

        return (
            <Pagination className="justify-content-center mt-4">
                <Pagination.First
                    onClick={() => handlePageChange(0)}
                    disabled={parsedPageNumber === 0}
                />
                <Pagination.Prev
                    onClick={() => handlePageChange(Math.max(parsedPageNumber - 1, 0))}
                    disabled={parsedPageNumber === 0}
                />
                {items}
                <Pagination.Next
                    onClick={() => handlePageChange(Math.min(parsedPageNumber + 1, totalPages - 1))}
                    disabled={parsedPageNumber === totalPages - 1}
                />
                <Pagination.Last
                    onClick={() => handlePageChange(totalPages - 1)}
                    disabled={parsedPageNumber === totalPages - 1}
                />
            </Pagination>
        );
    };

    return (
        <>
            <Navigation />
            <Container fluid className="d-flex flex-column">
                {loading && <Spinner animation="border" variant="primary" />}
                {error && <Alert variant="danger">{error}</Alert>}
                {!loading && !error && renderJobCards()}
                {!loading && !error && totalPages > 1 && renderPagination()}
            </Container>
            <Footer />
        </>
    );
}

export default HomePage;