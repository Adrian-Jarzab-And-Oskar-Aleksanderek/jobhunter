import { useState } from 'react';
import { Container, Form, Button, Row, Col } from "react-bootstrap";

const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        // Basic validation
        if (!email || !password) {
            setError('Both email and password are required');
            return;
        }

        setLoading(true);
        setError('');

        const loginData = { "username":email, "password":password };

        try {
            const response = await fetch('https://localhost:5216/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(loginData),
            });

            if (response.ok) {
                const data = await response.json();
                console.log('Login successful:', data);
                // Handle successful login here (e.g. store token in localStorage)
            } else {
                const data = await response.json();
                setError(data.message || 'Invalid credentials');
            }
        } catch (err) {
            setError('Something went wrong');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Container fluid className="d-flex flex-column jh-container-fluid" style={{ minHeight: '100vh' }}>
                <Row className="justify-content-center align-items-center flex-grow-1">
                    <Col md={4}>
                        <h1 className="text-center mb-4">Login Page</h1>
                        {error && <div className="alert alert-danger">{error}</div>}
                        <Form onSubmit={handleSubmit}>
                            <Form.Group className="mb-3" controlId="formBasicEmail">
                                <Form.Label>Email address</Form.Label>
                                <Form.Control
                                    type="email"
                                    placeholder="Enter email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                />
                            </Form.Group>

                            <Form.Group className="mb-3" controlId="formBasicPassword">
                                <Form.Label>Password</Form.Label>
                                <Form.Control
                                    type="password"
                                    placeholder="Password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                />
                            </Form.Group>

                            <Button variant="primary" type="submit" className="w-100" disabled={loading}>
                                {loading ? 'Logging in...' : 'Login'}
                            </Button>
                        </Form>
                    </Col>
                </Row>
            </Container>
        </>
    );
}

export default LoginPage;
