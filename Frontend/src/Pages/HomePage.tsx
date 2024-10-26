import 'react-bootstrap';
import { Container, Nav, Navbar } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import Footer from '../components/Footer';

const HomePage = () => {
    return (
        <>
            <Navbar bg="dark" variant="dark" expand="lg" className="sticky-top">
                <Container>
                    <Navbar.Brand className="me-auto" href="/">JobHunter</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="ms-auto">
                            <LinkContainer to="/">
                                <Nav.Link>Home</Nav.Link>
                            </LinkContainer>
                            <LinkContainer to="/about">
                                <Nav.Link>About</Nav.Link>
                            </LinkContainer>
                            <LinkContainer to="/contact">
                                <Nav.Link>Contact</Nav.Link>
                            </LinkContainer>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
            <Container>
                <p> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam fringilla cursus libero id feugiat.
                    Vivamus venenatis maximus ligula, sed laoreet velit maximus non. In pretium nunc non enim dapibus
                    hendrerit. Proin commodo, lectus vel placerat tristique, nulla lorem ultricies tellus, id posuere
                    ante elit ut ante. Suspendisse convallis, felis sed eleifend consectetur, ligula urna vulputate ex,
                    ut gravida nibh ante vel sapien. Sed nec erat non dui tincidunt ultricies id vel arcu. Donec
                    tincidunt tortor id risus pretium posuere. Aenean ut maximus tortor. Quisque at augue at elit
                    facilisis auctor a quis sapien.
    
                    Fusce eros justo, ornare non laoreet eu, tincidunt a nunc. Ut convallis, enim sit amet sollicitudin
                        finibus, enim nisi ornare lectus, sed gravida turpis massa vitae turpis. Etiam rutrum mauris eget
                        sapien cursus, eget vehicula dolor iaculis. Pellentesque bibendum laoreet viverra. Vestibulum ante
                        ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Duis molestie venenatis arcu
                        non convallis. Duis efficitur a nibh sit amet cursus. Nullam urna dui, porta gravida libero ut,
                        tempus pretium elit. In posuere sem consequat efficitur laoreet. Sed id lobortis magna.
    
                        Aenean volutpat ipsum tempor sem rhoncus pharetra. Phasellus viverra, lectus blandit dapibus
                        ultricies, turpis orci ultricies dui, quis pulvinar purus mi sit amet massa. Fusce aliquet tellus
                        eros, sed suscipit nibh bibendum sit amet. Nunc pretium pharetra pellentesque. Etiam feugiat nec
                        purus vitae gravida. Aliquam finibus a purus eget tristique. Praesent dapibus quam eu consectetur
                        vestibulum. Proin eu sapien vitae ex congue congue. Curabitur ac odio vehicula, egestas ipsum nec,
                        dictum eros. Aenean in sapien ac nisi sollicitudin molestie. Cras mattis viverra quam, tincidunt
                        aliquam leo finibus volutpat. Quisque varius, dui sit amet tempus iaculis, arcu augue condimentum
                        turpis, sit amet congue lacus sem non massa. Pellentesque habitant morbi tristique senectus et netus
                        et malesuada fames ac turpis egestas. Vestibulum ipsum nulla, efficitur consequat elementum et,
                        lobortis a lorem. Proin justo quam, lobortis et faucibus nec, vestibulum facilisis risus.
    
                        Nulla sem nibh, consectetur at porttitor vitae, varius et arcu. Proin sit amet posuere mauris.
                        Vivamus malesuada dapibus fermentum. Proin eu ornare massa. Ut eros magna, tempus at turpis vitae,
                        mollis lobortis turpis. Suspendisse hendrerit, nunc vel vestibulum viverra, metus nulla sollicitudin
                        orci, in scelerisque sapien lacus tincidunt ipsum. Suspendisse et tempor est, ac molestie velit.
    
                        Pellentesque vitae maximus eros. Sed vel justo congue, consectetur augue quis, commodo enim. Aenean
                        imperdiet laoreet urna, eu sagittis mauris. Fusce consequat interdum rhoncus. Vivamus consequat nibh
                        non ligula iaculis venenatis. Nulla facilisi. Donec pellentesque velit faucibus, gravida sem
                        aliquam, venenatis dolor. Aliquam hendrerit a nulla at tempor. Donec aliquam ex augue, sed
                        vestibulum dolor tempor id. Ut non interdum ante. Nulla facilisi. Etiam pretium ante varius, feugiat
                        neque et, volutpat dolor. Vestibulum quis cursus risus, sit amet feugiat eros. In commodo tempor
                        varius. Praesent non sapien facilisis, ultricies magna et, mattis urna.
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam fringilla cursus libero id feugiat.
                        Vivamus venenatis maximus ligula, sed laoreet velit maximus non. In pretium nunc non enim dapibus
                        hendrerit. Proin commodo, lectus vel placerat tristique, nulla lorem ultricies tellus, id posuere
                        ante elit ut ante. Suspendisse convallis, felis sed eleifend consectetur, ligula urna vulputate ex,
                        ut gravida nibh ante vel sapien. Sed nec erat non dui tincidunt ultricies id vel arcu. Donec
                        tincidunt tortor id risus pretium posuere. Aenean ut maximus tortor. Quisque at augue at elit
                        facilisis auctor a quis sapien.
    
                        Fusce eros justo, ornare non laoreet eu, tincidunt a nunc. Ut convallis, enim sit amet sollicitudin
                        finibus, enim nisi ornare lectus, sed gravida turpis massa vitae turpis. Etiam rutrum mauris eget
                        sapien cursus, eget vehicula dolor iaculis. Pellentesque bibendum laoreet viverra. Vestibulum ante
                        ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Duis molestie venenatis arcu
                        non convallis. Duis efficitur a nibh sit amet cursus. Nullam urna dui, porta gravida libero ut,
                        tempus pretium elit. In posuere sem consequat efficitur laoreet. Sed id lobortis magna.
    
                        Aenean volutpat ipsum tempor sem rhoncus pharetra. Phasellus viverra, lectus blandit dapibus
                        ultricies, turpis orci ultricies dui, quis pulvinar purus mi sit amet massa. Fusce aliquet tellus
                        eros, sed suscipit nibh bibendum sit amet. Nunc pretium pharetra pellentesque. Etiam feugiat nec
                        purus vitae gravida. Aliquam finibus a purus eget tristique. Praesent dapibus quam eu consectetur
                        vestibulum. Proin eu sapien vitae ex congue congue. Curabitur ac odio vehicula, egestas ipsum nec,
                        dictum eros. Aenean in sapien ac nisi sollicitudin molestie. Cras mattis viverra quam, tincidunt
                        aliquam leo finibus volutpat. Quisque varius, dui sit amet tempus iaculis, arcu augue condimentum
                        turpis, sit amet congue lacus sem non massa. Pellentesque habitant morbi tristique senectus et netus
                        et malesuada fames ac turpis egestas. Vestibulum ipsum nulla, efficitur consequat elementum et,
                        lobortis a lorem. Proin justo quam, lobortis et faucibus nec, vestibulum facilisis risus.
    
                        Nulla sem nibh, consectetur at porttitor vitae, varius et arcu. Proin sit amet posuere mauris.
                        Vivamus malesuada dapibus fermentum. Proin eu ornare massa. Ut eros magna, tempus at turpis vitae,
                        mollis lobortis turpis. Suspendisse hendrerit, nunc vel vestibulum viverra, metus nulla sollicitudin
                        orci, in scelerisque sapien lacus tincidunt ipsum. Suspendisse et tempor est, ac molestie velit.
    
                        Pellentesque vitae maximus eros. Sed vel justo congue, consectetur augue quis, commodo enim. Aenean
                        imperdiet laoreet urna, eu sagittis mauris. Fusce consequat interdum rhoncus. Vivamus consequat nibh
                        non ligula iaculis venenatis. Nulla facilisi. Donec pellentesque velit faucibus, gravida sem
                        aliquam, venenatis dolor. Aliquam hendrerit a nulla at tempor. Donec aliquam ex augue, sed
                        vestibulum dolor tempor id. Ut non interdum ante. Nulla facilisi. Etiam pretium ante varius, feugiat
                        neque et, volutpat dolor. Vestibulum quis cursus risus, sit amet feugiat eros. In commodo tempor
                        varius. Praesent non sapien facilisis, ultricies magna et, mattis urna.
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam fringilla cursus libero id feugiat.
                        Vivamus venenatis maximus ligula, sed laoreet velit maximus non. In pretium nunc non enim dapibus
                        hendrerit. Proin commodo, lectus vel placerat tristique, nulla lorem ultricies tellus, id posuere
                        ante elit ut ante. Suspendisse convallis, felis sed eleifend consectetur, ligula urna vulputate ex,
                        ut gravida nibh ante vel sapien. Sed nec erat non dui tincidunt ultricies id vel arcu. Donec
                        tincidunt tortor id risus pretium posuere. Aenean ut maximus tortor. Quisque at augue at elit
                        facilisis auctor a quis sapien.
    
                        Fusce eros justo, ornare non laoreet eu, tincidunt a nunc. Ut convallis, enim sit amet sollicitudin
                        finibus, enim nisi ornare lectus, sed gravida turpis massa vitae turpis. Etiam rutrum mauris eget
                        sapien cursus, eget vehicula dolor iaculis. Pellentesque bibendum laoreet viverra. Vestibulum ante
                        ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Duis molestie venenatis arcu
                        non convallis. Duis efficitur a nibh sit amet cursus. Nullam urna dui, porta gravida libero ut,
                        tempus pretium elit. In posuere sem consequat efficitur laoreet. Sed id lobortis magna.
    
                        Aenean volutpat ipsum tempor sem rhoncus pharetra. Phasellus viverra, lectus blandit dapibus
                        ultricies, turpis orci ultricies dui, quis pulvinar purus mi sit amet massa. Fusce aliquet tellus
                        eros, sed suscipit nibh bibendum sit amet. Nunc pretium pharetra pellentesque. Etiam feugiat nec
                        purus vitae gravida. Aliquam finibus a purus eget tristique. Praesent dapibus quam eu consectetur
                        vestibulum. Proin eu sapien vitae ex congue congue. Curabitur ac odio vehicula, egestas ipsum nec,
                        dictum eros. Aenean in sapien ac nisi sollicitudin molestie. Cras mattis viverra quam, tincidunt
                        aliquam leo finibus volutpat. Quisque varius, dui sit amet tempus iaculis, arcu augue condimentum
                        turpis, sit amet congue lacus sem non massa. Pellentesque habitant morbi tristique senectus et netus
                        et malesuada fames ac turpis egestas. Vestibulum ipsum nulla, efficitur consequat elementum et,
                        lobortis a lorem. Proin justo quam, lobortis et faucibus nec, vestibulum facilisis risus.
    
                        Nulla sem nibh, consectetur at porttitor vitae, varius et arcu. Proin sit amet posuere mauris.
                        Vivamus malesuada dapibus fermentum. Proin eu ornare massa. Ut eros magna, tempus at turpis vitae,
                        mollis lobortis turpis. Suspendisse hendrerit, nunc vel vestibulum viverra, metus nulla sollicitudin
                        orci, in scelerisque sapien lacus tincidunt ipsum. Suspendisse et tempor est, ac molestie velit.
    
                        Pellentesque vitae maximus eros. Sed vel justo congue, consectetur augue quis, commodo enim. Aenean
                        imperdiet laoreet urna, eu sagittis mauris. Fusce consequat interdum rhoncus. Vivamus consequat nibh
                        non ligula iaculis venenatis. Nulla facilisi. Donec pellentesque velit faucibus, gravida sem
                        aliquam, venenatis dolor. Aliquam hendrerit a nulla at tempor. Donec aliquam ex augue, sed
                        vestibulum dolor tempor id. Ut non interdum ante. Nulla facilisi. Etiam pretium ante varius, feugiat
                        neque et, volutpat dolor. Vestibulum quis cursus risus, sit amet feugiat eros. In commodo tempor
                        varius. Praesent non sapien facilisis, ultricies magna et, mattis urna.
                    </p>
                </Container>
                <Footer/>
            </>

            );
            }

            export default HomePage;
