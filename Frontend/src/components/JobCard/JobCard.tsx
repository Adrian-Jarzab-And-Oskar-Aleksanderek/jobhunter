import {Button, Card, Container } from "react-bootstrap";
import "./JobCard.css";

const JobCard = (props: {position: string, company: string, techStack: string[]}) => {
    return (
        <>
            <Container>
                <Card className="my-3 jh-job-card">
                    <Card.Body className="d-flex justify-content-between align-items-center">
                        <div>
                            <Card.Title className="jh-job-card-text-main">{props.position}</Card.Title>
                            <Card.Text className="jh-job-card-text-main">{props.company}</Card.Text>
                        </div>

                        <ul className="list-inline mb-2 jh-job-card-text-main">
                            {props.techStack.map((item, index) => (
                                <li key={index} className="list-inline-item">| {item} |</li>
                            ))}
                        </ul>
                        
                        <div className="d-flex flex-column align-items-end">
                            <Button className="jh-job-card-button">Button</Button>
                        </div>
                    </Card.Body>
                </Card>
            </Container>
        </>
    );
};

export default JobCard;