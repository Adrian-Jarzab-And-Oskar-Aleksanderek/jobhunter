import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import Navigation from "../components/Navigation/Navigation";
import Footer from "../components/Footer/Footer";
import {
  Alert,
  Badge,
  Card,
  Col,
  Container,
  Row,
  Spinner,
  Form,
  Button,
  Modal,
} from "react-bootstrap";

interface EmploymentType {
  $id: string;
  type: string;
  from: number;
  to: number;
  currency: string;
  gross: boolean;
}

interface JobOffer {
  $id: string;
  id: number;
  title: string;
  city: string;
  workplaceType: string;
  experienceLevel: string;
  employmentTypes: {
    $id: string;
    $values: EmploymentType[];
  };
}

interface Review {
  id: number;
  rating: number;
  comment: string;
  createdAt: string;
  user: string;
}

interface ReviewFormData {
  comment: string;
  rating: number;
}

interface Company {
  $id: string;
  id: number;
  name: string;
  description: string;
  logoUrl: string;
  jobOffers: {
    $id: string;
    $values: JobOffer[];
  };
  reviews: {
    $id: string;
    $values: Review[];
  };
}

const CompanyProfilePage = () => {
  const { id } = useParams<{ id: string }>();
  const [company, setCompany] = useState<Company | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showReviewModal, setShowReviewModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [reviewForm, setReviewForm] = useState<ReviewFormData>({
    comment: "",
    rating: 5,
  });
  const [editingReview, setEditingReview] = useState<Review | null>(null);
  const [reviewError, setReviewError] = useState<string | null>(null);
  const [canReview, setCanReview] = useState<boolean | null>(null);

  useEffect(() => {
    const fetchCompany = async () => {
      try {
        const response = await fetch(
          `https://localhost:7111/api/company/${id}`
        );
        if (!response.ok) {
          throw new Error("Failed to fetch company data");
        }
        const data = await response.json();
        setCompany(data);
      } catch (err) {
        setError(err instanceof Error ? err.message : "An error occurred");
      } finally {
        setLoading(false);
      }
    };

    const checkCanReview = async () => {
      const token = localStorage.getItem("token");
      if (!token) {
        setCanReview(false);
        return;
      }

      try {
        const response = await fetch(
          `https://localhost:7111/api/Review/can-review?companyId=${id}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        if (response.ok) {
          const data = await response.json();
          setCanReview(data.canReview);
        }
      } catch (err) {
        console.error("Failed to check if user can review:", err);
      }
    };

    if (id) {
      fetchCompany();
      checkCanReview();
    }
  }, [id]);

  const handleReviewSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setReviewError(null);

    try {
      const token = localStorage.getItem("token");
      if (!token) {
        setReviewError("You must be logged in to add a review");
        return;
      }

      const response = await fetch("https://localhost:7111/api/Review/create", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          companyId: company?.id,
          comment: reviewForm.comment,
          rating: reviewForm.rating,
        }),
      });

      if (!response.ok) {
        throw new Error("Failed to add review");
      }

      if (id) {
        const updatedResponse = await fetch(
          `https://localhost:7111/api/company/${id}`
        );
        if (updatedResponse.ok) {
          const updatedData = await updatedResponse.json();
          setCompany(updatedData);
        }
      }

      setShowReviewModal(false);
      setReviewForm({ comment: "", rating: 5 });
    } catch (err) {
      setReviewError(
        err instanceof Error ? err.message : "Failed to add review"
      );
    }
  };

  const handleEditReview = async (e: React.FormEvent) => {
    e.preventDefault();
    setReviewError(null);

    try {
      const token = localStorage.getItem("token");
      if (!token || !editingReview) {
        return;
      }

      const response = await fetch("https://localhost:7111/api/Review/edit", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          reviewId: editingReview.id,
          comment: reviewForm.comment,
          rating: reviewForm.rating,
        }),
      });

      if (!response.ok) {
        throw new Error("Failed to edit review");
      }

      if (id) {
        const updatedResponse = await fetch(
          `https://localhost:7111/api/company/${id}`
        );
        if (updatedResponse.ok) {
          const updatedData = await updatedResponse.json();
          setCompany(updatedData);
        }
      }

      setShowEditModal(false);
      setEditingReview(null);
      setReviewForm({ comment: "", rating: 5 });
    } catch (err) {
      setReviewError(
        err instanceof Error ? err.message : "Failed to edit review"
      );
    }
  };

  const handleDeleteReview = async (reviewId: number) => {
    if (!window.confirm("Are you sure you want to delete this review?")) {
      return;
    }

    try {
      const token = localStorage.getItem("token");
      if (!token) {
        setReviewError("You must be logged in to delete a review");
        return;
      }

      const response = await fetch("https://localhost:7111/api/Review/delete", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          reviewId: reviewId,
        }),
      });

      if (!response.ok) {
        throw new Error("Failed to delete review");
      }

      if (id) {
        const updatedResponse = await fetch(
          `https://localhost:7111/api/company/${id}`
        );
        if (updatedResponse.ok) {
          const updatedData = await updatedResponse.json();
          setCompany(updatedData);
        }
      }
    } catch (err) {
      setReviewError(
        err instanceof Error ? err.message : "Failed to delete review"
      );
    }
  };

  const openEditModal = (review: Review) => {
    setEditingReview(review);
    setReviewForm({
      comment: review.comment,
      rating: review.rating,
    });
    setShowEditModal(true);
  };

  if (loading) {
    return (
      <>
        <Navigation />
        <Container className="mt-5 text-center">
          <Spinner animation="border" variant="primary" />
        </Container>
      </>
    );
  }

  if (error) {
    return (
      <>
        <Navigation />
        <Container className="mt-5">
          <Alert variant="danger">{error}</Alert>
        </Container>
      </>
    );
  }

  if (!company) {
    return (
      <>
        <Navigation />
        <Container className="mt-5">
          <Alert variant="warning">Company not found</Alert>
        </Container>
      </>
    );
  }

  return (
    <>
      <Navigation />
      <Container className="mt-4 mb-5">
        <Row>
          <Col md={4}>
            <Card className="mb-4 shadow-sm">
              <Card.Body>
                {company.logoUrl && (
                  <img
                    src={company.logoUrl}
                    alt={`${company.name} logo`}
                    className="img-fluid mb-3"
                    style={{ maxHeight: "200px" }}
                  />
                )}
                <Card.Title className="fs-3">{company.name}</Card.Title>
                <Card.Text>
                  {company.description || "No description available"}
                </Card.Text>
              </Card.Body>
            </Card>

            <Card className="mb-4 shadow-sm">
              <Card.Body>
                <div className="d-flex justify-content-between align-items-center mb-3">
                  <Card.Title className="mb-0">Reviews</Card.Title>
                  {canReview && (
                    <Button
                      variant="primary"
                      size="sm"
                      onClick={() => setShowReviewModal(true)}
                    >
                      Add Review
                    </Button>
                  )}
                </div>
                {company.reviews.$values.length > 0 ? (
                  company.reviews.$values.map((review) => {
                    const userInfo = localStorage.getItem("user");
                    const currentUsername = userInfo
                      ? JSON.parse(userInfo).userName
                      : null;

                    return (
                      <div key={review.id} className="card border-primary my-3">
                        <div className="card-header d-flex align-items-center justify-content-start gap-2">
                          <div>
                            <small className="text-muted d-block mb-1">
                              {new Date(review.createdAt).toLocaleDateString()}
                            </small>
                            <strong className="d-block">{review.user}</strong>
                          </div>
                          {review.user === currentUsername && (
                            <div className="d-flex gap-2 ms-auto">
                              <Button
                                variant="outline-primary"
                                size="sm"
                                onClick={() => openEditModal(review)}
                              >
                                Edit
                              </Button>
                              <Button
                                variant="outline-danger"
                                size="sm"
                                onClick={() => handleDeleteReview(review.id)}
                              >
                                Delete
                              </Button>
                            </div>
                          )}
                        </div>
                        <div className="card-body">
                          <Badge bg="warning" text="dark" className="mb-2">
                            {review.rating}/5
                          </Badge>
                          <p className="card-text">{review.comment}</p>
                        </div>
                      </div>
                    );
                  })
                ) : (
                  <p>No reviews yet</p>
                )}
              </Card.Body>
            </Card>
          </Col>

          <Col md={8}>
            <h3 className="mb-4">Job Offers</h3>
            {company.jobOffers.$values.length > 0 ? (
              company.jobOffers.$values.map((offer) => (
                <Link
                  key={offer.id}
                  to={`/offers/${offer.id}`}
                  className="text-decoration-none text-dark"
                >
                  <Card className="mb-3 shadow-sm">
                    <Card.Body>
                      <Card.Title>{offer.title}</Card.Title>
                      <div className="mb-2">
                        <Badge bg="info" className="me-2">
                          {offer.city}
                        </Badge>
                        <Badge bg="secondary" className="me-2">
                          {offer.workplaceType}
                        </Badge>
                        <Badge bg="primary" className="me-2">
                          {offer.experienceLevel}
                        </Badge>
                      </div>
                      <div>
                        {offer.employmentTypes.$values.map((type, index) => (
                          <Badge key={index} bg="success" className="me-2">
                            {type.type}: {type.from} - {type.to} {type.currency}
                            {type.gross ? " (Gross)" : " (Net)"}
                          </Badge>
                        ))}
                      </div>
                    </Card.Body>
                  </Card>
                </Link>
              ))
            ) : (
              <Alert variant="info">No job offers available</Alert>
            )}
          </Col>
        </Row>
      </Container>

      <Modal show={showReviewModal} onHide={() => setShowReviewModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Add Review</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {reviewError && <Alert variant="danger">{reviewError}</Alert>}
          <Form onSubmit={handleReviewSubmit}>
            <Form.Group className="mb-3">
              <Form.Label>Rating</Form.Label>
              <Form.Select
                value={reviewForm.rating}
                onChange={(e) =>
                  setReviewForm({
                    ...reviewForm,
                    rating: parseInt(e.target.value),
                  })
                }
              >
                {[5, 4, 3, 2, 1].map((rating) => (
                  <option key={rating} value={rating}>
                    {rating} {rating === 1 ? "star" : "stars"}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Comment</Form.Label>
              <Form.Control
                as="textarea"
                rows={3}
                value={reviewForm.comment}
                onChange={(e) =>
                  setReviewForm({ ...reviewForm, comment: e.target.value })
                }
                required
              />
            </Form.Group>
            <Button variant="primary" type="submit">
              Submit Review
            </Button>
          </Form>
        </Modal.Body>
      </Modal>

      <Modal show={showEditModal} onHide={() => setShowEditModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Edit Review</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {reviewError && <Alert variant="danger">{reviewError}</Alert>}
          <Form onSubmit={handleEditReview}>
            <Form.Group className="mb-3">
              <Form.Label>Rating</Form.Label>
              <Form.Select
                value={reviewForm.rating}
                onChange={(e) =>
                  setReviewForm({
                    ...reviewForm,
                    rating: parseInt(e.target.value),
                  })
                }
              >
                {[5, 4, 3, 2, 1].map((rating) => (
                  <option key={rating} value={rating}>
                    {rating} {rating === 1 ? "star" : "stars"}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label>Comment</Form.Label>
              <Form.Control
                as="textarea"
                rows={3}
                value={reviewForm.comment}
                onChange={(e) =>
                  setReviewForm({ ...reviewForm, comment: e.target.value })
                }
                required
              />
            </Form.Group>
            <Button variant="primary" type="submit">
              Save Changes
            </Button>
          </Form>
        </Modal.Body>
      </Modal>

      <Footer />
    </>
  );
};

export default CompanyProfilePage;
