import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useAuth } from "../Context/useAuth";
import { useForm } from "react-hook-form";

type LoginFormsInputs = {
  userName: string;
  password: string;
};

const validation = Yup.object().shape({
  userName: Yup.string().required("Username is required"),
  password: Yup.string().required("Password is required"),
});

const LoginPage = () => {
  const { loginUser } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormsInputs>({ resolver: yupResolver(validation) });

  const handleLogin = (form: LoginFormsInputs) => {
    loginUser(form.userName, form.password);
  };

  return (
    <div className="container d-flex justify-content-center align-items-center vh-100">
      <div
        className="card shadow-sm p-4"
        style={{ maxWidth: "400px", width: "100%" }}
      >
        <h2 className="card-title text-center mb-4">Sign in to your account</h2>
        <form onSubmit={handleSubmit(handleLogin)} noValidate>
          <div className="mb-3">
            <label htmlFor="userName" className="form-label">
              Username
            </label>
            <input
              type="text"
              id="userName"
              autoComplete="username"
              placeholder="Username"
              className={`form-control ${errors.userName ? "is-invalid" : ""}`}
              {...register("userName")}
            />
            {errors.userName && (
              <div className="invalid-feedback">{errors.userName.message}</div>
            )}
          </div>

          <div className="mb-3">
            <label htmlFor="password" className="form-label">
              Password
            </label>
            <input
              type="password"
              id="password"
              autoComplete="current-password"
              placeholder="••••••••"
              className={`form-control ${errors.password ? "is-invalid" : ""}`}
              {...register("password")}
            />
            {errors.password && (
              <div className="invalid-feedback">{errors.password.message}</div>
            )}
          </div>

          <div className="mb-3 d-flex justify-content-between">
            <a href="/forgot-password" className="text-decoration-none">
              Forgot password?
            </a>
          </div>

          <button type="submit" className="btn btn-primary w-100 mb-3">
            Sign in
          </button>
        </form>

        <div className="text-center">
          <small>
            Don’t have an account yet?{" "}
            <a href="/register" className="text-decoration-none">
              Sign up
            </a>
          </small>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
