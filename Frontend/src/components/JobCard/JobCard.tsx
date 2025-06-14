import React from "react";
import { Badge } from "react-bootstrap";
import "./JobCard.css";

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

interface JobCardProps {
  title?: string;
  companyName?: string;
  companyLogoUrl?: string;
  requiredSkills?: SkillGroup[];
  experienceLevel?: string;
  employmentTypes?: { $values: EmploymentType[] };
}

const extractSkillNames = (
  requiredSkills?: SkillGroup[] | undefined
): string[] => {
  if (!requiredSkills) return [];

  const allSkills: Skill[] = requiredSkills.flatMap((group) => {
    if ("$values" in group.skills) {
      return group.skills.$values;
    }
    return group.skills;
  });

  return Array.from(new Set(allSkills.map((skill) => skill.name)));
};

const extractEmploymentTypes = (employmentTypes?: {
  $values: EmploymentType[];
}): string[] => {
  if (!employmentTypes) return [];

  return employmentTypes.$values.map((employment) => {
    const range =
      employment.from !== undefined && employment.to !== undefined
        ? `${employment.from} - ${employment.to}`
        : "Not given";
    return `${employment.type.toUpperCase()} (${
      employment.gross ? "Gross" : "Net"
    }) - ${range} ${employment.currency.toUpperCase()}`;
  });
};

const JobCard: React.FC<JobCardProps> = (props) => {
  const skillNames = extractSkillNames(props.requiredSkills);
  const employmentTypeStrings = extractEmploymentTypes(props.employmentTypes);

  return (
    <div className="card border-primary my-3">
      <div className="card-header d-flex align-items-center justify-content-start gap-2">
        <h5 className="mb-0">{props.companyName}</h5>
        {props.experienceLevel && (
          <Badge bg="info" className="ms-auto" style={{ fontSize: "1rem" }}>
            {props.experienceLevel}
          </Badge>
        )}
      </div>
      <div className="card-body align-items-baseline">
        <h4 className="card-title">{props.title}</h4>
        <div className="card-text d-flex flex-wrap gap-2">
          {employmentTypeStrings.length > 0 ? (
            employmentTypeStrings.map((employment, index) => (
              <Badge key={index} bg="info" className="me-1 mb-1">
                {employment}
              </Badge>
            ))
          ) : (
            <span>No employment types</span>
          )}
        </div>
        <div className="card-text d-flex flex-wrap gap-2 mb-2">
          {skillNames.length > 0 ? (
            skillNames.map((name, index) => (
              <Badge key={index} bg="secondary" className="me-1 mb-1">
                {name}
              </Badge>
            ))
          ) : (
            <span>No skills</span>
          )}
        </div>
      </div>
    </div>
  );
};

export default JobCard;
