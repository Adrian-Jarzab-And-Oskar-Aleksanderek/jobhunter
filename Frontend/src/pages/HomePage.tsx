import "react-bootstrap";
import {
  Container,
  Pagination,
  Spinner,
  Alert,
  Row,
  Col,
} from "react-bootstrap";
import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import JobCard from "../components/JobCard/JobCard.tsx";
import Filter, { FilterState } from "../components/Filter/Filter.tsx";
import { useEffect, useState } from "react";
import {
  useParams,
  useNavigate,
  Link,
  useSearchParams,
} from "react-router-dom";

interface Skill {
  id: number;
  name: string;
}

interface SkillGroup {
  skills: Skill[] | { $values: Skill[] };
}

interface EmploymentType {
  $id: string;
  id: number;
  gross: boolean;
  type: string;
  to_Pln?: number;
  currency: string;
  from?: number;
  to?: number;
}

interface JobOffer {
  id: number;
  title: string;
  company: {
    name: string;
  };
  experienceLevel: string;
  requiredSkills: {
    $values: SkillGroup[];
  };
  employmentTypes: {
    $values: EmploymentType[];
  };
}

const HomePage = () => {
  const { pageNumber } = useParams<{ pageNumber: string }>();
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();
  const parsedPageNumber = pageNumber ? parseInt(pageNumber) : 0;

  const [totalPages, setTotalPages] = useState(3);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [offers, setOffers] = useState<JobOffer[]>([]);
  const [apiPage, setApiPage] = useState<number>(parsedPageNumber);
  const [searchTerm, setSearchTerm] = useState(
    searchParams.get("search") || ""
  );
  const [filters, setFilters] = useState<FilterState>({
    experienceLevel: searchParams.get("experienceLevel") || "",
    employmentType: searchParams.get("employmentType") || "",
    minSalary: searchParams.get("minSalary") || "",
    maxSalary: searchParams.get("maxSalary") || "",
  });

  const fetchOffers = async (page: number) => {
    setLoading(true);
    setError(null);
    try {
      const queryParams = new URLSearchParams({
        page: page.toString(),
        search: searchTerm,
        ...(filters.experienceLevel && {
          experienceLevel: filters.experienceLevel,
        }),
        ...(filters.employmentType && {
          employmentType: filters.employmentType,
        }),
        ...(filters.minSalary && { minSalary: filters.minSalary }),
        ...(filters.maxSalary && { maxSalary: filters.maxSalary }),
      });

      const response = await fetch(
        `https://localhost:7111/api/search?${queryParams.toString()}`
      );
      if (!response.ok) {
        throw new Error("Error when fetching offers");
      }
      const data = await response.json();
      setTotalPages(data.totalPages);
      setApiPage(data.page);
      setOffers(data.jobOffers.$values);
    } catch (err: unknown) {
      setError(
        err instanceof Error ? err.message : "An unknown error occurred"
      );
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchOffers(parsedPageNumber);
  }, [parsedPageNumber, searchTerm, filters]);

  const handlePageChange = (page: number) => {
    if (page >= 0 && page < totalPages) {
      navigate(`/offers/page/${page}`);
    }
  };

  const handleSearch = (term: string) => {
    setSearchTerm(term);
    const newSearchParams = new URLSearchParams(searchParams);
    if (term) {
      newSearchParams.set("search", term);
    } else {
      newSearchParams.delete("search");
    }
    setSearchParams(newSearchParams);
    navigate("/offers/page/0");
  };

  const handleFilterChange = (newFilters: FilterState) => {
    setFilters(newFilters);
    const newSearchParams = new URLSearchParams(searchParams);

    if (newFilters.experienceLevel) {
      newSearchParams.set("experienceLevel", newFilters.experienceLevel);
    } else {
      newSearchParams.delete("experienceLevel");
    }

    if (newFilters.employmentType) {
      newSearchParams.set("employmentType", newFilters.employmentType);
    } else {
      newSearchParams.delete("employmentType");
    }

    if (newFilters.minSalary) {
      newSearchParams.set("minSalary", newFilters.minSalary);
    } else {
      newSearchParams.delete("minSalary");
    }

    if (newFilters.maxSalary) {
      newSearchParams.set("maxSalary", newFilters.maxSalary);
    } else {
      newSearchParams.delete("maxSalary");
    }

    setSearchParams(newSearchParams);
    navigate("/offers/page/0");
  };

  const renderJobCards = () => {
    return offers.map((offer) => (
      <div key={offer.id} style={{ width: "100%", margin: "0 auto" }}>
        <Link
          to={`/offers/${offer.id}`}
          style={{ textDecoration: "none", color: "inherit" }}
        >
          <JobCard
            title={offer.title}
            companyName={offer.company.name}
            experienceLevel={offer.experienceLevel}
            requiredSkills={offer.requiredSkills.$values}
            employmentTypes={offer.employmentTypes}
          />
        </Link>
      </div>
    ));
  };

  const renderPagination = () => {
    const items = [];

    const maxPagesToShow = 5;
    let startPage = Math.max(apiPage - Math.floor(maxPagesToShow / 2), 0);
    let endPage = startPage + maxPagesToShow;

    if (endPage > totalPages) {
      endPage = totalPages;
      startPage = Math.max(endPage - maxPagesToShow, 0);
    }

    for (let number = startPage; number < endPage; number++) {
      items.push(
        <Pagination.Item
          key={number}
          active={number === apiPage}
          onClick={() => handlePageChange(number)}
        >
          {number + 1}
        </Pagination.Item>
      );
    }

    return (
      <Pagination className="justify-content-center mt-4">
        <Pagination.First
          onClick={() => handlePageChange(0)}
          disabled={apiPage === 0}
        />
        <Pagination.Prev
          onClick={() => handlePageChange(Math.max(apiPage - 1, 0))}
          disabled={apiPage === 0}
        />
        {items}
        <Pagination.Next
          onClick={() =>
            handlePageChange(Math.min(apiPage + 1, totalPages - 1))
          }
          disabled={apiPage === totalPages - 1}
        />
        <Pagination.Last
          onClick={() => handlePageChange(totalPages - 1)}
          disabled={apiPage === totalPages - 1}
        />
      </Pagination>
    );
  };

  return (
    <>
      <Navigation />
      <Container fluid>
        <Row className="mt-4">
          <Col xs={12} md={3} className="d-flex justify-content-center">
            <Filter
              onSearch={handleSearch}
              onFilterChange={handleFilterChange}
            />
          </Col>
          <Col xs={12} md={9}>
            <div className="d-flex flex-column align-items-center">
              {loading && <Spinner animation="border" variant="primary" />}
              {error && <Alert variant="danger">{error}</Alert>}
              {!loading && !error && renderJobCards()}
              {!loading && !error && totalPages > 1 && renderPagination()}
            </div>
          </Col>
        </Row>
      </Container>
      <Footer />
    </>
  );
};

export default HomePage;
