import 'react-bootstrap';
import { Container } from 'react-bootstrap';
import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import JobCard from "../components/JobCard/JobCard.tsx";
import {useEffect, useState } from 'react';

const HomePage = () => {

    const [position, setPosition] = useState('');
    const [company, setCompany] = useState('');
    const [techStack, setTechStack] = useState(['']);

    useEffect(() => {
        setPosition("Junior Full Stack Developer");
        setCompany("ABC Inc.");
        setTechStack(["React", "Javascript", "C#/.NET", "ASP.NET"]);
    }, []);

    return (
        <>
            <Navigation/>
            <Container fluid className="d-flex flex-column">
                {Array.from({length: 10}).map((_, index) => (
                    <JobCard key={index} position={position} company={company} techStack={techStack}/>
                ))}
            </Container>
            <Footer/>
        </>

    );
}
    export default HomePage;
