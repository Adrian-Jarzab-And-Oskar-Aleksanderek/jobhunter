import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import {Alert, Badge, Button, Card, Col, Container, Form, Modal, Row, Spinner} from "react-bootstrap";
import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";


interface Review {
    $id: string;
    id: number;
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

    const [showEditModal, setShowEditModal] = useState<boolean>(false);
    const [currentReview, setCurrentReview] = useState<Review | null>(null);
    const [editComment, setEditComment] = useState<string>('');
    const [editRating, setEditRating] = useState<number>(1);

    const [newComment, setNewComment] = useState<string>('');
    const [newRating, setNewRating] = useState<number>(1);
    const [addingReview, setAddingReview] = useState<boolean>(false);
    const [addError, setAddError] = useState<string | null>(null);
    const [addSuccess, setAddSuccess] = useState<boolean>(false);
    
    const fetchDetails = async (id: number) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`https://localhost:7111/api/offer?id=${id}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
                mode: 'cors'
            });

            if (!response.ok) {
                // throw new Error('Error when fetching details');
            }

            const data = await response.json();
            setDetails(data);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (reviewId: number) => {
        if (!window.confirm("Are you sure you want to delete this review?")) return;

        try {
            const response = await fetch(`http://localhost:5216/api/ReviewControler/delete`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ reviewId: reviewId })
            });

            if (!response.ok) {
                // throw new Error('Failed to delete review.');
            }

            await fetchDetails(offerId!);
        } catch (err: any) {
            alert(err.message || 'Something went wrong while deleting the review.');
        }
    };

    const handleEdit = (review: Review) => {
        setCurrentReview(review);
        setEditComment(review.comment);
        setEditRating(review.rating);
        setShowEditModal(true);
    };
    const submitEdit = async () => {
        if (!currentReview) return;

        try {
            const response = await fetch(`http://localhost:5216/api/ReviewControler/edit`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    reviewId: currentReview.id,
                    comment: editComment,
                    rating: editRating
                })
            });

            if (!response.ok) {
                // throw new Error('Failed to edit review.');
            }

            setShowEditModal(false);
            setCurrentReview(null);
            fetchDetails(offerId!);
        } catch (err: any) {
            alert(err.message || 'Something went wrong while editing the review.');
        }
    };

    const handleAddReview = async (e: React.FormEvent) => {
        e.preventDefault();
        setAddingReview(true);
        setAddError(null);
        setAddSuccess(false);

        try {
            console.log(editComment);
            console.log(editRating);
            const response = await fetch(`http://localhost:5216/api/ReviewControler/create`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    comment: newComment,
                    rating: newRating,
                    jobOfferId: offerId
                })
            });

            if (!response.ok) {
                const data = await response.json();
                // throw new Error(data.message || 'Failed to add review.');
            }

            setAddSuccess(true);
            setNewComment('');
            setNewRating(1);
            fetchDetails(offerId!);
        } catch (err: any) {
            setAddError(err.message || 'Something went wrong while adding the review.');
        } finally {
            setAddingReview(false);
        }
    };
    
    useEffect(() => {
        if(offerId){
            fetchDetails(offerId);
        }
        console.log(details);
    }, [offerId]);


    //TO DO: OFFER DATA VALIDATION - SITE CRASH IF DON'T HAVE ANY DATA
    const renderEmploymentTypes = () => {
        if (!details?.employmentTypes.$values.length) {
            return <p className="text-center">No employment types available.</p>;
        }

        return (
            <div className="mb-3">
                <h5>Employment Types</h5>
                {details.employmentTypes.$values.map((employment: EmploymentType) => (
                    <Badge key={employment.$id} bg="info" className="me-2 mb-2">
                        {employment.type.toUpperCase()} {employment.gross ? "(Gross)" : "(Net)"} - {employment.to_Pln || "Not given"} {employment.currency.toUpperCase()}
                    </Badge>
                ))}
            </div>
        );
    };
    
    //TO DO: OFFER DATA VALIDATION - SITE CRASH IF DON'T HAVE ANY DATA
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
                                <Card.Subtitle
                                    className="mb-2 text-muted">{new Date(review.createdAt).toLocaleDateString()}</Card.Subtitle>
                                <Card.Text>{review.comment}</Card.Text>
                                <Badge bg="warning" text="dark">
                                    Rating: {review.rating}/5
                                </Badge>
                                <div className="mt-3">
                                    <Button variant="outline-primary" size="sm" className="me-2"
                                            onClick={() => handleEdit(review)}>
                                        Edit
                                    </Button>
                                    <Button variant="outline-danger" size="sm" onClick={() => handleDelete(review.id)}>
                                        Delete
                                    </Button>
                                </div>
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
                    <Card.Title className="fs-3 fw-bold">{details?.title}</Card.Title>
                    <Card.Subtitle className="mb-3 text-muted fs-5">{details?.companyName}</Card.Subtitle>

                    {renderEmploymentTypes()}

                    <div className="mb-3">
                        <h5>Required Skills</h5>
                        {details?.requiredSkills?.$values.length > 0 ? (
                            details?.requiredSkills?.$values.map((skill, index) => (
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
                    <h5 className="mt-4">Add a Review</h5>
                    {addError && <Alert variant="danger">{addError}</Alert>}
                    {addSuccess && <Alert variant="success">Your review has been added successfully!</Alert>}
                    <Form onSubmit={handleAddReview}>
                        <Form.Group className="mb-3" controlId="formComment">
                            <Form.Label>Comment</Form.Label>
                            <Form.Control
                                as="textarea"
                                rows={3}
                                placeholder="Enter your comment"
                                value={newComment}
                                onChange={(e) => setNewComment(e.target.value)}
                                required
                            />
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formRating">
                            <Form.Label>Rating</Form.Label>
                            <Form.Select value={newRating} onChange={(e) => setNewRating(parseInt(e.target.value, 10))}
                                         required>
                                <option value={1}>1 - Very Bad</option>
                                <option value={2}>2 - Bad</option>
                                <option value={3}>3 - Okay</option>
                                <option value={4}>4 - Good</option>
                                <option value={5}>5 - Excellent</option>
                            </Form.Select>
                        </Form.Group>
                        <Button variant="primary" type="submit" disabled={addingReview}>
                            {addingReview ? <Spinner as="span" animation="border" size="sm" role="status"
                                                     aria-hidden="true"/> : 'Add Review'}
                        </Button>
                    </Form>
                </Card.Body>
            </Card>
        );
    };

    return (
        <>
            <Navigation/>
            <Container className="mt-4 mb-5">
                {loading && (
                    <div className="d-flex justify-content-center my-5">
                        <Spinner animation="border" variant="primary"/>
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
            
            <Modal show={showEditModal} onHide={() => setShowEditModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit Review</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {currentReview && (
                        <Form>
                            <Form.Group className="mb-3" controlId="editComment">
                                <Form.Label>Comment</Form.Label>
                                <Form.Control
                                    as="textarea"
                                    rows={3}
                                    value={editComment}
                                    onChange={(e) => setEditComment(e.target.value)}
                                />
                            </Form.Group>
                            <Form.Group className="mb-3" controlId="editRating">
                                <Form.Label>Rating</Form.Label>
                                <Form.Select value={editRating} onChange={(e) => setEditRating(parseInt(e.target.value, 10))}>
                                    <option value={1}>1 - Very Bad</option>
                                    <option value={2}>2 - Bad</option>
                                    <option value={3}>3 - Okay</option>
                                    <option value={4}>4 - Good</option>
                                    <option value={5}>5 - Excellent</option>
                                </Form.Select>
                            </Form.Group>
                        </Form>
                    )}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowEditModal(false)}>
                        Cancel
                    </Button>
                    <Button variant="primary" onClick={submitEdit}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
            <Footer />
        </>
    );
}

export default DetailsPage;