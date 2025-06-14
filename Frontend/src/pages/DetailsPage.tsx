import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import {
  Alert,
  Badge,
  Button,
  Card,
  Container,
  Spinner,
} from "react-bootstrap";
import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";

interface Skill {
  id: number;
  name: string;
}

interface SkillGroup {
  $id: string;
  id: number;
  skills: { $values: Skill[] };
}

interface EmploymentType {
  $id: string;
  id: number;
  gross: boolean;
  type: string;
  to_Pln: number;
  currency: string;
  from?: number;
  to?: number;
}

interface Details {
  id: number;
  slug: string;
  company: {
    id: number;
    name: string;
    description: string;
    logoUrl: string;
  };
  title: string;
  description: string;
  requiredSkills: { $values: SkillGroup[] };
  city: string;
  workplaceType: string;
  experienceLevel: string;
  employmentTypes: { $values: EmploymentType[] };
}

const getAuthHeaders = (): HeadersInit => {
  const token = localStorage.getItem("token");
  return {
    "Content-Type": "application/json",
    Authorization: token ? `Bearer ${token}` : "",
  };
};

function extractSkillNames(
  skillGroups?: { $values: SkillGroup[] } | null
): string[] {
  if (!skillGroups || !Array.isArray(skillGroups.$values)) return [];

  const allSkills = skillGroups.$values.flatMap((group) => {
    if (!group.skills || !Array.isArray(group.skills.$values)) return [];
    return group.skills.$values.map((skill) => skill.name);
  });

  return Array.from(new Set(allSkills));
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
      const response = await fetch(
        `https://localhost:7111/api/offer?id=${id}`,
        {
          method: "GET",
          headers: getAuthHeaders(),
          mode: "cors",
        }
      );

      if (!response.ok) {
        throw new Error("Error when fetching details");
      }

      const data = await response.json();
      setDetails(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : "An error occurred");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (offerId) {
      fetchDetails(offerId);
    }
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
            {employment.type.toUpperCase()}{" "}
            {employment.gross ? "(Gross)" : "(Net)"} -{" "}
            {employment.from && employment.to
              ? `${employment.from} - ${employment.to}`
              : "Not given"}{" "}
            {employment.currency.toUpperCase()}
          </Badge>
        ))}
      </div>
    );
  };

  const renderDetailsCard = () => {
    if (!details) return null;

    const skills = extractSkillNames(details.requiredSkills);
    const applyUrl = `https://justjoin.it/job-offer/${details.slug}`;

    return (
      <Card className="mb-4 shadow-sm border-primary position-relative">
        <Card.Body>
          <div
            style={{
              position: "absolute",
              top: "15px",
              right: "15px",
              zIndex: 10,
            }}
          >
            <Button
              variant="success"
              href={applyUrl}
              target="_blank"
              rel="noopener noreferrer"
            >
              Apply
            </Button>
          </div>

          <Card.Subtitle className="mb-2 text-muted fs-5">
            <Link
              to={`/company/${details.company.id}`}
              className="text-decoration-none"
            >
              {details.company.name}
            </Link>
          </Card.Subtitle>
          <Card.Title className="fs-3 fw-bold mb-3">{details.title}</Card.Title>

          {renderEmploymentTypes()}

          <div className="mb-3">
            <h5>Required Skills</h5>
            {skills.length > 0 ? (
              skills.map((skill, index) => (
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

          <Card.Text className="fs-6">City: {details.city}</Card.Text>
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
      <Container className="mt-4 mb-5">
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
        {!loading && !error && details && renderDetailsCard()}
      </Container>
      <Footer />
    </>
  );
};

export default DetailsPage;
