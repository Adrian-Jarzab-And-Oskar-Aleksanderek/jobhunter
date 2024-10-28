import { Container } from "react-bootstrap";
import "./JobCard.css";

const JobCard = () => {
    return (
        <>
        <Container className="d-inline-flex col-3 gap-3 w-75 my-2 text-start justify-content-center align-items-center jh-job-card-container">
            <h5>Position</h5>
            <h6>Company</h6>
            <ul className="list-inline">
                <li className="list-inline-item">tech-stack 1</li>
                <li className="list-inline-item">tech-stack 2</li>
                <li className="list-inline-item">tech-stack 3</li>
            </ul>
        </Container>
        </>
    );
};

export default JobCard;