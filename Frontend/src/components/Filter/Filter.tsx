import React, { useState } from "react";
import { Form, InputGroup, Button, Card } from "react-bootstrap";
import "./Filter.css";

interface FilterProps {
  onSearch: (searchTerm: string) => void;
  onFilterChange: (filters: FilterState) => void;
}

export interface FilterState {
  experienceLevel: string;
  employmentType: string;
  minSalary: string;
  maxSalary: string;
}

const Filter: React.FC<FilterProps> = ({ onSearch, onFilterChange }) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [filters, setFilters] = useState<FilterState>({
    experienceLevel: "",
    employmentType: "",
    minSalary: "",
    maxSalary: "",
  });

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch(searchTerm);
  };

  const handleFilterChange = (field: keyof FilterState, value: string) => {
    const newFilters = { ...filters, [field]: value };
    setFilters(newFilters);
    onFilterChange(newFilters);
  };

  return (
    <div style={{ position: "relative", width: "100%" }}>
      <Card className="filter-card">
        <Card.Body>
          <Form onSubmit={handleSearch}>
            <Form.Group className="mb-3">
              <InputGroup>
                <Form.Control
                  type="text"
                  placeholder="Search jobs..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                />
                <Button variant="primary" type="submit">
                  Search
                </Button>
              </InputGroup>
            </Form.Group>
          </Form>

          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Experience Level</Form.Label>
              <Form.Select
                value={filters.experienceLevel}
                onChange={(e) =>
                  handleFilterChange("experienceLevel", e.target.value)
                }
              >
                <option value="">All Levels</option>
                <option value="Senior">Senior</option>
                <option value="Mid">Mid</option>
                <option value="Junior">Junior</option>
                <option value="Any">Any</option>
              </Form.Select>
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Employment Type</Form.Label>
              <Form.Select
                value={filters.employmentType}
                onChange={(e) =>
                  handleFilterChange("employmentType", e.target.value)
                }
              >
                <option value="">All Types</option>
                <option value="b2b">B2B</option>
                <option value="permanent">Permanent</option>
                <option value="internship">Internship</option>
                <option value="mandate_contract">Mandate Contract</option>
                <option value="any">Other</option>
              </Form.Select>
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Salary Range</Form.Label>
              <div className="d-flex gap-2">
                <Form.Control
                  type="number"
                  placeholder="Min"
                  value={filters.minSalary}
                  onChange={(e) =>
                    handleFilterChange("minSalary", e.target.value)
                  }
                />
                <Form.Control
                  type="number"
                  placeholder="Max"
                  value={filters.maxSalary}
                  onChange={(e) =>
                    handleFilterChange("maxSalary", e.target.value)
                  }
                />
              </div>
            </Form.Group>
          </Form>
        </Card.Body>
      </Card>
    </div>
  );
};

export default Filter;
