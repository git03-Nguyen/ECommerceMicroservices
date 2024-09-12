import { LockOutlined } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import {
  Avatar,
  Box,
  Container,
  Grid,
  MenuItem,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { set, useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import agent from "../../app/api/agent";

export default function Register() {
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    setError,
    formState: { isSubmitting, errors, isValid },
  } = useForm({
    mode: "onTouched",
  });

  function handleApiErrors(errors: any) {
    console.log(errors.data);
    if (errors.data.errors) {
      errors.data.errors.forEach((error: any) => {
        setError(error.field, { message: error.message });
      });
    } else {
      toast.error(errors.data.message);
    }

  }

  return (
    <Container
      component={Paper}
      maxWidth="sm"
      sx={{
        p: 4,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
        <LockOutlined />
      </Avatar>
      <Typography component="h1" variant="h5">
        Register
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit((data) =>
          agent.Account.register(data)
            .then(() => {
              toast.success("Registration successful - you can now login");
              navigate("/login");
            })
            .catch((error) => handleApiErrors(error))
        )}
        noValidate
        sx={{ mt: 1 }}
      >
        <TextField
          margin="normal"
          required
          fullWidth
          label="Username"
          autoFocus
          {...register("username", { required: "Username is required" })}
          error={!!errors.username}
          helperText={errors?.username?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Email"
          {...register("email", {
            required: "Email is required",
            pattern: {
              value: /^([a-zA-Z0-9_\-.]+)@([a-zA-Z0-9_\-.]+)\.([a-zA-Z]{2,5})$/,
              message: "Not a valid email address",
            },
          })}
          error={!!errors.email}
          helperText={errors?.email?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Password"
          type="password"
          {...register("password", {
            required: "password is required",
            pattern: {
              value: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/,
              message: "Password does not meet complexity requirements",
            },
          })}
          error={!!errors.password}
          helperText={errors?.password?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Full Name"
          {...register("fullName", { required: "Full Name is required" })}
          error={!!errors.fullName}
          helperText={errors?.fullname?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Phone Number"
          {...register("phoneNumber", {
            required: "Phone Number is required",
            pattern: {
              value: /^[0-9]{10}$/,
              message: "Phone Number does not meet complexity requirements",
            },
          })}
          error={!!errors.phoneNumber}
          helperText={errors?.phoneNumber?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Address"
          {...register("address", { required: "Address is required" })}
          error={!!errors.address}
          helperText={errors?.address?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Role"
          select
          {...register("role", { required: "Role is required" })}
          error={!!errors.role}
          helperText={errors?.role?.message as string}
        >
          <MenuItem value="Admin">Admin</MenuItem>
          <MenuItem value="Customer">Customer</MenuItem>
          <MenuItem value="Seller">Seller</MenuItem>
        </TextField>

        <LoadingButton
          disabled={!isValid}
          loading={isSubmitting}
          type="submit"
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Register
        </LoadingButton>
        <Grid container>
          <Grid item>
            <Link to="/login" style={{ textDecoration: "none" }}>
              {"Already have an account? Sign In"}
            </Link>
          </Grid>
        </Grid>
      </Box>
    </Container>
  );
}
