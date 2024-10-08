import { LockOutlined } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import { HubConnectionBuilder } from "@microsoft/signalr";
import {
  Avatar,
  Box,
  Container,
  Grid,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { FieldValues, useForm } from "react-hook-form";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAppDispatch } from "../../app/store/configureStore";
import { logInUser } from "./accountSlice";

export default function Login() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const location = useLocation();

  const connection = new HubConnectionBuilder()
    .withUrl("http://localhost:16000/AuthService/Hubs/test")
    .build();

  connection.start()
    .then(() => console.log('Connected to SignalR hub'))
    .catch(err => console.error('Error connecting to hub:', err));

  connection.on('AccountCreated', message => {
    console.log('AccountCreated:', message);
    connection.send('SendMessage', "ThisUser", 'Hello from client');
  });

  const {
    register,
    handleSubmit,
    formState: { isSubmitting, errors, isValid },
  } = useForm({
    mode: "onBlur",
  });

  async function submitForm(data: FieldValues) {
    try {
      await dispatch(logInUser(data));
      navigate(location.state?.from || "/");
    } catch (error: any) {
      console.log(error.message);
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
        Sign in
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit(submitForm)}
        noValidate
        sx={{ mt: 1 }}
      >
        <TextField
          margin="normal"
          required
          fullWidth
          label="Username"
          autoFocus
          {...register("userName", { required: "Username is required" })}
          error={!!errors.username}
          helperText={errors?.username?.message as string}
        />
        <TextField
          margin="normal"
          required
          fullWidth
          label="Password"
          type="password"
          {...register("password", { required: "Password is required" })}
          error={!!errors.password}
          helperText={errors?.password?.message as string}
        />
        <LoadingButton
          disabled={!isValid}
          loading={isSubmitting}
          type="submit"
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Sign In
        </LoadingButton>
        <Grid container>
          <Grid item>
            <Link to="/register" style={{ textDecoration: "none" }}>
              {"Don't have an account? Sign Up"}
            </Link>
          </Grid>
        </Grid>
      </Box>
    </Container>
  );
}
