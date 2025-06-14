import React, { useState } from "react";
import { useAuth } from "../Context/useAuth";
import { Button, Form, Card, Collapse } from "react-bootstrap";
import axios, { AxiosError } from "axios";
import { toast } from "react-toastify";
import { UserProfileToken } from "../Models/User";
import Navigation from "../components/Navigation/Navigation.tsx";

const UserProfilePage = () => {
  const { user, setToken, setUser } = useAuth();

  const [showForm, setShowForm] = useState({
    username: false,
    email: false,
    password: false,
  });

  const [inputs, setInputs] = useState({
    newUsername: "",
    newPassword: "",
    newEmail: "",
    actualPassword: "",
  });

  const toggleForm = (form: keyof typeof showForm) => {
    setShowForm((prev) => ({ ...prev, [form]: !prev[form] }));
  };

  const updateLocalAuth = (token: string, userName: string, email: string) => {
    const userObj = { userName, email };
    localStorage.setItem("token", token);
    localStorage.setItem("user", JSON.stringify(userObj));
    axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    setToken(token);
    setUser(userObj);
  };

  const handleFieldChange = async (
    e: React.FormEvent,
    endpoint: string,
    payload: object,
    successMessage: string,
    clearFields: (keyof typeof inputs)[]
  ) => {
    e.preventDefault();
    try {
      const response = await axios.post<UserProfileToken>(
        `https://localhost:7111/api/Account/${endpoint}`,
        payload
      );

      const { Token, UserName, Email } = response.data;

      if (!Token) {
        console.error("Token not included in response:", response.data);
        toast.error("Error: Token not included in response");
        return;
      }

      updateLocalAuth(Token, UserName, Email);
      toast.success(successMessage);

      setInputs((prev) => {
        const updated = { ...prev };
        clearFields.forEach((field) => (updated[field] = ""));
        return updated;
      });

      setShowForm({ username: false, email: false, password: false });
    } catch (error) {
      console.error("Error while changing user data:", error);
      if (axios.isAxiosError(error)) {
        toast.error(`Error: ${error.response?.data ?? "Request failed."}`);
      } else {
        toast.error("An unexpected error occurred.");
      }
    }
  };

  return (
    <>
      <Navigation />
      <div className="container py-4">
        <h2 className="mb-4">User Profile</h2>
        <Card className="mb-4">
          <Card.Body>
            {/* Username Section */}
            <div className="d-flex align-items-center mb-3">
              <p className="mb-0">
                <strong>Username:</strong> {user?.userName}
              </p>
              <Button
                variant="outline-primary"
                size="sm"
                onClick={() => toggleForm("username")}
                aria-controls="username-form"
                aria-expanded={showForm.username}
                className="ms-2"
              >
                Change
              </Button>
            </div>
            <Collapse in={showForm.username}>
              <div id="username-form" className="mb-3">
                <Form
                  onSubmit={(e) =>
                    handleFieldChange(
                      e,
                      "ChangeUsername",
                      { newUserName: inputs.newUsername },
                      "Username changed successfully.",
                      ["newUsername"]
                    )
                  }
                >
                  <Form.Group>
                    <Form.Label>New Username</Form.Label>
                    <Form.Control
                      type="text"
                      value={inputs.newUsername}
                      onChange={(e) =>
                        setInputs({ ...inputs, newUsername: e.target.value })
                      }
                    />
                  </Form.Group>
                  <Button type="submit" className="mt-2">
                    Save
                  </Button>
                </Form>
              </div>
            </Collapse>

            {/* Email Section */}
            <div className="d-flex align-items-center mb-3">
              <p className="mb-0">
                <strong>Email:</strong> {user?.email}
              </p>
              <Button
                variant="outline-primary"
                size="sm"
                onClick={() => toggleForm("email")}
                aria-controls="email-form"
                aria-expanded={showForm.email}
                className="ms-2"
              >
                Change
              </Button>
            </div>
            <Collapse in={showForm.email}>
              <div id="email-form" className="mb-3">
                <Form
                  onSubmit={(e) =>
                    handleFieldChange(
                      e,
                      "ChangeEmail",
                      {
                        newEmail: inputs.newEmail,
                      },
                      "Email changed successfully.",
                      ["newEmail"]
                    )
                  }
                >
                  <Form.Group>
                    <Form.Label>New Email</Form.Label>
                    <Form.Control
                      type="email"
                      value={inputs.newEmail}
                      onChange={(e) =>
                        setInputs({ ...inputs, newEmail: e.target.value })
                      }
                    />
                  </Form.Group>
                  <Button type="submit" className="mt-2">
                    Save
                  </Button>
                </Form>
              </div>
            </Collapse>

            <div className="d-flex align-items-center mb-3">
              <p className="mb-0">
                <strong>Password:</strong> ********
              </p>
              <Button
                variant="outline-primary"
                size="sm"
                onClick={() => toggleForm("password")}
                aria-controls="password-form"
                aria-expanded={showForm.password}
                className="ms-2"
              >
                Change
              </Button>
            </div>
            <Collapse in={showForm.password}>
              <div id="password-form" className="mb-3">
                <Form
                  onSubmit={(e) =>
                    handleFieldChange(
                      e,
                      "ChangePassword",
                      {
                        actualPassword: inputs.actualPassword,
                        NewPassword: inputs.newPassword,
                      },
                      "Password changed successfully.",
                      ["actualPassword", "newPassword"]
                    )
                  }
                >
                  <Form.Group>
                    <Form.Label>Current Password</Form.Label>
                    <Form.Control
                      type="password"
                      value={inputs.actualPassword}
                      onChange={(e) =>
                        setInputs({
                          ...inputs,
                          actualPassword: e.target.value,
                        })
                      }
                    />
                  </Form.Group>
                  <Form.Group className="mt-2">
                    <Form.Label>New Password</Form.Label>
                    <Form.Control
                      type="password"
                      value={inputs.newPassword}
                      onChange={(e) =>
                        setInputs({ ...inputs, newPassword: e.target.value })
                      }
                    />
                  </Form.Group>
                  <Button type="submit" className="mt-2">
                    Save
                  </Button>
                </Form>
              </div>
            </Collapse>
          </Card.Body>
        </Card>
      </div>
    </>
  );
};

export default UserProfilePage;
