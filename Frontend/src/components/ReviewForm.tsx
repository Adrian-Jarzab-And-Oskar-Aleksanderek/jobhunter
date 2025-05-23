import React, { useState } from "react";
import { Alert, Button, Form, Spinner } from "react-bootstrap";

interface ReviewFormProps {
  offerId: number;
  onReviewAdded: () => void;
}

const ReviewForm: React.FC<ReviewFormProps> = ({ offerId, onReviewAdded }) => {
  const [newComment, setNewComment] = useState("");
  const [newRating, setNewRating] = useState(1);
  const [addingReview, setAddingReview] = useState(false);
  const [addError, setAddError] = useState<string | null>(null);
  const [addSuccess, setAddSuccess] = useState(false);

  const getAuthToken = (): string | null => localStorage.getItem("token");

  const handleAddReview = async (e: React.FormEvent) => {
    e.preventDefault();
    const token = getAuthToken();
    if (!token) {
      setAddError("Musisz być zalogowany, aby dodać recenzję.");
      return;
    }

    setAddingReview(true);
    setAddError(null);
    setAddSuccess(false);

    try {
      const response = await fetch(`https://localhost:7111/api/Review/create`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          comment: newComment,
          rating: newRating,
          jobOfferId: offerId,
        }),
      });

      if (!response.ok) {
        const data = await response.json();
        throw new Error(data.message || "Nie udało się dodać recenzji.");
      }

      setAddSuccess(true);
      setNewComment("");
      setNewRating(1);
      onReviewAdded();
    } catch (err: any) {
      setAddError(err.message || "Wystąpił błąd podczas dodawania recenzji.");
    } finally {
      setAddingReview(false);
    }
  };

  return (
    <>
      <h5 className="mt-4">Dodaj recenzję</h5>
      {addError && <Alert variant="danger">{addError}</Alert>}
      {addSuccess && (
        <Alert variant="success">
          Twoja recenzja została dodana pomyślnie!
        </Alert>
      )}

      <Form onSubmit={handleAddReview}>
        <Form.Group className="mb-3" controlId="formComment">
          <Form.Label>Komentarz</Form.Label>
          <Form.Control
            as="textarea"
            rows={3}
            placeholder="Wpisz swój komentarz"
            value={newComment}
            onChange={(e) => setNewComment(e.target.value)}
            required
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formRating">
          <Form.Label>Ocena</Form.Label>
          <Form.Select
            value={newRating}
            onChange={(e) => setNewRating(parseInt(e.target.value, 10))}
            required
          >
            <option value={1}>1 - Bardzo źle</option>
            <option value={2}>2 - Źle</option>
            <option value={3}>3 - Średnio</option>
            <option value={4}>4 - Dobrze</option>
            <option value={5}>5 - Doskonale</option>
          </Form.Select>
        </Form.Group>
        <Button variant="primary" type="submit" disabled={addingReview}>
          {addingReview ? (
            <Spinner
              as="span"
              animation="border"
              size="sm"
              role="status"
              aria-hidden="true"
            />
          ) : (
            "Dodaj recenzję"
          )}
        </Button>
      </Form>
    </>
  );
};

export default ReviewForm;
