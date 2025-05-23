import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useAuth } from "../Context/useAuth";
import { useForm } from "react-hook-form";

type RegisterFormsInputs = {
  email: string;
  userName: string;
  password: string;
};

const validation = Yup.object().shape({
  email: Yup.string().email("Invalid email").required("Email is required"),
  userName: Yup.string()
    .min(3, "Username too short")
    .required("Username is required"),
  password: Yup.string()
    .min(6, "Password too short")
    .required("Password is required"),
});

const RegisterPage = () => {
  const { registerUser } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormsInputs>({ resolver: yupResolver(validation) });

  const handleRegister = (form: RegisterFormsInputs) => {
    registerUser(form.email, form.userName, form.password);
  };

  return (
    <div className="container d-flex justify-content-center align-items-center vh-100">
      <div
        className="card shadow-sm p-4"
        style={{ maxWidth: "400px", width: "100%" }}
      >
        <h2 className="card-title text-center mb-4">Create your account</h2>
        <form onSubmit={handleSubmit(handleRegister)} noValidate>
          <div className="mb-3">
            <label htmlFor="email" className="form-label">
              Email address
            </label>
            <input
              id="email"
              type="email"
              placeholder="you@example.com"
              className={`form-control ${errors.email ? "is-invalid" : ""}`}
              {...register("email")}
            />
            {errors.email && (
              <div className="invalid-feedback">{errors.email.message}</div>
            )}
          </div>

          <div className="mb-3">
            <label htmlFor="userName" className="form-label">
              Username
            </label>
            <input
              id="userName"
              type="text"
              placeholder="Your username"
              className={`form-control ${errors.userName ? "is-invalid" : ""}`}
              {...register("userName")}
            />
            {errors.userName && (
              <div className="invalid-feedback">{errors.userName.message}</div>
            )}
          </div>

          <div className="mb-4">
            <label htmlFor="password" className="form-label">
              Password
            </label>
            <input
              id="password"
              type="password"
              placeholder="••••••••"
              className={`form-control ${errors.password ? "is-invalid" : ""}`}
              {...register("password")}
            />
            {errors.password && (
              <div className="invalid-feedback">{errors.password.message}</div>
            )}
          </div>

          <button type="submit" className="btn btn-primary w-100 mb-3">
            Register
          </button>
        </form>

        <div className="text-center">
          <small>
            Have an account already?{" "}
            <a href="/login" className="text-decoration-none">
              Sign in
            </a>
          </small>
        </div>
      </div>
    </div>
  );
};

export default RegisterPage;
