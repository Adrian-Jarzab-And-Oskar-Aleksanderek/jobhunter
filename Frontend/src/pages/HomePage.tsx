import 'react-bootstrap';
import { Container } from 'react-bootstrap';
import Navigation from "../components/Navigation/Navigation.tsx";
import Footer from "../components/Footer/Footer.tsx";
import JobCard from "../components/JobCard/JobCard.tsx";

const HomePage = () => {
    return (
        <>
            <Navigation />
            <Container fluid className="d-flex flex-column jh-container-fluid" style={{minHeight: '100vh'}}>
                <JobCard />
                <p> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam fringilla cursus libero id feugiat.
                    Vivamus venenatis maximus ligula, sed laoreet velit maximus non. In pretium nunc non enim dapibus
                    hendrerit. Proin commodo, lectus vel placerat tristique, nulla lorem ultricies tellus, id posuere
                    ante elit ut ante. Suspendisse convallis, felis sed eleifend consectetur, ligula urna vulputate ex,
                    ut gravida nibh ante vel sapien. Sed nec erat non dui tincidunt ultricies id vel arcu. Donec
                    tincidunt tortor id risus pretium posuere. Aenean ut maximus tortor. Quisque at augue at elit
                    facilisis auctor a quis sapien.
                    </p>
                </Container>
            <Footer/>
            </>

            );
            }

            export default HomePage;
